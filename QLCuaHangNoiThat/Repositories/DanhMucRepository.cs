using System;
using System.Data;
using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.DataAccess;
using QLCuaHangNoiThat.Models;

namespace QLCuaHangNoiThat.Repositories
{
    public class DanhMucRepository
    {
        public DataTable GetAll()
        {
            try
            {
                string query = "SELECT MaDanhMuc, TenDanhMuc, MoTa FROM DanhMuc ORDER BY TenDanhMuc";
                return DatabaseHelper.ExecuteDataTable(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi GetAll DanhMuc: {ex.Message}");
                return new DataTable(); // Trả về DataTable rỗng
            }
        }

        public bool Add(DanhMuc danhMuc)
        {
            try
            {
                string query = "INSERT INTO DanhMuc (TenDanhMuc, MoTa) VALUES (@TenDanhMuc, @MoTa)";
                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@TenDanhMuc", danhMuc.TenDanhMuc),
                    new MySqlParameter("@MoTa", danhMuc.MoTa ?? (object)DBNull.Value)
                };
                return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi Add DanhMuc: {ex.Message}");
                return false;
            }
        }

        public bool Update(DanhMuc danhMuc)
        {
            try
            {
                string query = "UPDATE DanhMuc SET TenDanhMuc = @TenDanhMuc, MoTa = @MoTa WHERE MaDanhMuc = @MaDanhMuc";
                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@TenDanhMuc", danhMuc.TenDanhMuc),
                    new MySqlParameter("@MoTa", danhMuc.MoTa ?? (object)DBNull.Value),
                    new MySqlParameter("@MaDanhMuc", danhMuc.MaDanhMuc)
                };
                return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi Update DanhMuc: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int maDanhMuc)
        {
            try
            {
                // Kiểm tra xem danh mục có đang được sử dụng không
                if (IsDanhMucInUse(maDanhMuc))
                {
                    return false; // Không thể xóa vì đang được sử dụng
                }

                string query = "DELETE FROM DanhMuc WHERE MaDanhMuc = @MaDanhMuc";
                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@MaDanhMuc", maDanhMuc)
                };
                return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi Delete DanhMuc: {ex.Message}");
                return false;
            }
        }

        public bool IsDanhMucInUse(int maDanhMuc)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM SanPham WHERE MaDanhMuc = @MaDanhMuc";
                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@MaDanhMuc", maDanhMuc)
                };
                var result = DatabaseHelper.ExecuteScalar(query, parameters);
                return Convert.ToInt32(result) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi IsDanhMucInUse: {ex.Message}");
                return true; // Để an toàn, giả sử đang được sử dụng nếu có lỗi
            }
        }

        public DanhMuc GetById(int maDanhMuc)
        {
            try
            {
                string query = "SELECT MaDanhMuc, TenDanhMuc, MoTa FROM DanhMuc WHERE MaDanhMuc = @MaDanhMuc";
                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@MaDanhMuc", maDanhMuc)
                };

                DataTable dt = DatabaseHelper.ExecuteDataTable(query, parameters);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new DanhMuc
                    {
                        MaDanhMuc = Convert.ToInt32(row["MaDanhMuc"]),
                        TenDanhMuc = row["TenDanhMuc"].ToString(),
                        MoTa = row["MoTa"]?.ToString()
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi GetById DanhMuc: {ex.Message}");
                return null;
            }
        }
    }
}