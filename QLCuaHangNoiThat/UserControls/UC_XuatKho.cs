using System;
using System.Windows.Forms;
using QLCuaHangNoiThat.Models;
using QLCuaHangNoiThat.Services;

namespace QLCuaHangNoiThat.UserControls
{
    public partial class UC_XuatKho : UserControl
    {
        private readonly KhoService _khoService = new KhoService();

        public UC_XuatKho()
        {
            InitializeComponent();
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO: Đổi lại tên controls theo form thật
                int maKH = Convert.ToInt32(cboKhachHang.SelectedValue);
                int maKho = Convert.ToInt32(cboKhoXuat.SelectedValue);
                int maSP = Convert.ToInt32(cboSanPhamXuat.SelectedValue);
                int soLuong = Convert.ToInt32(nudSoLuongXuat.Value);
                decimal donGia = decimal.Parse(txtDonGiaXuat.Text);
                string ghiChu = txtGhiChuXuat.Text;

                var phieu = new PhieuXuatKho
                {
                    MaKhachHang = maKH,
                    MaNhanVien = 1, // TODO: Lấy từ user đăng nhập
                    MaKho = maKho,
                    NgayXuat = DateTime.Now,
                    TongTien = soLuong * donGia,
                    GhiChu = ghiChu
                };

                bool ok = _khoService.XuatHang(phieu, maSP, soLuong, donGia);

                if (ok)
                {
                    MessageBox.Show("Xuất hàng thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // TonKhoChanged sẽ gọi UC_QuanLyKho refresh tự động
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất hàng: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnThemPhieuXuat_Click(object sender, EventArgs e)
        {
            try
            {
                // Ví dụ: lấy thông tin từ các control
                int maKho = Convert.ToInt32(cboKhoXuat.SelectedValue);
                int maSanPham = Convert.ToInt32(cboSanPhamXuat.SelectedValue);
                int soLuong = Convert.ToInt32(nudSoLuongXuat.Value);
                decimal donGia = decimal.Parse(txtDonGiaXuat.Text);
                string ghiChu = txtGhiChuXuat.Text;

                var phieu = new PhieuXuatKho
                {
                    MaKho = maKho,
                    MaNhanVien = 1, // TODO: lấy từ user đăng nhập
                    NgayXuat = DateTime.Now,
                    TongTien = soLuong * donGia, // tự động là decimal
                    GhiChu = ghiChu
                };

                bool ok = _khoService.XuatHang(phieu, maSanPham, soLuong, donGia);
                if (ok)
                {
                    MessageBox.Show("Xuất hàng thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // UC_QuanLyKho hoặc UC_TonKho sẽ tự refresh qua sự kiện TonKhoChanged
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
