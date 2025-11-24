using System;
using System.Drawing;
using System.Windows.Forms;
using QLCuaHangNoiThat.Forms;
using QLCuaHangNoiThat.UserControls;

namespace QLCuaHangNoiThat
{
    public partial class FormMain : Form
    {
        private UserControl currentControl;
        private Button currentButton;

        public FormMain()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            SetButtonColors();
            SetButtonIcons();
            AddHoverEffects();
        }

        private void SetButtonIcons()
        {
            // Sử dụng emoji cho icons - ĐÃ THÊM ĐẦY ĐỦ
            btnTrangChu.Text = "   🏠 Trang Chủ";
            btnSanPham.Text = "   📦 Sản Phẩm";
            btnKho.Text = "   🏭 Quản Lý Kho";
            btnBanHang.Text = "   🛒 Bán Hàng";
            btnKhachHang.Text = "   👥 Khách Hàng";
            btnNhanVien.Text = "   👨‍💼 Nhân Viên";
            btnTaiChinh.Text = "   💰 Tài Chính";
            btnBaoCao.Text = "   📊 Báo Cáo Thống Kê";
            btnNhaCungCap.Text = "   🚚 Nhà Cung Cấp";
            btnCaiDat.Text = "   ⚙️ Cài Đặt";
            btnLogout.Text = "🚪";
        }

        private void SetButtonColors()
        {
            foreach (Control control in panelSidebar.Controls)
            {
                if (control is Button btn)
                {
                    btn.BackColor = Color.FromArgb(51, 51, 76);
                    btn.ForeColor = Color.White;
                }
            }
        }

        private void AddHoverEffects()
        {
            foreach (Control control in panelSidebar.Controls)
            {
                if (control is Button btn)
                {
                    btn.MouseEnter += Button_MouseEnter;
                    btn.MouseLeave += Button_MouseLeave;
                }
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            ShowControl(typeof(UC_TrangChu), btnTrangChu);
        }

        private void ShowControl(Type controlType, Button activeButton)
        {
            // Xóa control hiện tại
            if (currentControl != null)
            {
                panelContent.Controls.Remove(currentControl);
                currentControl.Dispose();
            }

            // Reset màu button cũ
            if (currentButton != null)
            {
                currentButton.BackColor = Color.FromArgb(51, 51, 76);
                currentButton.ForeColor = Color.White;
            }

            // Set màu button mới
            activeButton.BackColor = Color.FromArgb(0, 150, 136);
            activeButton.ForeColor = Color.White;
            currentButton = activeButton;

            try
            {
                // Tạo và hiển thị control mới
                currentControl = (UserControl)Activator.CreateInstance(controlType);
                currentControl.Dock = DockStyle.Fill;
                panelContent.Controls.Add(currentControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải module: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hiệu ứng hover
        private void Button_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn != currentButton)
            {
                btn.BackColor = Color.FromArgb(70, 70, 100);
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn != currentButton)
            {
                btn.BackColor = Color.FromArgb(51, 51, 76);
            }
        }

        // Các sự kiện click menu - ĐÃ THÊM ĐẦY ĐỦ
        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            ShowControl(typeof(UC_TrangChu), (Button)sender);
            lblHeader.Text = "TRANG CHỦ - TỔNG QUAN";
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            ShowControl(typeof(UC_QuanLySanPham), (Button)sender);
            lblHeader.Text = "QUẢN LÝ SẢN PHẨM";
        }

        private void btnKho_Click(object sender, EventArgs e)
        {
            ShowControl(typeof(UC_QuanLyKho), (Button)sender);
            lblHeader.Text = "QUẢN LÝ KHO";
        }

        private void btnBanHang_Click(object sender, EventArgs e)
        {
            ShowControl(typeof(UC_QuanLyBanHang), (Button)sender);
            lblHeader.Text = "QUẢN LÝ BÁN HÀNG";
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            ShowControl(typeof(UC_QuanLyKhachHang), (Button)sender);
            lblHeader.Text = "QUẢN LÝ KHÁCH HÀNG";
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            ShowControl(typeof(UC_QuanLyNhanVien), (Button)sender);
            lblHeader.Text = "QUẢN LÝ NHÂN VIÊN";
        }

        private void btnTaiChinh_Click(object sender, EventArgs e)
        {
            ShowControl(typeof(UC_TaiChinh), (Button)sender);
            lblHeader.Text = "QUẢN LÝ TÀI CHÍNH";
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            ShowControl(typeof(UC_BaoCaoThongKe), (Button)sender);
            lblHeader.Text = "BÁO CÁO THỐNG KÊ";
        }

        private void btnNhaCungCap_Click(object sender, EventArgs e)
        {
            ShowControl(typeof(UC_QuanLyNhaCungCap), (Button)sender);
            lblHeader.Text = "QUẢN LÝ NHÀ CUNG CẤP";
        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            lblHeader.Text = "CÀI ĐẶT HỆ THỐNG";
            MessageBox.Show("Tính năng đang phát triển", "Thông báo",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận",
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                login log = new login();
                log.Show();
                this.Hide();
            }
        }
    }
}