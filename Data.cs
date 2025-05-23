using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                @"SELECT id, username, email, role, created_at 
                  FROM users;", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new User  // dit nog aanpassen
                {
                    ID = reader.GetInt32("id"),
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
            // Delete(...) returns the row-count, so >0 means “we deleted something”
            return Delete(sql) > 0;
        }


        // EXERCISES:
        public List<Exercise> GetExercises()
        {
            var list = new List<Exercise>();
            using var conn = new MySqlConnection(connectionString);
            conn.Open();

            using var cmd = new MySqlCommand(
                @"SELECT 
              id,
              name,
              description,
              muscle_group
          FROM exercises;", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Exercise
                {
                    ID = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Description = reader.GetString("description"),
                    MuscleGroup = reader.GetString("muscle_group")
                });
            }

            return list;
        }

        // Exercise toevoegen:
        public int InsertExercise(Exercise exercise)
        {
            string query =
                $"INSERT INTO exercises " +
                $"(id, name, description, muscle_group, difficulty) " +
                $"VALUES (NULL, " +
                $"'{exercise.Name}', " +
                $"'{exercise.Description}', " +
                $"'{exercise.MuscleGroup}', " +
                $"'{exercise.Difficulty}');";

            return Insert(query);
        }

        // Exercise verwijderen:
        public bool DeleteExercise(int exerciseId)
        {
            string sql = $"DELETE FROM exercises WHERE id = {exerciseId};";
            return Delete(sql) > 0;
        }


    }
}
