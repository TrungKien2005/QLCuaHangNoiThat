using System.Data;
using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.Models;

namespace QLCuaHangNoiThat.Repositories
{
    public class KhoRepository
    {
        private readonly string connectionString =
            "Server=localhost;Database=qlcuahangnoithat;Uid=root;Pwd=;";

        // Lấy danh sách kho
        public DataTable GetAll()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Kho";
                var adapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // ============================
        //  THÊM KHO
        // ============================
        public bool ThemKho(Kho kho)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO Kho (TenKho, DiaChi, SoDienThoaiLienHe)
                    VALUES (@TenKho, @DiaChi, @SDT)";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenKho", kho.TenKho);
                cmd.Parameters.AddWithValue("@DiaChi", kho.DiaChi);
                cmd.Parameters.AddWithValue("@SDT", kho.SoDienThoaiLienHe);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ============================
        //  SỬA KHO
        // ============================
        public bool SuaKho(Kho kho)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string query = @"
                    UPDATE Kho SET 
                        TenKho=@TenKho,
                        DiaChi=@DiaChi,
                        SoDienThoaiLienHe=@SDT
                    WHERE MaKho=@MaKho";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKho", kho.MaKho);
                cmd.Parameters.AddWithValue("@TenKho", kho.TenKho);
                cmd.Parameters.AddWithValue("@DiaChi", kho.DiaChi);
                cmd.Parameters.AddWithValue("@SDT", kho.SoDienThoaiLienHe);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ============================
        //  XÓA KHO
        // ============================
        public bool XoaKho(int maKho)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM Kho WHERE MaKho=@MaKho";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKho", maKho);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
