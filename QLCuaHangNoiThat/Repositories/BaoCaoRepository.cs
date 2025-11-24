using System;
using System.Data;
using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.DataAccess;

namespace QLCuaHangNoiThat.Repositories
{
    public class BaoCaoRepository
    {
        // Doanh thu theo ngày
        public DataTable GetDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            string query = @"
                SELECT NgayDatHang AS Ngay, SUM(TongTien) AS TongTien
                FROM donhang
                WHERE NgayDatHang BETWEEN @TuNgay AND @DenNgay
                GROUP BY NgayDatHang
                ORDER BY NgayDatHang";

            return DatabaseHelper.ExecuteDataTable(query, new MySqlParameter[]
            {
                new MySqlParameter("@TuNgay", tuNgay),
                new MySqlParameter("@DenNgay", denNgay)
            });
        }

        // Top sản phẩm bán chạy (bảng chi tiết cần tên là donhang_chitiet)
        public DataTable GetTopSanPham()
        {
            string query = @"
                SELECT sp.TenSanPham, SUM(ct.SoLuong) AS SoLuongBan
                FROM donhang_chitiet ct
                JOIN sanpham sp ON ct.MaSanPham = sp.MaSanPham
                GROUP BY sp.TenSanPham
                ORDER BY SoLuongBan DESC
                LIMIT 10";

            return DatabaseHelper.ExecuteDataTable(query);
        }

        // Tồn kho
        public DataTable GetTonKho()
        {
            string query = "SELECT TenSanPham, SoLuongTon FROM sanpham";
            return DatabaseHelper.ExecuteDataTable(query);
        }
    }
}