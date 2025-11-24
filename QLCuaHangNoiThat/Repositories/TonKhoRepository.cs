using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace QLCuaHangNoiThat.Repositories
{
    public class TonKhoRepository
    {
        private readonly string _connectionString =
            "Server=localhost;Database=qlcuahangnoithat;Uid=root;Pwd=;";

        public int GetSoLuongTon(int maSanPham, int maKho, MySqlConnection conn = null, MySqlTransaction tran = null)
        {
            bool ownConnection = conn == null;
            if (conn == null)
            {
                conn = new MySqlConnection(_connectionString);
            }

            try
            {
                if (ownConnection) conn.Open();

                string q = @"SELECT SoLuong FROM TonKho WHERE MaSanPham = @SP AND MaKho = @Kho";
                var cmd = new MySqlCommand(q, conn, tran);
                cmd.Parameters.AddWithValue("@SP", maSanPham);
                cmd.Parameters.AddWithValue("@Kho", maKho);

                object result = cmd.ExecuteScalar();
                return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
            }
            finally
            {
                if (ownConnection && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void SetSoLuongTon(int maSanPham, int maKho, int soLuongMoi,
                                  MySqlConnection conn, MySqlTransaction tran)
        {
            string q = @"
                INSERT INTO TonKho (MaSanPham, MaKho, SoLuong)
                VALUES (@SP, @Kho, @SL)
                ON DUPLICATE KEY UPDATE SoLuong = @SL;";

            var cmd = new MySqlCommand(q, conn, tran);
            cmd.Parameters.AddWithValue("@SP", maSanPham);
            cmd.Parameters.AddWithValue("@Kho", maKho);
            cmd.Parameters.AddWithValue("@SL", soLuongMoi);
            cmd.ExecuteNonQuery();
        }

        public DataTable GetAllTonKho()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                string q = @"
            SELECT tk.MaTonKho, sp.MaSanPham, sp.TenSanPham, 
                   tk.SoLuong, 
                   FLOOR(tk.SoLuong * 0.3) as TonMin,  -- 30% của tồn hiện tại
                   k.TenKho
            FROM TonKho tk
            JOIN SanPham sp ON tk.MaSanPham = sp.MaSanPham
            JOIN Kho k ON tk.MaKho = k.MaKho
            ORDER BY tk.SoLuong ASC";

                var da = new MySqlDataAdapter(q, conn);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
