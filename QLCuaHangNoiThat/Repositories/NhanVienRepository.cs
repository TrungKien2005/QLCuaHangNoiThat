using MySql.Data.MySqlClient;
using System.Collections.Generic;
using QLCuaHangNoiThat.DataAccess;
using System;
using System.Data;
using QLCuaHangNoiThat.Models;

namespace QLCuaHangNoiThat.Repositories
{
    public class NhanVienRepository
    {
        // ------------------
        // 1. TẢI DỮ LIỆU (READ)
        // ------------------
        public List<NhanVien> GetAllNhanVien()
        {
            List<NhanVien> list = new List<NhanVien>();

            // Sử dụng GetConnection() từ DatabaseHelper
            using (MySqlConnection connection = DatabaseHelper.GetConnection())
            {
                string query = @"
                    SELECT nv.MaNhanVien, nv.MaChucVu, nv.Ho, nv.Ten, nv.Email, 
                           nv.SoDienThoai, nv.NgaySinh, nv.DiaChi, nv.NgayVaoLam,
                           nv.TrangThai, cv.TenChucVu
                    FROM NhanVien nv
                    JOIN ChucVu cv ON nv.MaChucVu = cv.MaChucVu";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                connection.Open(); // Mở kết nối ở đây

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new NhanVien
                        {
                            MaNhanVien = reader.GetInt32("MaNhanVien"),
                            MaChucVu = reader.GetInt32("MaChucVu"),
                            Ho = reader.GetString("Ho"),
                            Ten = reader.GetString("Ten"),
                            Email = reader.GetString("Email"),
                            SoDienThoai = reader.GetString("SoDienThoai"),

                            // Xử lý NgaySinh (Sử dụng IsDBNull)
                            NgaySinh = reader.IsDBNull(reader.GetOrdinal("NgaySinh"))
                                     ? (DateTime?)null
                                     : reader.GetDateTime("NgaySinh"),

                            // Xử lý DiaChi (Sử dụng IsDBNull)
                            DiaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi"))
                                   ? null // Gán null cho string
                                   : reader.GetString("DiaChi"),

                            // Xử lý NgayVaoLam (Sử dụng IsDBNull)
                            NgayVaoLam = reader.IsDBNull(reader.GetOrdinal("NgayVaoLam"))
                                             ? (DateTime?)null
                                             : reader.GetDateTime("NgayVaoLam"),

                            TrangThai = reader.GetString("TrangThai"),
                            TenChucVu = reader.GetString("TenChucVu")
                        });
                    }
                }
            } // Kết nối tự động đóng khi ra khỏi khối using
            return list;
        }

        // ------------------
        // 2. THÊM DỮ LIỆU (CREATE) - ĐÃ CẬP NHẬT CHỮ KÝ HÀM VÀ KHẮC PHỤC LỖI GÁN CỨNG
        // ------------------
        public int AddNhanVien(NhanVien nv, string tenDangNhap, string matKhau, string loaiTaiKhoan)
        {
            int maNV = -1;

            using (MySqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    // Lệnh 1: Thêm Nhân viên và lấy ID mới
                    string queryNV = @"INSERT INTO NhanVien (MaChucVu, Ho, Ten, Email, SoDienThoai, NgaySinh, DiaChi, NgayVaoLam, TrangThai) 
                                       VALUES (@MaChucVu, @Ho, @Ten, @Email, @SDT, @NS, @DC, @NVL, @TrangThai);
                                       SELECT LAST_INSERT_ID();";

                    MySqlCommand cmdNV = new MySqlCommand(queryNV, connection, transaction);
                    cmdNV.Parameters.AddWithValue("@MaChucVu", nv.MaChucVu);
                    cmdNV.Parameters.AddWithValue("@Ho", nv.Ho);
                    cmdNV.Parameters.AddWithValue("@Ten", nv.Ten);
                    cmdNV.Parameters.AddWithValue("@Email", nv.Email);
                    cmdNV.Parameters.AddWithValue("@SDT", nv.SoDienThoai);
                    cmdNV.Parameters.AddWithValue("@NS", nv.NgaySinh.HasValue ? (object)nv.NgaySinh.Value : DBNull.Value);
                    cmdNV.Parameters.AddWithValue("@DC", string.IsNullOrEmpty(nv.DiaChi) ? DBNull.Value : (object)nv.DiaChi);
                    cmdNV.Parameters.AddWithValue("@NVL", nv.NgayVaoLam.HasValue ? (object)nv.NgayVaoLam.Value : DBNull.Value);
                    cmdNV.Parameters.AddWithValue("@TrangThai", nv.TrangThai);

                    object result = cmdNV.ExecuteScalar();

                    // Kiểm tra: Nếu kết quả không null, chuyển đổi qua Int64 rồi về Int32
                    if (result != null)
                    {
                        // Bắt buộc phải dùng Convert.ToInt64/Decimal vì LAST_INSERT_ID có thể trả về kiểu BIGINT/long
                        maNV = Convert.ToInt32(Convert.ToInt64(result));
                    }
                    else
                    {
                        // Trường hợp ExecuteScalar trả về null (rất hiếm nếu câu lệnh đúng)
                        throw new InvalidOperationException("Không thể lấy ID nhân viên mới.");
                    }

                    // Mã hóa mật khẩu (Giả sử SecurityHelper đã tồn tại)
                    string hashedPassword = SecurityHelper.HashSha256(matKhau);

                    // Lệnh 2: Thêm Tài khoản
                    string queryTK = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, LoaiTaiKhoan, MaNhanVien) VALUES (@TenDangNhap, @MatKhau, @LoaiTK, @MaNV)";
                    MySqlCommand cmdTK = new MySqlCommand(queryTK, connection, transaction);
                    cmdTK.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmdTK.Parameters.AddWithValue("@MatKhau", hashedPassword);

                    // >>> SỬA LỖI: SỬ DỤNG THAM SỐ loaiTaiKhoan ĐƯỢC TRUYỀN VÀO <<<
                    cmdTK.Parameters.AddWithValue("@LoaiTK", loaiTaiKhoan);

                    cmdTK.Parameters.AddWithValue("@MaNV", maNV);
                    cmdTK.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return maNV;
        }

        // ------------------
        // 3. CẬP NHẬT (UPDATE) - Sử dụng DatabaseHelper.ExecuteNonQuery()
        // ------------------
        public void UpdateNhanVien(NhanVien nv)
        {
            string query = @"UPDATE NhanVien SET 
                             MaChucVu=@MaChucVu, Ho=@Ho, Ten=@Ten, Email=@Email, SoDienThoai=@SDT, 
                             NgaySinh=@NS, DiaChi=@DC, NgayVaoLam=@NVL, TrangThai=@TrangThai 
                             WHERE MaNhanVien=@MaNV";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@MaChucVu", nv.MaChucVu),
                new MySqlParameter("@Ho", nv.Ho),
                new MySqlParameter("@Ten", nv.Ten),
                new MySqlParameter("@Email", nv.Email),
                new MySqlParameter("@SDT", nv.SoDienThoai),
                new MySqlParameter("@NS", nv.NgaySinh.HasValue ? (object)nv.NgaySinh.Value : DBNull.Value),
                new MySqlParameter("@DC", string.IsNullOrEmpty(nv.DiaChi) ? DBNull.Value : (object)nv.DiaChi),
                new MySqlParameter("@NVL", nv.NgayVaoLam.HasValue ? (object)nv.NgayVaoLam.Value : DBNull.Value),
                new MySqlParameter("@TrangThai", nv.TrangThai),
                new MySqlParameter("@MaNV", nv.MaNhanVien)
            };

            // Gọi hàm trợ giúp từ DatabaseHelper
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // ------------------
        // 4. XÓA (DELETE) - Sử dụng DatabaseHelper.ExecuteNonQuery()
        // ------------------
        // Trong NhanVienRepository.cs -> public void DeleteNhanVien(int maNV)
        public void DeleteNhanVien(int maNV)
        {
            using (MySqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    // BƯỚC 1: Xóa các bản ghi liên quan (Tài khoản)
                    string queryDeleteTK = "DELETE FROM TaiKhoan WHERE MaNhanVien=@MaNV";
                    MySqlCommand cmdTK = new MySqlCommand(queryDeleteTK, connection, transaction);
                    cmdTK.Parameters.AddWithValue("@MaNV", maNV);
                    cmdTK.ExecuteNonQuery();

                    // BƯỚC 2: Xóa bản ghi chính (Nhân viên)
                    string queryDeleteNV = "DELETE FROM NhanVien WHERE MaNhanVien=@MaNV";
                    MySqlCommand cmdNV = new MySqlCommand(queryDeleteNV, connection, transaction);
                    cmdNV.Parameters.AddWithValue("@MaNV", maNV);
                    cmdNV.ExecuteNonQuery();

                    transaction.Commit(); // Hoàn tất giao dịch
                }
                catch (Exception)
                {
                    transaction.Rollback(); // Hoàn tác nếu có lỗi
                    throw;
                }
            }
        }

        public List<NhanVien> SearchNhanVien(string keyword, int maChucVu, string trangThai, DateTime? ngayVaoLam)
        {
            List<NhanVien> list = new List<NhanVien>();

            using (MySqlConnection connection = DatabaseHelper.GetConnection())
            {
                string query = @"
            SELECT nv.MaNhanVien, nv.MaChucVu, nv.Ho, nv.Ten, nv.Email, nv.SoDienThoai, 
                   nv.NgaySinh, nv.DiaChi, nv.NgayVaoLam, nv.TrangThai, cv.TenChucVu
            FROM NhanVien nv
            JOIN ChucVu cv ON nv.MaChucVu = cv.MaChucVu
            WHERE (nv.Ho LIKE @Keyword OR nv.Ten LIKE @Keyword OR nv.Email LIKE @Keyword OR nv.SoDienThoai LIKE @Keyword)
            AND (@MaChucVu = 0 OR nv.MaChucVu = @MaChucVu)
            AND (@TrangThai = 'All' OR nv.TrangThai = @TrangThai)
            AND (@NgayVaoLam IS NULL OR DATE(nv.NgayVaoLam) >= DATE(@NgayVaoLam))";

                MySqlCommand cmd = new MySqlCommand(query, connection);

                // 1. Gán tham số tìm kiếm theo chuỗi
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                // 2. Gán tham số lọc Chức vụ (0 = Tất cả)
                cmd.Parameters.AddWithValue("@MaChucVu", maChucVu);

                // 3. Gán tham số lọc Trạng thái ('All' = Tất cả)
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                // Thêm tham số thứ 4
                cmd.Parameters.AddWithValue("@NgayVaoLam", ngayVaoLam.HasValue ? (object)ngayVaoLam.Value.Date : DBNull.Value);

                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new NhanVien
                        {
                            MaNhanVien = reader.GetInt32("MaNhanVien"),
                            MaChucVu = reader.GetInt32("MaChucVu"),
                            Ho = reader.GetString("Ho"),
                            Ten = reader.GetString("Ten"),
                            Email = reader.GetString("Email"),
                            SoDienThoai = reader.GetString("SoDienThoai"),

                            // Xử lý NgaySinh (Sử dụng IsDBNull)
                            NgaySinh = reader.IsDBNull(reader.GetOrdinal("NgaySinh")) ? (DateTime?)null : reader.GetDateTime("NgaySinh"),

                            // Xử lý DiaChi (Sử dụng IsDBNull)
                            DiaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? null : reader.GetString("DiaChi"),

                            // Xử lý NgayVaoLam (Sử dụng IsDBNull)
                            NgayVaoLam = reader.IsDBNull(reader.GetOrdinal("NgayVaoLam")) ? (DateTime?)null : reader.GetDateTime("NgayVaoLam"),

                            TrangThai = reader.GetString("TrangThai"),
                            TenChucVu = reader.GetString("TenChucVu")
                        });
                    }
                }
            }
            return list;
        }
    }
}