using System.Data;
using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.Database;
using QLCuaHangNoiThat.Models;

namespace QLCuaHangNoiThat.Repositories
{
    public class SanPhamRepository
    {
        // 📋 Lấy toàn bộ danh sách sản phẩm
        public DataTable GetAll()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = DbConnection.GetConnection()) // ✅ gọi static
            {
                conn.Open();
                string query = "SELECT MaSP, TenSP, Gia, SoLuong, DanhMuc, MoTa FROM SanPham";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        // ➕ Thêm sản phẩm mới
        public bool Add(SanPham sp)
        {
            using (MySqlConnection conn = DbConnection.GetConnection()) // ✅ static
            {
                conn.Open();
                string query = @"INSERT INTO SanPham (MaSP, TenSP, Gia, SoLuong, DanhMuc, MoTa)
                                 VALUES (@MaSP, @TenSP, @Gia, @SoLuong, @DanhMuc, @MoTa)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSP", sp.MaSP);
                    cmd.Parameters.AddWithValue("@TenSP", sp.TenSP);
                    cmd.Parameters.AddWithValue("@Gia", sp.Gia);
                    cmd.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                    cmd.Parameters.AddWithValue("@DanhMuc", sp.DanhMuc);
                    cmd.Parameters.AddWithValue("@MoTa", sp.MoTa);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // ✏️ Cập nhật sản phẩm
        public bool Update(SanPham sp)
        {
            using (MySqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();
                string query = @"UPDATE SanPham 
                                 SET TenSP=@TenSP, Gia=@Gia, SoLuong=@SoLuong, 
                                     DanhMuc=@DanhMuc, MoTa=@MoTa 
                                 WHERE MaSP=@MaSP";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSP", sp.MaSP);
                    cmd.Parameters.AddWithValue("@TenSP", sp.TenSP);
                    cmd.Parameters.AddWithValue("@Gia", sp.Gia);
                    cmd.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                    cmd.Parameters.AddWithValue("@DanhMuc", sp.DanhMuc);
                    cmd.Parameters.AddWithValue("@MoTa", sp.MoTa);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // ❌ Xóa sản phẩm
        public bool Delete(string maSP)
        {
            using (MySqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM SanPham WHERE MaSP=@MaSP";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSP", maSP);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
