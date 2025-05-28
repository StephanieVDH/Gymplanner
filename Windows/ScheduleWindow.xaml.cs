using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using MySqlConnector;
using GymPlanner;

namespace GymPlanner.Windows
{
    public partial class ScheduleWindow : Window
    {
        public IEnumerable<DaySchedule> Days { get; set; }

        public ScheduleWindow(int userId)
        {
            InitializeComponent();
            DataContext = this;
            LoadSchedule(userId);
        }

        private void LoadSchedule(int userId)
        {
            string connStr =
               "datasource=127.0.0.1;" +
               "port=3306;" +
               "username=root;password=;" +
               "database=gymplanner;";
            using var conn = new MySqlConnection(connStr);
            conn.Open();

            const string sql = @"
SELECT sd.day_of_week_id AS DayOfWeek,
       e.name            AS Name,
       si.sets           AS Sets,
       si.reps           AS Reps
FROM schedules s
JOIN schedule_days sd  ON s.id = sd.schedule_id
JOIN schedule_items si ON si.schedule_day_id = sd.id
JOIN exercises e       ON e.id = si.exercise_id
WHERE s.user_id = @uid
  AND s.id = (SELECT MAX(id) FROM schedules WHERE user_id = @uid)
ORDER BY sd.day_of_week_id;";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uid", userId);

            var tempList = new List<DayScheduleTemp>();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tempList.Add(new DayScheduleTemp
                {
                    DayOfWeek = reader.GetInt32("DayOfWeek"),
                    Name = reader.GetString("Name"),
                    Sets = reader.GetInt32("Sets"),
                    Reps = reader.GetInt32("Reps")
                });
            }

            var grouped = tempList
                .GroupBy(x => x.DayOfWeek)
                .Select(g => new DaySchedule
                {
                    DayName = ((DayOfWeek)g.Key).ToString(),
                    Exercises = g.Select(x => new ExerciseEntry
                    {
                        Name = x.Name,
                        Sets = x.Sets,
                        Reps = x.Reps
                    }).ToList()
                })
                .ToList();

            Days = grouped;
        }
    }

    internal class DayScheduleTemp
    {
        public int DayOfWeek;
        public string Name;
        public int Sets;
        public int Reps;
    }

    public class DaySchedule
    {
        public string DayName { get; set; }
        public List<ExerciseEntry> Exercises { get; set; }
    }

    public class ExerciseEntry
    {
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
    }
}

