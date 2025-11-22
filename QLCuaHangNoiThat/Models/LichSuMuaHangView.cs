using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangNoiThat.Models
{
    public class LichSuMuaHangView
    {
        public int MaDonHang { get; set; }
        public DateTime? NgayDatHang { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; }

        // List này sẽ chứa các sản phẩm chi tiết của đơn hàng
        public List<ChiTietDonHangView> ChiTiet { get; set; } = new List<ChiTietDonHangView>();
    }
}
