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

                    // ✅ KIỂM TRA VÀ TẠO BẢNG NẾU CHƯA CÓ
                    EnsureTableExists(conn);

                    string query = "SELECT * FROM SanPham ORDER BY MaSP";
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

        // ✅ PHƯƠNG THỨC KIỂM TRA VÀ TẠO BẢNG
        private void EnsureTableExists(MySqlConnection conn)
        {
            try
            {
                // Kiểm tra xem bảng có tồn tại không
                string checkTableQuery = "SHOW TABLES LIKE 'SanPham'";
                MySqlCommand checkCmd = new MySqlCommand(checkTableQuery, conn);
                object result = checkCmd.ExecuteScalar();

                if (result == null)
                {
                    // Tạo bảng nếu chưa tồn tại
                    string createTableQuery = @"
                        CREATE TABLE SanPham (
                            MaSP VARCHAR(50) PRIMARY KEY,
                            TenSP VARCHAR(100) NOT NULL,
                            Gia DECIMAL(18,2) NOT NULL,
                            SoLuong INT NOT NULL,
                            DanhMuc VARCHAR(50),
                            MoTa TEXT
                        )";

                    MySqlCommand createCmd = new MySqlCommand(createTableQuery, conn);
                    createCmd.ExecuteNonQuery();

                    // Thêm dữ liệu mẫu
                    InsertSampleData(conn);

                    Console.WriteLine("✅ Đã tạo bảng SanPham và thêm dữ liệu mẫu!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi kiểm tra/tạo bảng: {ex.Message}");
            }
        }

        // ✅ THÊM DỮ LIỆU MẪU
        private void InsertSampleData(MySqlConnection conn)
        {
            try
            {
                string insertQuery = @"
                    INSERT INTO SanPham (MaSP, TenSP, Gia, SoLuong, DanhMuc, MoTa) VALUES
                    ('SP001', 'Ghế Sofa Gỗ', 5000000, 10, 'Phòng khách', 'Ghế sofa gỗ cao cấp'),
                    ('SP002', 'Bàn Ăn 6 Ghế', 3500000, 5, 'Phòng ăn', 'Bàn ăn gỗ 6 chỗ ngồi'),
                    ('SP003', 'Giường Ngủ Queen', 8000000, 3, 'Phòng ngủ', 'Giường ngủ size Queen')";

                MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                insertCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Không thể thêm dữ liệu mẫu: {ex.Message}");
            }
        }

        public bool Add(SanPham sp)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    EnsureTableExists(conn); // ✅ Đảm bảo bảng tồn tại

                    string query = @"INSERT INTO SanPham (MaSP, TenSP, Gia, SoLuong, DanhMuc, MoTa) 
                                   VALUES (@MaSP, @TenSP, @Gia, @SoLuong, @DanhMuc, @MoTa)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSP", sp.MaSP);
                    cmd.Parameters.AddWithValue("@TenSP", sp.TenSP);
                    cmd.Parameters.AddWithValue("@Gia", sp.Gia);
                    cmd.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                    cmd.Parameters.AddWithValue("@DanhMuc", sp.DanhMuc);
                    cmd.Parameters.AddWithValue("@MoTa", sp.MoTa);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 1062)
                        throw new Exception("Mã sản phẩm đã tồn tại!");
                    else
                        throw new Exception($"Lỗi khi thêm sản phẩm: {ex.Message}");
                }
            }
        }

        // ... các phương thức Update, Delete, Search giữ nguyên
    }
}