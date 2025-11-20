using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangNoiThat.Models
{
    public class NhanVien
    {
        public int MaNhanVien { get; set; }
        public int MaChucVu { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public DateTime? NgaySinh { get; set; } // Kiểu date trong CSDL là DateTime trong C#
        public string DiaChi { get; set; }
        public DateTime? NgayVaoLam { get; set; } // Kiểu date trong CSDL là DateTime trong C#
        public string TrangThai { get; set; }

        public string TenChucVu { get; set; }
    }
}
