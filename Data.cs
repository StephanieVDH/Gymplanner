using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        // Users --> Profile Window:
        public Gymplanner.CS.User GetUserByEmailForProfile(string email)
        {
            Gymplanner.CS.User user = null;
            string query = "SELECT id, username, email, created_at, updated_at FROM users WHERE email = @email";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@email", email);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new Gymplanner.CS.User
                                {
                                    Id = reader.GetInt32("id"),
                                    Username = reader.GetString("username"),
                                    Email = reader.GetString("email"),
                                    CreatedAt = reader.GetDateTime("created_at"),
                                    UpdatedAt = reader.GetDateTime("updated_at")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting user by email: {ex.Message}");
                Console.WriteLine($"Error getting user by email: {ex.Message}");
            }

            return user;
        }

        // Gets user preferences with goal and fitness level names
        public Gymplanner.CS.User.UserPreferences GetUserPreferences(int userId)
        {
            Gymplanner.CS.User.UserPreferences preferences = null;
            string query = @"
                SELECT up.id, up.user_id, g.name as goal_name, fl.name as fitness_level, 
                       up.available_days_per_week, up.session_duration_minutes, up.created_at
                FROM user_preferences up
                JOIN goals g ON up.goal_id = g.id
                JOIN fitness_levels fl ON up.level_id = fl.id
                WHERE up.user_id = @userId
                ORDER BY up.created_at DESC
                LIMIT 1";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                preferences = new Gymplanner.CS.User.UserPreferences
                                {
                                    Id = reader.GetInt32("id"),
                                    UserId = reader.GetInt32("user_id"),
                                    GoalName = reader.GetString("goal_name"),
                                    FitnessLevel = reader.GetString("fitness_level"),
                                    AvailableDaysPerWeek = reader.GetInt32("available_days_per_week"),
                                    SessionDurationMinutes = reader.GetInt32("session_duration_minutes"),
                                    CreatedAt = reader.GetDateTime("created_at")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting user preferences: {ex.Message}");
                Console.WriteLine($"Error getting user preferences: {ex.Message}");
            }

            return preferences;
        }

        // Gets complete user profile with all related data
        public Gymplanner.CS.User.UserProfile GetCompleteUserProfile(string email)
        {
            var user = GetUserByEmailForProfile(email);
            if (user == null) return null;

            var preferences = GetUserPreferences(user.Id);

            return new Gymplanner.CS.User.UserProfile
            {
                User = user,
                Preferences = preferences,
            };
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
                    Role = reader.GetString("role"),       
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

        public bool UpdateUserPassword(int userId, string newPasswordHash)
        {
            string query = "UPDATE users SET password_hash = @passwordHash, updated_at = NOW() WHERE id = @userId";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@passwordHash", newPasswordHash);
                        command.Parameters.AddWithValue("@userId", userId);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating password: {ex.Message}");
                return false;
            }
        }


        // Verify current password before allowing reset
        public bool VerifyCurrentPassword(int userId, string currentPasswordHash)
        {
            string query = "SELECT password_hash FROM users WHERE id = @userId AND deleted_at IS NULL";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);

                        var storedHash = command.ExecuteScalar() as string;
                        return storedHash != null && storedHash == currentPasswordHash;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error verifying password: {ex.Message}");
                return false;
            }
        }

        // Stores the given file-path into users.picture (VARCHAR).
        public bool UpdateUserProfilePicture(int userId, string picturePath)
        {
            const string sql = @"
                UPDATE users
                   SET picture    = @path,
                       updated_at = NOW()
                 WHERE id         = @userId;
            ";

            try
            {
                using var conn = new MySqlConnection(connectionString);
                conn.Open();
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add("@path", MySqlDbType.VarChar).Value = picturePath;
                cmd.Parameters.Add("@userId", MySqlDbType.Int32).Value = userId;

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving profile picture path: {ex}");
                return false;
            }
        }

        // Retrieves the picture path (VARCHAR) for the given user, or null if none set.
        public string? GetUserProfilePicture(int userId)
        {
            const string sql = @"
                SELECT picture
                  FROM users
                 WHERE id = @userId
                 LIMIT 1;
            ";

            using var conn = new MySqlConnection(connectionString);
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userId", userId);

            conn.Open();
            var result = cmd.ExecuteScalar();
            if (result == null || result == DBNull.Value)
                return null;

            return result.ToString();
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

        // Oefeningen toevoegen als admin:
        public int InsertExercise(Exercise ex, int difficultyId, List<int> muscleGroupIds)
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open();

            // Insert core row
            using var cmd = new MySqlCommand(
              @"INSERT INTO exercises 
         (name, description, difficulty_id)
        VALUES(@n,@d,@dl);", conn);
            cmd.Parameters.AddWithValue("@n", ex.Name);
            cmd.Parameters.AddWithValue("@d", ex.Description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@dl", difficultyId);
            cmd.ExecuteNonQuery();
            int newId = (int)cmd.LastInsertedId;

            // Link M–M
            foreach (var mgId in muscleGroupIds)
            {
                using var link = new MySqlCommand(
                  @"INSERT INTO exercise_muscle_groups
             (exercise_id, muscle_group_id)
            VALUES(@e,@m);", conn);
                link.Parameters.AddWithValue("@e", newId);
                link.Parameters.AddWithValue("@m", mgId);
                link.ExecuteNonQuery();
            }

            return newId;
        }

        public bool UpdateExercise(Exercise ex, int difficultyId, List<int> muscleGroupIds)
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open();

            // 1) update core
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
            foreach (var mgId in muscleGroupIds)
            {
                using var link = new MySqlCommand(
                  @"INSERT INTO exercise_muscle_groups
             (exercise_id, muscle_group_id)
            VALUES(@e,@m);", conn);
                link.Parameters.AddWithValue("@e", ex.Id);
                link.Parameters.AddWithValue("@m", mgId);
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
