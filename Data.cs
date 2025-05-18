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

        // Users:
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

    }
}
