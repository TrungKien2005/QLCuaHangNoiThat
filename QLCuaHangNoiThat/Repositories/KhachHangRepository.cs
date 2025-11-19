using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.DataAccess;
using QLCuaHangNoiThat.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace QLCuaHangNoiThat.Repositories
{
    // Giả sử bạn có Interface IKhachHangRepository
    public class KhachHangRepository // : IKhachHangRepository 
    {
        // Hàm trợ giúp để ánh xạ DataReader sang KhachHang Model
        private KhachHang MapReaderToKhachHang(MySqlDataReader reader)
        {
            return new KhachHang
            {
                MaKhachHang = reader.GetInt32("MaKhachHang"),
                Ho = reader.GetString("Ho"),
                Ten = reader.GetString("Ten"),

                // Xử lý các trường có thể NULL khi ĐỌC (Sử dụng chỉ số cột)
                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                SoDienThoai = reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? null : reader.GetString(reader.GetOrdinal("SoDienThoai")),
                DiaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? null : reader.GetString(reader.GetOrdinal("DiaChi")),

                // NgayTao phải là DateTime?
                NgayTao = reader.IsDBNull(reader.GetOrdinal("NgayTao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("NgayTao"))
            };
        }

        // ------------------
        // 1. TẢI TẤT CẢ (READ ALL) - Đã thêm try-catch an toàn
        // ------------------
        // --- Sửa Hàm GetAllKhachHang ---

        public List<KhachHang> GetAllKhachHang()
        {
            List<KhachHang> list = new List<KhachHang>();
            string query = "SELECT MaKhachHang, Ho, Ten, Email, SoDienThoai, DiaChi, NgayTao FROM KhachHang";

            using (MySqlConnection connection = DatabaseHelper.GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                try
                {
                    connection.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        // 💡 CHỈ LẤY CHỈ SỐ CỘT (ORDINAL) MỘT LẦN TRƯỚC VÒNG LẶP!
                        int emailIndex = reader.GetOrdinal("Email");
                        int sdtIndex = reader.GetOrdinal("SoDienThoai");
                        int diaChiIndex = reader.GetOrdinal("DiaChi");
                        int ngayTaoIndex = reader.GetOrdinal("NgayTao");

                        while (reader.Read())
                        {
                            list.Add(new KhachHang
                            {
                                // MaKhachHang, Ho, Ten có thể dùng tên vì chúng không NULL và không bị lặp
                                MaKhachHang = reader.GetInt32("MaKhachHang"),
                                Ho = reader.GetString("Ho"),
                                Ten = reader.GetString("Ten"),

                                // Sử dụng chỉ số cột đã lấy để truy cập
                                Email = reader.IsDBNull(emailIndex) ? null : reader.GetString(emailIndex),
                                SoDienThoai = reader.IsDBNull(sdtIndex) ? null : reader.GetString(sdtIndex),
                                DiaChi = reader.IsDBNull(diaChiIndex) ? null : reader.GetString(diaChiIndex),
                                NgayTao = reader.IsDBNull(ngayTaoIndex) ? (DateTime?)null : reader.GetDateTime(ngayTaoIndex)
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi tải dữ liệu khách hàng từ CSDL.", ex);
                }
            }
            return list;
        }

        // 🛑 Tương tự, áp dụng cách này cho hàm SearchKhachHang (thay thế MapReaderToKhachHang(reader))

        // ------------------
        // 2. THÊM DỮ LIỆU (CREATE)
        // ------------------
        public int AddKhachHang(KhachHang kh)
        {
            int maKH = -1;
            string query = @"
                INSERT INTO KhachHang (Ho, Ten, Email, SoDienThoai, DiaChi)
                VALUES (@Ho, @Ten, @Email, @SDT, @DiaChi);
                SELECT LAST_INSERT_ID();";

            using (MySqlConnection connection = DatabaseHelper.GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Gán tham số và xử lý NULL/TRIM
                cmd.Parameters.AddWithValue("@Ho", kh.Ho.Trim());
                cmd.Parameters.AddWithValue("@Ten", kh.Ten.Trim());

                cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(kh.Email) ? DBNull.Value : (object)kh.Email.Trim());
                cmd.Parameters.AddWithValue("@SDT", string.IsNullOrEmpty(kh.SoDienThoai) ? DBNull.Value : (object)kh.SoDienThoai.Trim());
                cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrEmpty(kh.DiaChi) ? DBNull.Value : (object)kh.DiaChi.Trim());

                try
                {
                    connection.Open();
                    // Thực thi và lấy ID mới
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        maKH = Convert.ToInt32(Convert.ToInt64(result));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi CSDL khi thêm khách hàng.", ex);
                }
            }
            return maKH;
        }

        // ------------------
        // 3. CẬP NHẬT (UPDATE)
        // ------------------
        public void UpdateKhachHang(KhachHang kh)
        {
            string query = @"
                UPDATE KhachHang SET Ho=@Ho, Ten=@Ten, Email=@Email, SoDienThoai=@SDT, DiaChi=@DiaChi 
                WHERE MaKhachHang=@MaKH";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                // Sử dụng .Trim() cho nhất quán
                new MySqlParameter("@Ho", kh.Ho.Trim()),
                new MySqlParameter("@Ten", kh.Ten.Trim()),
                
                // Xử lý NULL
                new MySqlParameter("@Email", string.IsNullOrEmpty(kh.Email) ? DBNull.Value : (object)kh.Email.Trim()),
                new MySqlParameter("@SDT", string.IsNullOrEmpty(kh.SoDienThoai) ? DBNull.Value : (object)kh.SoDienThoai.Trim()),
                new MySqlParameter("@DiaChi", string.IsNullOrEmpty(kh.DiaChi) ? DBNull.Value : (object)kh.DiaChi.Trim()),

                new MySqlParameter("@MaKH", kh.MaKhachHang)
            };

            // Sử dụng ExecuteNonQuery từ DatabaseHelper (Nếu helper không có try-catch, lỗi sẽ ném từ đây)
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // ------------------
        // 4. XÓA (DELETE)
        // ------------------
        public void DeleteKhachHang(int maKH)
        {
            // Trong Khách hàng, thường chỉ cần xóa dòng chính (không có Tài khoản)
            string query = "DELETE FROM KhachHang WHERE MaKhachHang=@MaKH";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@MaKH", maKH)
            };

            // Sử dụng ExecuteNonQuery từ DatabaseHelper
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // ------------------
        // 5. TÌM KIẾM (SEARCH)
        // ------------------
        public List<KhachHang> SearchKhachHang(string keyword)
        {
            List<KhachHang> list = new List<KhachHang>();

            string query = @"
                SELECT MaKhachHang, Ho, Ten, Email, SoDienThoai, DiaChi, NgayTao
                FROM KhachHang
                WHERE Ho LIKE @Keyword OR Ten LIKE @Keyword OR SoDienThoai LIKE @Keyword OR Email LIKE @Keyword";

            using (MySqlConnection connection = DatabaseHelper.GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                try
                {
                    connection.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapReaderToKhachHang(reader));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi tìm kiếm khách hàng trong CSDL.", ex);
                }
            }
            return list;
        }
    }
}