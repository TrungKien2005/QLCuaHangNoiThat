using System;

namespace QLCuaHangNoiThat.Models
{
    public class PhieuXuatKho
    {
        public int MaPhieuXuat { get; set; }
        public int MaKhachHang { get; set; }
        public int MaNhanVien { get; set; }
        public int MaKho { get; set; }
        public DateTime NgayXuat { get; set; }
        public decimal TongTien { get; set; }
        public string GhiChu { get; set; }
    }
}
