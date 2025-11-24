using System;
using System.Data;
using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.Models;

namespace QLCuaHangNoiThat.Repositories
{
    public class PhieuXuatKhoRepository
    {
        private readonly string _connectionString =
            "Server=localhost;Database=qlcuahangnoithat;Uid=root;Pwd=;";

        public DataTable GetAll()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM PhieuXuatKho ORDER BY NgayXuat DESC";
                var da = new MySqlDataAdapter(query, conn);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public int Insert(PhieuXuatKho p, MySqlConnection externalConn = null, MySqlTransaction tran = null)
        {
            bool ownConnection = externalConn == null;
            var conn = externalConn ?? new MySqlConnection(_connectionString);

            try
            {
                if (ownConnection)
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                }

                string q = @"
                    INSERT INTO PhieuXuatKho
                    (MaKhachHang, MaNhanVien, MaKho, NgayXuat, TongTien, GhiChu)
                    VALUES
                    (@KH, @NV, @Kho, @Ngay, @Tong, @GhiChu);
                    SELECT LAST_INSERT_ID();";

                var cmd = new MySqlCommand(q, conn, tran);
                cmd.Parameters.AddWithValue("@KH", p.MaKhachHang);
                cmd.Parameters.AddWithValue("@NV", p.MaNhanVien);
                cmd.Parameters.AddWithValue("@Kho", p.MaKho);
                cmd.Parameters.AddWithValue("@Ngay", p.NgayXuat);
                cmd.Parameters.AddWithValue("@Tong", p.TongTien);
                cmd.Parameters.AddWithValue("@GhiChu", p.GhiChu);

                int newId = Convert.ToInt32(cmd.ExecuteScalar());

                if (ownConnection)
                    tran.Commit();

                return newId;
            }
            catch
            {
                if (externalConn == null && tran != null)
                    tran.Rollback();
                throw;
            }
            finally
            {
                if (externalConn == null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}
