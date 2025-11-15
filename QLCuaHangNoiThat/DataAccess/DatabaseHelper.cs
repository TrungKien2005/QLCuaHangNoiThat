using System;
using MySql.Data.MySqlClient;

namespace QLCuaHangNoiThat.DataAccess
{
    public static class DatabaseHelper
    {
        private static readonly string connectionString =
            "Server=localhost;Port=3306;Database=qlcuahangnoithat;" +
            "User ID=root;Password=;SslMode=Disabled;" +
            "Charset=utf8mb4;ConnectionTimeout=30;Pooling=true;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection test failed: {ex.Message}");
                return false;
            }
        }

        public static int ExecuteNonQuery(string query, MySqlParameter[] parameters = null)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);
                    return command.ExecuteNonQuery();
                }
            }
        }

        public static object ExecuteScalar(string query, MySqlParameter[] parameters = null)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);
                    return command.ExecuteScalar();
                }
            }
        }
    }
}