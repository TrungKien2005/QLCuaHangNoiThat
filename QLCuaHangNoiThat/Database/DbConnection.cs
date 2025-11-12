using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace QLCuaHangNoiThat.Database
{
    public static class DbConnection
    {
        private static readonly string connectionString =
            "Server=localhost;Port=3306;Database=qlcuahangnoithat;User ID=root;Password=;SslMode=Disabled;";
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}