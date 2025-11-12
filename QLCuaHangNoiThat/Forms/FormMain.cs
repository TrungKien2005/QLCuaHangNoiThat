using System;
using System.Drawing;
using System.Windows.Forms;
using QLCuaHangNoiThat.UserControls;

namespace QLCuaHangNoiThat
{
    public partial class FormMain : Form
    {
        private Panel sidebar;
        private Panel panelMain;
        private Label lblTitle;

        public FormMain()
        {
            InitializeComponent();
            BuildDashboard(); // 🟢 Xây dashboard động
        }

        // ============ HÀM TẠO DASHBOARD ============ //
        private void BuildDashboard()
        {
            // === SIDEBAR ===
            sidebar = new Panel()
            {
                Dock = DockStyle.Left,
                Width = 220,
                BackColor = Color.FromArgb(45, 45, 60)
            };
            this.Controls.Add(sidebar);

            // === HEADER ===
            lblTitle = new Label()
            {
                Dock = DockStyle.Top,
                Height = 60,
                Text = "QUẢN LÝ CỬA HÀNG NỘI THẤT",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(70, 70, 90)
            };
            this.Controls.Add(lblTitle);
            lblTitle.BringToFront();

            // === MAIN PANEL ===
            panelMain = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            this.Controls.Add(panelMain);
            panelMain.BringToFront();

            // === SIDEBAR BUTTONS - ĐÃ SỬA TÊN PHƯƠNG THỨC ===
            AddSidebarButton("🏠 Trang chủ", TrangChu_Click);
            AddSidebarButton("📦 Quản lý sản phẩm", QuanLySanPham_Click);
            AddSidebarButton("🏗 Quản lý kho", QuanLyKho_Click);
            AddSidebarButton("🛒 Quản lý bán hàng", QuanLyBanHang_Click);
            AddSidebarButton("👥 Quản lý khách hàng", QuanLyKhachHang_Click);
            AddSidebarButton("💼 Quản lý nhân viên", QuanLyNhanVien_Click);
            AddSidebarButton("💰 Quản lý tài chính", QuanLyTaiChinh_Click);
            AddSidebarButton("📊 Báo cáo & thống kê", BaoCaoThongKe_Click);
            AddSidebarButton("🏭 Nhà cung cấp", NhaCungCap_Click);
            AddSidebarButton("🚪 Đăng xuất", DangXuat_Click);

            // 🔹 Mặc định hiển thị Trang chủ
            LoadDashboardHome();
        }

        // ============ HÀM THÊM NÚT ============ //
        private void AddSidebarButton(string text, EventHandler onClick)
        {
            Button btn = new Button()
            {
                Text = text,
                Dock = DockStyle.Top,
                Height = 45,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                BackColor = Color.FromArgb(50, 50, 70)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += onClick;
            sidebar.Controls.Add(btn);
            sidebar.Controls.SetChildIndex(btn, 0);
        }

        // ============ HÀM LOAD USER CONTROL ============ //
        private void LoadControl(UserControl control)
        {
            panelMain.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panelMain.Controls.Add(control);
        }

        // ============ DASHBOARD HOME ============ //
        private void LoadDashboardHome()
        {
            panelMain.Controls.Clear();
            Label lbl = new Label()
            {
                Text = "📊 BẢNG ĐIỀU KHIỂN CHÍNH\n\nChọn chức năng ở menu bên trái để quản lý.",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelMain.Controls.Add(lbl);
            lblTitle.Text = "Bảng điều khiển";
        }

        // ============ CÁC SỰ KIỆN MENU - ĐÃ SỬA TÊN PHƯƠNG THỨC ============ //
        private void TrangChu_Click(object sender, EventArgs e)
        {
            LoadDashboardHome();
        }

        private void QuanLySanPham_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Quản lý sản phẩm";
            try
            {
                var uc = new UC_QuanLySanPham();
                LoadControl(uc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Lỗi khi tải UC_QuanLySanPham", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void QuanLyKho_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Quản lý kho";
            panelMain.Controls.Clear();
            panelMain.Controls.Add(new Label()
            {
                Text = "Chức năng Quản lý kho đang được phát triển...",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12, FontStyle.Regular)
            });
        }

        private void QuanLyBanHang_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Quản lý bán hàng";
            // Thêm code xử lý ở đây
        }

        private void QuanLyKhachHang_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Quản lý khách hàng";
            // Thêm code xử lý ở đây
        }

        private void QuanLyNhanVien_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Quản lý nhân viên";
            // Thêm code xử lý ở đây
        }

        private void QuanLyTaiChinh_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Quản lý tài chính";
            // Thêm code xử lý ở đây
        }

        private void BaoCaoThongKe_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Báo cáo & thống kê";
            // Thêm code xử lý ở đây
        }

        private void NhaCungCap_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Quản lý nhà cung cấp";
            // Thêm code xử lý ở đây
        }

        private void DangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Code xử lý khi form load
        }
    }
}