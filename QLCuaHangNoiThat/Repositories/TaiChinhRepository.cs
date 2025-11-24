using System;
using System.Data;
using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.DataAccess;

namespace QLCuaHangNoiThat.Repositories
{
    public class TaiChinhRepository
    {
        // Lấy tất cả giao dịch
        public DataTable GetAllGiaoDich()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT tc.MaGiaoDich, tc.LoaiGiaoDich, tc.SoTien, tc.NoiDung, tc.NgayGiaoDich,
                               nv.MaNhanVien, CONCAT(nv.Ho, ' ', nv.Ten) AS TenNhanVien
                        FROM taichinh tc
                        LEFT JOIN nhanvien nv ON tc.MaNhanVien = nv.MaNhanVien
                        ORDER BY tc.NgayGiaoDich DESC";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi lấy dữ liệu tài chính: {ex.Message}");
                }
            }
            return dt;
        }

        // Thêm giao dịch mới
        public bool ThemGiaoDich(string loaiGD, decimal soTien, string noiDung, DateTime ngayGD, string maNhanVien)
        {
            try
            {
                string query = @"INSERT INTO taichinh (LoaiGiaoDich, SoTien, NoiDung, NgayGiaoDich, MaNhanVien)
                         VALUES (@LoaiGD, @SoTien, @NoiDung, @NgayGD, @MaNV)";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
            new MySqlParameter("@LoaiGD", loaiGD),
            new MySqlParameter("@SoTien", soTien),
            new MySqlParameter("@NoiDung", noiDung),
            new MySqlParameter("@NgayGD", ngayGD),
            new MySqlParameter("@MaNV", maNhanVien)
                };

                return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
            }
            catch
            {
                throw;
            }
        }

        // Lọc giao dịch theo ngày và loại
        public DataTable LocGiaoDich(DateTime tuNgay, DateTime denNgay, string loaiGD)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT tc.MaGiaoDich, tc.LoaiGiaoDich, tc.SoTien, tc.NoiDung, tc.NgayGiaoDich,
                               nv.MaNhanVien, CONCAT(nv.Ho, ' ', nv.Ten) AS TenNhanVien
                        FROM taichinh tc
                        LEFT JOIN nhanvien nv ON tc.MaNhanVien = nv.MaNhanVien
                        WHERE tc.NgayGiaoDich BETWEEN @TuNgay AND @DenNgay";

                    if (loaiGD != "Tất cả")
                        query += " AND tc.LoaiGiaoDich = @LoaiGiaoDich";

                    query += " ORDER BY tc.NgayGiaoDich DESC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                    cmd.Parameters.AddWithValue("@DenNgay", denNgay);
                    if (loaiGD != "Tất cả")
                        cmd.Parameters.AddWithValue("@LoaiGiaoDich", loaiGD);

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi lọc dữ liệu tài chính: {ex.Message}");
                }
            }
            return dt;
        }

        // Tính tổng thu
        public decimal TongThu(DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT SUM(SoTien) FROM taichinh WHERE LoaiGiaoDich = 'Thu'";
                    if (tuNgay.HasValue && denNgay.HasValue)
                        query += " AND NgayGiaoDich BETWEEN @TuNgay AND @DenNgay";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    if (tuNgay.HasValue && denNgay.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@TuNgay", tuNgay.Value);
                        cmd.Parameters.AddWithValue("@DenNgay", denNgay.Value);
                    }

                    object result = cmd.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi tính tổng thu: {ex.Message}");
                }
            }
        }

        // Tính tổng chi
        public decimal TongChi(DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT SUM(SoTien) FROM taichinh WHERE LoaiGiaoDich = 'Chi'";
                    if (tuNgay.HasValue && denNgay.HasValue)
                        query += " AND NgayGiaoDich BETWEEN @TuNgay AND @DenNgay";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    if (tuNgay.HasValue && denNgay.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@TuNgay", tuNgay.Value);
                        cmd.Parameters.AddWithValue("@DenNgay", denNgay.Value);
                    }

                    object result = cmd.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi tính tổng chi: {ex.Message}");
                }
            }
        }

        // Lấy danh sách nhân viên để ComboBox
        public DataTable GetNhanVienList()
        {
            string query = @"
        SELECT 
            MaNhanVien, 
            CONCAT(Ho, ' ', Ten) AS Ten
        FROM nhanvien
        WHERE 
            TrangThai = 'Active' 
            OR TrangThai = 1 
            OR TrangThai = '1'
    ";

            return DatabaseHelper.ExecuteDataTable(query);
        }



    }
}
