using MySql.Data.MySqlClient;
using System;
using System.Data;
using QLCuaHangNoiThat.Models;

namespace QLCuaHangNoiThat.Repositories
{
    public class PhieuNhapKhoRepository
    {
        private string connectionString = "Server=localhost;Database=qlcuahangnoithat;Uid=root;Pwd=;";

        /// <summary>
        /// Lấy danh sách tất cả phiếu nhập kho
        /// </summary>
        public DataTable GetAll()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string query = @"SELECT * FROM PhieuNhapKho ORDER BY NgayNhap DESC";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        /// <summary>
        /// Thêm phiếu nhập kho mới và trả về ID mới tạo.
        /// </summary>
        public int Insert(PhieuNhapKho p)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string q = @"
                    INSERT INTO PhieuNhapKho 
                    (MaNhaCungCap, MaNhanVien, MaKho, NgayNhap, TongTien, GhiChu)
                    VALUES 
                    (@NCC, @NV, @Kho, @Ngay, @Tong, @GhiChu);
                    SELECT LAST_INSERT_ID();";

                MySqlCommand cmd = new MySqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@NCC", p.MaNhaCungCap);
                cmd.Parameters.AddWithValue("@NV", p.MaNhanVien);
                cmd.Parameters.AddWithValue("@Kho", p.MaKho);
                cmd.Parameters.AddWithValue("@Ngay", p.NgayNhap);
                cmd.Parameters.AddWithValue("@Tong", p.TongTien);
                cmd.Parameters.AddWithValue("@GhiChu", p.GhiChu);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        /// <summary>
        /// Lấy phiếu nhập theo ID
        /// </summary>
        public PhieuNhapKho GetById(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM PhieuNhapKho WHERE MaPhieuNhap = @Id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new PhieuNhapKho
                    {
                        MaPhieuNhap = reader.GetInt32("MaPhieuNhap"),
                        MaNhaCungCap = reader.GetInt32("MaNhaCungCap"),
                        MaNhanVien = reader.GetInt32("MaNhanVien"),
                        MaKho = reader.GetInt32("MaKho"),
                        NgayNhap = reader.GetDateTime("NgayNhap"),
                        TongTien = (double)reader.GetDecimal("TongTien"),
                        GhiChu = reader["GhiChu"]?.ToString()
                    };
                }

                return null;
            }
        }

        /// <summary>
        /// Xóa phiếu nhập
        /// </summary>
        public bool Delete(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM PhieuNhapKho WHERE MaPhieuNhap = @Id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
