using System;
using System.Data;
using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.Models;
using QLCuaHangNoiThat.DataAccess;

namespace QLCuaHangNoiThat.Repositories
{
    public class SanPhamRepository
    {
        public DataTable GetAll()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            sp.MaSanPham,
                            sp.TenSanPham, 
                            sp.GiaBan,
                            sp.SoLuongTon,
                            sp.MoTa,
                            dm.TenDanhMuc,
                            sp.SKU
                        FROM sanpham sp
                        LEFT JOIN danhmuc dm ON sp.MaDanhMuc = dm.MaDanhMuc
                        ORDER BY sp.MaSanPham";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi lấy dữ liệu sản phẩm: {ex.Message}");
                }
            }
            return dt;
        }

        public bool Add(SanPham sp)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"INSERT INTO sanpham 
                        (TenSanPham, MaDanhMuc, SKU, MoTa, GiaNhap, GiaBan, SoLuongTon, DangKinhDoanh) 
                        VALUES (@TenSanPham, @MaDanhMuc, @SKU, @MoTa, @GiaNhap, @GiaBan, @SoLuongTon, 1)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@TenSanPham", sp.TenSanPham);
                    cmd.Parameters.AddWithValue("@MaDanhMuc", sp.MaDanhMuc);
                    cmd.Parameters.AddWithValue("@SKU", GenerateSKU(sp.TenSanPham));
                    cmd.Parameters.AddWithValue("@MoTa", sp.MoTa);
                    cmd.Parameters.AddWithValue("@GiaNhap", sp.GiaNhap);
                    cmd.Parameters.AddWithValue("@GiaBan", sp.GiaBan);
                    cmd.Parameters.AddWithValue("@SoLuongTon", sp.SoLuongTon);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 1062)
                        throw new Exception("SKU sản phẩm đã tồn tại!");
                    else
                        throw new Exception($"Lỗi khi thêm sản phẩm: {ex.Message}");
                }
            }
        }

        public bool Update(SanPham sp)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"UPDATE sanpham 
                                   SET TenSanPham = @TenSanPham, 
                                       GiaBan = @GiaBan, 
                                       SoLuongTon = @SoLuongTon,
                                       MoTa = @MoTa,
                                       MaDanhMuc = @MaDanhMuc
                                   WHERE MaSanPham = @MaSanPham";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@MaSanPham", sp.MaSanPham);
                    cmd.Parameters.AddWithValue("@TenSanPham", sp.TenSanPham);
                    cmd.Parameters.AddWithValue("@GiaBan", sp.GiaBan);
                    cmd.Parameters.AddWithValue("@SoLuongTon", sp.SoLuongTon);
                    cmd.Parameters.AddWithValue("@MoTa", sp.MoTa);
                    cmd.Parameters.AddWithValue("@MaDanhMuc", sp.MaDanhMuc);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi cập nhật sản phẩm: {ex.Message}");
                }
            }
        }

        public bool Delete(string maSP)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = "DELETE FROM sanpham WHERE MaSanPham = @MaSanPham";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSanPham", int.Parse(maSP));

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi xóa sản phẩm: {ex.Message}");
                }
            }
        }

        public DataTable Search(string keyword)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            sp.MaSanPham,
                            sp.TenSanPham,
                            sp.GiaBan,
                            sp.SoLuongTon,
                            sp.MoTa,
                            dm.TenDanhMuc
                        FROM sanpham sp
                        LEFT JOIN danhmuc dm ON sp.MaDanhMuc = dm.MaDanhMuc
                        WHERE sp.TenSanPham LIKE @Keyword 
                           OR dm.TenDanhMuc LIKE @Keyword
                           OR sp.MoTa LIKE @Keyword
                        ORDER BY sp.MaSanPham";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi tìm kiếm sản phẩm: {ex.Message}");
                }
            }
            return dt;
        }

        private string GenerateSKU(string tenSP)
        {
            return "SP" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}