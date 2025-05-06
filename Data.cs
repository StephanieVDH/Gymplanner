using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
