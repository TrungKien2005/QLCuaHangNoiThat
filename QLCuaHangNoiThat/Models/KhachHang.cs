using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangNoiThat.Models
{
    public class KhachHang
    {
        public int MaKhachHang { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }

        // Các trường string luôn cho phép null, nhưng tốt nhất vẫn nên để kiểu string
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }

        // 🚨 KHẮC PHỤC LỖI: PHẢI THÊM DẤU ? (Nullable) 
        // vì NgayTao trong DB CÓ NULL
        public DateTime? NgayTao { get; set; }
    }
}
