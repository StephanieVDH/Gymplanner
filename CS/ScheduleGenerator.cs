using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GymPlanner.CS
{
    public class UserPreference
    {
        public int UserId { get; set; }
        public int GoalId { get; set; }
        public int LevelId { get; set; }
        public int AvailableDaysPerWeek { get; set; }
        public int SessionDuration { get; set; }
        public List<int> MuscleGroupIds { get; set; } = new List<int>();
    }

    public class ExerciseGenerator
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class ScheduleGenerator
    {
        private readonly string _connectionString;

        public ScheduleGenerator(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void GenerateForUser(int userId)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            using var transaction = conn.BeginTransaction();
            try
            {
                // 1. Load user preferences
                var pref = LoadPreferences(conn, transaction, userId);

                // 2. Determine workout days of week
                var workoutDays = DetermineWorkoutDays(pref.AvailableDaysPerWeek);

                // 3. Create schedule record
                int scheduleId = InsertSchedule(conn, transaction, userId);

                // 4. For each chosen day
                foreach (int dayOfWeek in workoutDays)
                {
                    int scheduleDayId = InsertScheduleDay(conn, transaction, scheduleId, dayOfWeek);

                    // 5. Load exercises for this day
                    var candidates = LoadExercises(conn, transaction,
                        pref.MuscleGroupIds, pref.LevelId);

                    // 6. Determine how many exercises fit
                    int exerciseCount = Math.Max(1, pref.SessionDuration / 10);

                    // 7. Random selection
                    var selected = candidates.OrderBy(e => Guid.NewGuid())
                                          .Take(exerciseCount)
                                          .ToList();

                    // 8. Determine sets & reps based solely on goal
                    (int sets, int reps) = MapSetsReps(pref.GoalId);

                    // 9. Insert schedule items
                    foreach (var ex in selected)
                    {
                        InsertScheduleItem(conn, transaction, scheduleDayId, ex.Id, sets, reps);
                    }
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        private UserPreference LoadPreferences(MySqlConnection conn, MySqlTransaction tx, int userId)
        {
            var pref = new UserPreference { UserId = userId };
            const string sql = @"
SELECT up.goal_id, up.level_id, up.available_days_per_week, up.session_duration_minutes
FROM user_preferences up
WHERE up.user_id = @uid;";
            using var cmd = new MySqlCommand(sql, conn, tx);
            cmd.Parameters.AddWithValue("@uid", userId);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) throw new Exception("User preferences not found.");
            pref.GoalId = reader.GetInt32("goal_id");
            pref.LevelId = reader.GetInt32("level_id");
            pref.AvailableDaysPerWeek = reader.GetInt32("available_days_per_week");
            pref.SessionDuration = reader.GetInt32("session_duration_minutes");
            reader.Close();

            // Load muscle groups
            const string mgSql = @"
            SELECT pmg.muscle_group_id
            FROM preference_muscle_groups AS pmg
            INNER JOIN user_preferences AS up
                ON pmg.preference_id = up.id
            WHERE up.user_id = @uid;";

            using var mgCmd = new MySqlCommand(mgSql, conn, tx);
            mgCmd.Parameters.AddWithValue("@uid", userId);

            using var mgReader = mgCmd.ExecuteReader();
            while (mgReader.Read())
            {
                pref.MuscleGroupIds.Add(mgReader.GetInt32("muscle_group_id"));
            }
            mgReader.Close();
            // ↑ end replacement block ↑

            return pref;
        }

        private List<int> DetermineWorkoutDays(int daysPerWeek)
        {
            if (daysPerWeek < 1 || daysPerWeek > 7)
                throw new ArgumentException("Days per week must be between 1 and 7.");

            // Define available slots: Mon-Fri for <=5, add Sat for 6, Sun for 7
            int totalSlots = daysPerWeek <= 5 ? 5 : (daysPerWeek == 6 ? 6 : 7);
            double interval = (double)(totalSlots - 1) / (daysPerWeek - 1);

            var slots = new List<int>();
            for (int i = 0; i < daysPerWeek; i++)
            {
                int slotIndex = (int)Math.Round(i * interval);
                // DayOfWeek: Monday=1, ... Sunday=7
                int dayOfWeek = slotIndex + 1;
                slots.Add(dayOfWeek);
            }
            return slots;
        }

        private int InsertSchedule(MySqlConnection conn, MySqlTransaction tx, int userId)
        {
            const string sql = @"
INSERT INTO schedules (user_id)
VALUES (@uid);
SELECT LAST_INSERT_ID();";
            using var cmd = new MySqlCommand(sql, conn, tx);
            cmd.Parameters.AddWithValue("@uid", userId);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private int InsertScheduleDay(MySqlConnection conn, MySqlTransaction tx,
                                      int scheduleId, int dayOfWeek)
        {
            const string sql = @"
INSERT INTO schedule_days (schedule_id, day_of_week_id)
VALUES (@sid, @dow);
SELECT LAST_INSERT_ID();";
            using var cmd = new MySqlCommand(sql, conn, tx);
            cmd.Parameters.AddWithValue("@sid", scheduleId);
            cmd.Parameters.AddWithValue("@dow", dayOfWeek);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private List<ExerciseGenerator> LoadExercises(MySqlConnection conn, MySqlTransaction tx,
                                             List<int> muscleGroupIds, int difficultyId)
        {
            const string sql = @"
SELECT DISTINCT e.id, e.name
FROM exercises e
JOIN exercise_muscle_groups emg ON e.id = emg.exercise_id
WHERE emg.muscle_group_id IN ({0})
  AND e.difficulty_id = @diff;";

            // Build parameter list
            var parameters = new List<string>();
            for (int i = 0; i < muscleGroupIds.Count; i++)
                parameters.Add("@mg" + i);

            string inClause = string.Join(",", parameters);
            using var cmd = new MySqlCommand(string.Format(sql, inClause), conn, tx);
            for (int i = 0; i < muscleGroupIds.Count; i++)
                cmd.Parameters.AddWithValue(parameters[i], muscleGroupIds[i]);
            cmd.Parameters.AddWithValue("@diff", difficultyId);

            var list = new List<ExerciseGenerator>();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ExerciseGenerator { Id = reader.GetInt32("id"), Name = reader.GetString("name") });
            }
            reader.Close();
            return list;
        }

        private (int sets, int reps) MapSetsReps(int goalId)
        {
            // Sets & reps depend solely on the user's goal
            return goalId switch
            {
                1 => (3, 15),  // Endurance
                2 => (4, 10),  // Hypertrophy
                3 => (5, 5),   // Strength
                _ => (3, 12)
            };
        }

        private void InsertScheduleItem(MySqlConnection conn, MySqlTransaction tx,
                                        int scheduleDayId, int exerciseId, int sets, int reps)
        {
            const string sql = @"
INSERT INTO schedule_items (schedule_day_id, exercise_id, sets, reps)
VALUES (@sdid, @eid, @sets, @reps);";
            using var cmd = new MySqlCommand(sql, conn, tx);
            cmd.Parameters.AddWithValue("@sdid", scheduleDayId);
            cmd.Parameters.AddWithValue("@eid", exerciseId);
            cmd.Parameters.AddWithValue("@sets", sets);
            cmd.Parameters.AddWithValue("@reps", reps);
            cmd.ExecuteNonQuery();
        }
    }

}
