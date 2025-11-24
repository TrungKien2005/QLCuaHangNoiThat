using System;
using System.Data;
using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.DataAccess;
using QLCuaHangNoiThat.Models;

namespace QLCuaHangNoiThat.Repositories
{
    public class SanPhamRepository
    {
        // =============================
        // LẤY TẤT CẢ SẢN PHẨM VỚI DANH MỤC VÀ TRẠNG THÁI KINH DOANH
        // =============================
        public DataTable GetAllWithDanhMuc()
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            sp.MaSanPham as MaSP,
                            sp.TenSanPham as TenSP,
                            dm.TenDanhMuc as DanhMuc,
                            sp.GiaBan as Gia,
                            sp.SoLuongTon as SoLuong,
                            sp.MoTa,
                            sp.DangKinhDoanh as DangKinhDoanh,
                            sp.HinhAnhURL as HinhAnhURL,
                            sp.SKU as SKU,
                            CASE 
                                WHEN sp.DangKinhDoanh = 0 THEN 'Ngừng kinh doanh'
                                WHEN sp.SoLuongTon > 0 THEN 'Còn hàng'
                                ELSE 'Hết hàng'
                            END as TinhTrang
                        FROM sanpham sp
                        LEFT JOIN danhmuc dm ON sp.MaDanhMuc = dm.MaDanhMuc
                        ORDER BY sp.MaSanPham";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi lấy dữ liệu sản phẩm: {ex.Message}");
                }
            }
            return dt;
        }

        // =============================
        // THÊM SẢN PHẨM MỚI
        // =============================
        public bool AddSanPham(SanPham sp)
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO sanpham 
                                    (TenSanPham, MaDanhMuc, GiaBan, SoLuongTon, MoTa, HinhAnhURL, DangKinhDoanh, SKU)
                                    VALUES (@TenSanPham, @MaDanhMuc, @GiaBan, @SoLuongTon, @MoTa, @HinhAnhURL, @DangKinhDoanh, @SKU)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenSanPham", sp.TenSanPham);
                        cmd.Parameters.AddWithValue("@MaDanhMuc", sp.MaDanhMuc);
                        cmd.Parameters.AddWithValue("@GiaBan", sp.GiaBan);
                        cmd.Parameters.AddWithValue("@SoLuongTon", sp.SoLuongTon);
                        cmd.Parameters.AddWithValue("@MoTa", sp.MoTa ?? "");
                        cmd.Parameters.AddWithValue("@HinhAnhURL", sp.HinhAnhURL ?? "");
                        cmd.Parameters.AddWithValue("@DangKinhDoanh", sp.DangKinhDoanh);
                        cmd.Parameters.AddWithValue("@SKU", sp.SKU ?? GenerateSKU());

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm sản phẩm: {ex.Message}");
            }
        }

        // =============================
        // CẬP NHẬT SẢN PHẨM
        // =============================
        public bool UpdateSanPham(SanPham sp)
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE sanpham 
                                    SET TenSanPham = @TenSanPham, 
                                        MaDanhMuc = @MaDanhMuc, 
                                        GiaBan = @GiaBan, 
                                        SoLuongTon = @SoLuongTon, 
                                        MoTa = @MoTa, 
                                        HinhAnhURL = @HinhAnhURL, 
                                        DangKinhDoanh = @DangKinhDoanh,
                                        SKU = @SKU
                                    WHERE MaSanPham = @MaSanPham";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSanPham", sp.MaSanPham);
                        cmd.Parameters.AddWithValue("@TenSanPham", sp.TenSanPham);
                        cmd.Parameters.AddWithValue("@MaDanhMuc", sp.MaDanhMuc);
                        cmd.Parameters.AddWithValue("@GiaBan", sp.GiaBan);
                        cmd.Parameters.AddWithValue("@SoLuongTon", sp.SoLuongTon);
                        cmd.Parameters.AddWithValue("@MoTa", sp.MoTa ?? "");
                        cmd.Parameters.AddWithValue("@HinhAnhURL", sp.HinhAnhURL ?? "");
                        cmd.Parameters.AddWithValue("@DangKinhDoanh", sp.DangKinhDoanh);
                        cmd.Parameters.AddWithValue("@SKU", sp.SKU ?? "");

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật sản phẩm: {ex.Message}");
            }
        }

        // =============================
        // XÓA SẢN PHẨM
        // =============================
        public bool DeleteSanPham(int maSanPham)
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM sanpham WHERE MaSanPham = @MaSanPham";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa sản phẩm: {ex.Message}");
            }
        }

        // =============================
        // TÌM KIẾM SẢN PHẨM
        // =============================
        public DataTable SearchSanPham(string keyword)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            sp.MaSanPham as MaSP,
                            sp.TenSanPham as TenSP,
                            dm.TenDanhMuc as DanhMuc,
                            sp.GiaBan as Gia,
                            sp.SoLuongTon as SoLuong,
                            sp.MoTa,
                            sp.DangKinhDoanh as DangKinhDoanh,
                            sp.HinhAnhURL as HinhAnhURL,
                            sp.SKU as SKU,
                            CASE 
                                WHEN sp.DangKinhDoanh = 0 THEN 'Ngừng kinh doanh'
                                WHEN sp.SoLuongTon > 0 THEN 'Còn hàng'
                                ELSE 'Hết hàng'
                            END as TinhTrang
                        FROM sanpham sp
                        LEFT JOIN danhmuc dm ON sp.MaDanhMuc = dm.MaDanhMuc
                        WHERE sp.TenSanPham LIKE @Keyword 
                           OR sp.MoTa LIKE @Keyword 
                           OR sp.SKU LIKE @Keyword
                           OR dm.TenDanhMuc LIKE @Keyword
                        ORDER BY sp.MaSanPham";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@Keyword", $"%{keyword}%");
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi tìm kiếm sản phẩm: {ex.Message}");
                }
            }
            return dt;
        }

        // =============================
        // LỌC THEO DANH MỤC
        // =============================
        public DataTable FilterByDanhMuc(string tenDanhMuc)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            sp.MaSanPham as MaSP,
                            sp.TenSanPham as TenSP,
                            dm.TenDanhMuc as DanhMuc,
                            sp.GiaBan as Gia,
                            sp.SoLuongTon as SoLuong,
                            sp.MoTa,
                            sp.DangKinhDoanh as DangKinhDoanh,
                            sp.HinhAnhURL as HinhAnhURL,
                            sp.SKU as SKU,
                            CASE 
                                WHEN sp.DangKinhDoanh = 0 THEN 'Ngừng kinh doanh'
                                WHEN sp.SoLuongTon > 0 THEN 'Còn hàng'
                                ELSE 'Hết hàng'
                            END as TinhTrang
                        FROM sanpham sp
                        LEFT JOIN danhmuc dm ON sp.MaDanhMuc = dm.MaDanhMuc
                        WHERE dm.TenDanhMuc = @TenDanhMuc
                        ORDER BY sp.MaSanPham";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@TenDanhMuc", tenDanhMuc);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi lọc theo danh mục: {ex.Message}");
                }
            }
            return dt;
        }

        // =============================
        // LẤY SẢN PHẨM THEO ID
        // =============================
        // Trong SanPhamRepository.cs - Đảm bảo phương thức GetById hoạt động
        public SanPham GetById(int maSanPham)
        {
            SanPham sp = null;

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = "SELECT * FROM sanpham WHERE MaSanPham = @MaSanPham";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                sp = new SanPham
                                {
                                    MaSanPham = reader.GetInt32("MaSanPham"),
                                    TenSanPham = reader.GetString("TenSanPham"),
                                    MaDanhMuc = reader.GetInt32("MaDanhMuc"),
                                    GiaBan = reader.GetDecimal("GiaBan"),
                                    SoLuongTon = reader.GetInt32("SoLuongTon"),
                                    MoTa = reader.IsDBNull(reader.GetOrdinal("MoTa")) ? "" : reader.GetString("MoTa"),
                                    HinhAnhURL = reader.IsDBNull(reader.GetOrdinal("HinhAnhURL")) ? "" : reader.GetString("HinhAnhURL"),
                                    DangKinhDoanh = reader.GetBoolean("DangKinhDoanh"),
                                    SKU = reader.IsDBNull(reader.GetOrdinal("SKU")) ? "" : reader.GetString("SKU")
                                };

                                Console.WriteLine($"Đã load sản phẩm: {sp.TenSanPham}"); // DEBUG
                            }
                            else
                            {
                                Console.WriteLine($"Không tìm thấy sản phẩm với mã: {maSanPham}"); // DEBUG
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi GetById: {ex.Message}"); // DEBUG
                    throw new Exception($"Lỗi khi lấy thông tin sản phẩm: {ex.Message}");
                }
            }
            return sp;
        }

        // =============================
        // CẬP NHẬT TRẠNG THÁI KINH DOANH
        // =============================
        public bool UpdateTrangThaiKinhDoanh(int maSanPham, bool dangKinhDoanh)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = "UPDATE sanpham SET DangKinhDoanh = @DangKinhDoanh WHERE MaSanPham = @MaSanPham";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);
                    cmd.Parameters.AddWithValue("@DangKinhDoanh", dangKinhDoanh);

                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi cập nhật trạng thái kinh doanh: {ex.Message}");
                }
            }
        }

        // =============================
        // LẤY TẤT CẢ DANH MỤC
        // =============================
        public DataTable GetAllDanhMuc()
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = "SELECT MaDanhMuc, TenDanhMuc FROM danhmuc ORDER BY TenDanhMuc";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi lấy danh mục: {ex.Message}");
                }
            }
            return dt;
        }

        // =============================
        // TẠO SKU TỰ ĐỘNG
        // =============================
        public string GenerateSKU()
        {
            return "SP" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        // =============================
        // KIỂM TRA SKU TRÙNG
        // =============================
        public bool IsSKUExists(string sku, int excludeMaSP = 0)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM sanpham WHERE SKU = @SKU AND MaSanPham != @ExcludeMaSP";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SKU", sku);
                        cmd.Parameters.AddWithValue("@ExcludeMaSP", excludeMaSP);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi kiểm tra SKU: {ex.Message}");
                }
            }
        }

        // =============================
        // CẬP NHẬT SỐ LƯỢNG TỒN
        // =============================
        public bool UpdateSoLuongTon(int maSanPham, int soLuong)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = "UPDATE sanpham SET SoLuongTon = @SoLuongTon WHERE MaSanPham = @MaSanPham";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);
                    cmd.Parameters.AddWithValue("@SoLuongTon", soLuong);

                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi cập nhật số lượng tồn: {ex.Message}");
                }
            }
        }

        // =============================
        // LẤY SẢN PHẨM ĐANG KINH DOANH
        // =============================
        public DataTable GetSanPhamDangKinhDoanh()
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            sp.MaSanPham as MaSP,
                            sp.TenSanPham as TenSP,
                            dm.TenDanhMuc as DanhMuc,
                            sp.GiaBan as Gia,
                            sp.SoLuongTon as SoLuong,
                            sp.MoTa,
                            sp.HinhAnhURL as HinhAnhURL,
                            sp.SKU as SKU
                        FROM sanpham sp
                        LEFT JOIN danhmuc dm ON sp.MaDanhMuc = dm.MaDanhMuc
                        WHERE sp.DangKinhDoanh = 1 AND sp.SoLuongTon > 0
                        ORDER BY sp.TenSanPham";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi lấy sản phẩm đang kinh doanh: {ex.Message}");
                }
            }
            return dt;
        }
        // =============================
// KIỂM TRA SẢN PHẨM CÓ ĐANG ĐƯỢC SỬ DỤNG
// =============================
public bool IsSanPhamInUse(int maSanPham)
{
    using (MySqlConnection conn = DatabaseHelper.GetConnection())
    {
        try
        {
            conn.Open();

            // Kiểm tra trong bảng chitietdonhang (hoặc các bảng liên quan)
            string query = @"
                SELECT COUNT(*) FROM chitietdonhang 
                WHERE MaSanPham = @MaSanPham
                UNION ALL
                SELECT COUNT(*) FROM chitietphieunhap 
                WHERE MaSanPham = @MaSanPham";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);
            
            int totalCount = 0;
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    totalCount += reader.GetInt32(0);
                }
            }
            
            return totalCount > 0;
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi khi kiểm tra sản phẩm đang sử dụng: {ex.Message}");
        }
    }
}
    }
}