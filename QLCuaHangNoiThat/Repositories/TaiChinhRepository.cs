using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.DataAccess;

namespace QLCuaHangNoiThat.Repositories
{
    public class TaiChinhRepository
    {
        public DataTable GetAllGiaoDich()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT 
                                    MaGD, LoaiGD, SoTien, NoiDung, NgayGD, NhanVien, CreatedAt
                                    FROM GiaoDich 
                                    ORDER BY NgayGD DESC, MaGD DESC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi lấy dữ liệu giao dịch: {ex.Message}");
                }
            }
            return dt;
        }

        public bool ThemGiaoDich(string loaiGD, decimal soTien, string noiDung, DateTime ngayGD, string nhanVien)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO GiaoDich (LoaiGD, SoTien, NoiDung, NgayGD, NhanVien)
                                   VALUES (@LoaiGD, @SoTien, @NoiDung, @NgayGD, @NhanVien)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@LoaiGD", loaiGD);
                    cmd.Parameters.AddWithValue("@SoTien", soTien);
                    cmd.Parameters.AddWithValue("@NoiDung", noiDung);
                    cmd.Parameters.AddWithValue("@NgayGD", ngayGD);
                    cmd.Parameters.AddWithValue("@NhanVien", nhanVien ?? (object)DBNull.Value);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi thêm giao dịch: {ex.Message}");
                }
            }
        }

        public DataTable LocGiaoDich(DateTime tuNgay, DateTime denNgay, string loaiGD)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT * FROM GiaoDich 
                                   WHERE NgayGD BETWEEN @TuNgay AND @DenNgay";

                    if (loaiGD != "Tất cả")
                        query += " AND LoaiGD = @LoaiGD";

                    query += " ORDER BY NgayGD DESC, MaGD DESC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TuNgay", tuNgay.Date);
                    cmd.Parameters.AddWithValue("@DenNgay", denNgay.Date.AddDays(1).AddSeconds(-1));

                    if (loaiGD != "Tất cả")
                        cmd.Parameters.AddWithValue("@LoaiGD", loaiGD);

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi lọc giao dịch: {ex.Message}");
                }
            }
            return dt;
        }

        public decimal TongThu(DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            return TongTheoLoai("Thu", tuNgay, denNgay);
        }

        public decimal TongChi(DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            return TongTheoLoai("Chi", tuNgay, denNgay);
        }

        private decimal TongTheoLoai(string loaiGD, DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COALESCE(SUM(SoTien), 0) FROM GiaoDich WHERE LoaiGD = @LoaiGD";

                    if (tuNgay.HasValue && denNgay.HasValue)
                    {
                        query += " AND NgayGD BETWEEN @TuNgay AND @DenNgay";
                    }

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@LoaiGD", loaiGD);

                    if (tuNgay.HasValue && denNgay.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@TuNgay", tuNgay.Value.Date);
                        cmd.Parameters.AddWithValue("@DenNgay", denNgay.Value.Date.AddDays(1).AddSeconds(-1));
                    }

                    object result = cmd.ExecuteScalar();
                    return result == DBNull.Value ? 0 : Convert.ToDecimal(result);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi tính tổng {loaiGD}: {ex.Message}");
                }
            }
        }

        public DataTable GetNhanVienList()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MaNV, TenNV FROM NhanVien ORDER BY TenNV";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}");
                    // Hoặc: Console.WriteLine(ex.Message);
                }
            }
            return dt;
        }
    }
}