using System;
using System.Windows.Forms;
using QLCuaHangNoiThat.Models;
using QLCuaHangNoiThat.Services;

namespace QLCuaHangNoiThat.UserControls
{
    public partial class UC_NhapKho : UserControl
    {
        private readonly KhoService _khoService = new KhoService();

        public UC_NhapKho()
        {
            InitializeComponent(); // gọi từ Designer.cs
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            try
            {
                int maNCC = Convert.ToInt32(cboNhaCungCap.SelectedValue);
                int maKho = Convert.ToInt32(cboKhoNhap.SelectedValue);
                int maSP = Convert.ToInt32(cboSanPhamNhap.SelectedValue);
                int soLuong = Convert.ToInt32(nudSoLuongNhap.Value);
                decimal donGia = decimal.Parse(txtDonGiaNhap.Text);
                string ghiChu = txtGhiChuNhap.Text;

                var phieu = new PhieuNhapKho
                {
                    MaNhaCungCap = maNCC,
                    MaNhanVien = 1, // TODO: lấy user hiện tại
                    MaKho = maKho,
                    NgayNhap = DateTime.Now,
                    TongTien = (double)(soLuong * donGia),
                    GhiChu = ghiChu
                };

                bool ok = _khoService.NhapHang(phieu, maSP, soLuong, donGia);

                if (ok)
                {
                    MessageBox.Show("Nhập hàng thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nhập hàng: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
