using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.Models;

namespace QLCuaHangNoiThat.Repositories
{
    public class KhoRepository
    {
        private string connectionString =
            "Server=localhost;Database=qlcuahangnoithat;Uid=root;Pwd=;";

        public DataTable GetAll()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Kho";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void Insert(Kho kho)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = @"INSERT INTO Kho (TenKho, DiaChi, SoDienThoaiLienHe)
                                 VALUES (@TenKho, @DiaChi, @SDT)";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenKho", kho.TenKho);
                cmd.Parameters.AddWithValue("@DiaChi", kho.DiaChi);
                cmd.Parameters.AddWithValue("@SDT", kho.SoDienThoaiLienHe);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Kho kho)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = @"UPDATE Kho SET 
                                    TenKho=@TenKho, 
                                    DiaChi=@DiaChi, 
                                    SoDienThoaiLienHe=@SDT
                                 WHERE MaKho=@MaKho";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKho", kho.MaKho);
                cmd.Parameters.AddWithValue("@TenKho", kho.TenKho);
                cmd.Parameters.AddWithValue("@DiaChi", kho.DiaChi);
                cmd.Parameters.AddWithValue("@SDT", kho.SoDienThoaiLienHe);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int maKho)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM Kho WHERE MaKho=@MaKho";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKho", maKho);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
