using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Gymplanner.CS;
using MySqlConnector;

namespace Gymplanner
{
    public class Data
    {
        // 1) point to your local XAMPP MySQL server
        private string connectionString =
            "datasource=127.0.0.1;" +
            "port=3306;" +
            "username=root;password=;" +
            "database=gymplanner;";

        private const int _admin = 1;
        private const int _user = 2;

        private int Insert(string query)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, connection);

            try
            {
                connection.Open();
                int result = commandDatabase.ExecuteNonQuery();
                return (int)commandDatabase.LastInsertedId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
        }

        private int Delete(string query)
        {
            using var connection = new MySqlConnection(connectionString);
            using var command = new MySqlCommand(query, connection);

            try
            {
                connection.Open();
                // ExecuteNonQuery returns number of rows affected
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }


        // USERS:
        // 1. Registreren 
        public int InsertUser(User user)
        {
            string query = $"INSERT INTO users (id, username, email, password_hash) " +
                           $"VALUES (NULL,'{user.Username}', '{user.Email}', '{user.PasswordHash}');";
            return this.Insert(query);
        }

        public string? GetPasswordHash(string email)
        {
            string query =
                $"SELECT password_hash " +
                $"FROM users " +
                $"WHERE email = '{email}' " +
                $"LIMIT 1;";

            using var conn = new MySqlConnection(connectionString);
            using var cmd = new MySqlCommand(query, conn);

            conn.Open();
            var result = cmd.ExecuteScalar();
            return result as string;   
        }

        //2. User overview voor admin page
        public List<User> GetUsers()
        {
            var list = new List<User>();
            using var conn = new MySqlConnection(connectionString);
            conn.Open();

            using var cmd = new MySqlCommand(
            @"SELECT u.id,
                     u.username,
                     u.email,
                     r.name   AS role,
                     u.created_at
              FROM users u
              JOIN roles r
                ON u.role_id = r.id;", conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new User
                {
                    Id = reader.GetInt32("id"),
                    Username = reader.GetString("username"),
                    Email = reader.GetString("email"),
                    Role = reader.GetString("role"),        // now valid
                    CreatedAt = reader.GetDateTime("created_at")
                });
            }
            return list;
        }

        // 3. User verwijderen
        public bool DeleteUser(int userId)
        {
            string sql = $"DELETE FROM users WHERE id = {userId};";
            return Delete(sql) > 0;
        }


        // EXERCISES:
        // 1. Overview van exercises voor admin page
        public List<Exercise> GetExercises()
        {
            var list = new List<Exercise>();
            using var conn = new MySqlConnection(connectionString);
            conn.Open();

            var sql = @"
        SELECT 
            e.id,
            e.name,
            e.description,
            e.difficulty_id,
            dl.name            AS difficulty,
            GROUP_CONCAT(mg.name SEPARATOR ', ') AS muscle_groups
        FROM exercises e
        JOIN difficulty_levels dl 
          ON e.difficulty_id = dl.id
        LEFT JOIN exercise_muscle_groups emg 
          ON e.id = emg.exercise_id
        LEFT JOIN muscle_groups mg 
          ON emg.muscle_group_id = mg.id
        GROUP BY e.id, e.name, e.description, e.difficulty_id, dl.name;
    ";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var ex = new Exercise
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Description = reader.IsDBNull(reader.GetOrdinal("description"))
                                           ? string.Empty
                                           : reader.GetString("description"),
                    DifficultyId = reader.GetInt32("difficulty_id"),
                    Difficulty = reader.GetString("difficulty"),
                    MuscleGroupNames = reader.IsDBNull(reader.GetOrdinal("muscle_groups"))
                                           ? string.Empty
                                           : reader.GetString("muscle_groups"),
                };
                list.Add(ex);
            }

            return list;
        }


        // Exercise verwijderen:
        public bool DeleteExercise(int exerciseId)
        {
            string sql = $"DELETE FROM exercises WHERE id = {exerciseId};";
            return Delete(sql) > 0;
        }

        // All difficulty levels (id + name).
        public List<DifficultyLevel> GetDifficultyLevels()
        {
            var list = new List<DifficultyLevel>();
            using var conn = new MySqlConnection(connectionString);
            conn.Open();
            using var cmd = new MySqlCommand(
                "SELECT id, name FROM difficulty_levels ORDER BY id;", conn);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(new DifficultyLevel
                {
                    Id = rdr.GetInt32("id"),
                    Name = rdr.GetString("name")
                });
            }
            return list;
        }

        // All muscle‐group options.
        public List<MuscleGroup> GetMuscleGroups()
        {
            var list = new List<MuscleGroup>();
            using var conn = new MySqlConnection(connectionString);
            conn.Open();
            using var cmd = new MySqlCommand(
                "SELECT id, name FROM muscle_groups ORDER BY name;", conn);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(new MuscleGroup
                {
                    Id = rdr.GetInt32("id"),
                    Name = rdr.GetString("name")
                });
            }
            return list;
        }

        // Inserts a new exercise + its difficulty + muscle‐group links.
        public int InsertExercise(Exercise ex, int difficultyId, List<int> muscleGroupIds)
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open();

            // 1) insert into exercises
            using var cmd = new MySqlCommand(
                @"INSERT INTO exercises (name, description, difficulty_id)
              VALUES (@n, @d, @dl);", conn);
            cmd.Parameters.AddWithValue("@n", ex.Name);
            cmd.Parameters.AddWithValue("@d", ex.Description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@dl", difficultyId);
            cmd.ExecuteNonQuery();
            int newId = (int)cmd.LastInsertedId;

            // 2) link muscle groups
            foreach (var mg in muscleGroupIds)
            {
                using var link = new MySqlCommand(
                    @"INSERT INTO exercise_muscle_groups
                  (exercise_id, muscle_group_id)
                  VALUES (@e, @m);", conn);
                link.Parameters.AddWithValue("@e", newId);
                link.Parameters.AddWithValue("@m", mg);
                link.ExecuteNonQuery();
            }

            return newId;
        }


        // Updates an existing exercise and its muscle‐group links.
        public bool UpdateExercise(Exercise ex, int difficultyId, List<int> muscleGroupIds)
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open();

            // 1) update core row
            using var cmd = new MySqlCommand(
                @"UPDATE exercises
              SET name=@n, description=@d, difficulty_id=@dl
              WHERE id=@i;", conn);
            cmd.Parameters.AddWithValue("@n", ex.Name);
            cmd.Parameters.AddWithValue("@d", ex.Description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@dl", difficultyId);
            cmd.Parameters.AddWithValue("@i", ex.Id);
            cmd.ExecuteNonQuery();

            // 2) clear old links
            using var del = new MySqlCommand(
                "DELETE FROM exercise_muscle_groups WHERE exercise_id=@i;", conn);
            del.Parameters.AddWithValue("@i", ex.Id);
            del.ExecuteNonQuery();

            // 3) insert new links
            foreach (var mg in muscleGroupIds)
            {
                using var link = new MySqlCommand(
                    @"INSERT INTO exercise_muscle_groups
                  (exercise_id, muscle_group_id)
                  VALUES (@e, @m);", conn);
                link.Parameters.AddWithValue("@e", ex.Id);
                link.Parameters.AddWithValue("@m", mg);
                link.ExecuteNonQuery();
            }

            return true;
        }

        // Wizard/ Genereren van schema's:
        public int QuerySingleInt(string query)
        {
            using var conn = new MySqlConnection(connectionString);
            using var cmd = new MySqlCommand(query, conn);

            conn.Open();
            var result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : -1;
        }
        // 1. User preferences opslaan
        public int InsertUserPreferences(
            int userId,
            int goalId,
            int levelId,
            int availableDaysPerWeek,
            int sessionDurationMinutes,
            IEnumerable<int> muscleGroupIds)      // ← now ints
        {
            // 1) Insert the main preference record
            var prefQuery = $@"INSERT INTO user_preferences
            (id, user_id, goal_id, level_id, available_days_per_week, session_duration_minutes)
            VALUES (NULL, {userId}, {goalId}, {levelId}, {availableDaysPerWeek}, {sessionDurationMinutes});
             ";

            int prefId = Insert(prefQuery);
            if (prefId < 0) return -1;

            // 2) Insert each muscle_group link directly by its ID
            foreach (var mgId in muscleGroupIds)
            {
                // no lookup by name—mgId is already the PK
                var linkQuery = $@"
            INSERT INTO preference_muscle_groups
              (preference_id, muscle_group_id)
            VALUES
              ({prefId}, {mgId});
        ";
                Insert(linkQuery);
            }

            return prefId;
        }

    }
}
