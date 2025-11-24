namespace QLCuaHangNoiThat.Models
{
    public class SanPham
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int MaDanhMuc { get; set; }
        public int? MaNhaCungCap { get; set; }
        public string SKU { get; set; }
        public string MoTa { get; set; }
        public decimal GiaNhap { get; set; }
        public decimal GiaBan { get; set; }
        public int SoLuongTon { get; set; }
        public string HinhAnhURL { get; set; }
        public bool DangKinhDoanh { get; set; }
        public string TenDanhMuc { get; set; }
        public string TenNhaCungCap { get; set; }

        // Thêm property mới nếu cần
        public string TrangThaiKinhDoanh { get; set; }
    }
}