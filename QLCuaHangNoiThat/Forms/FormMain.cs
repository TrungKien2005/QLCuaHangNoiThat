using System;
using System.Drawing;
using System.Windows.Forms;
using QLCuaHangNoiThat.UserControls;
using QLCuaHangNoiThat.DataAccess; // ✅ Đã sửa namespace
using QLCuaHangNoiThat.Repositories; // ✅ Thêm using này

namespace QLCuaHangNoiThat
{
    public partial class FormMain : Form
    {
        private Panel sidebar;
        private Panel panelMain;
        private Label lblTitle;
        private Button currentButton;

        public FormMain()
        {
            InitializeComponent();

            // ✅ Test connection khi khởi động
            TestDatabaseConnection();

            BuildDashboard();
        }

        // ✅ Hàm test kết nối database
        private void TestDatabaseConnection()
        {
            if (DatabaseHelper.TestConnection())
            {
                Console.WriteLine("✅ Kết nối MySQL thành công!");
            }
            else
            {
                MessageBox.Show("❌ Không thể kết nối đến MySQL! Vui lòng kiểm tra database.",
                              "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============ HÀM TẠO DASHBOARD ============ //
        private void BuildDashboard()
        {
            // Cấu hình form chính
            this.Text = "Hệ Thống Quản Lý Cửa Hàng Nội Thất";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.White;

            // === SIDEBAR ===
            sidebar = new Panel()
            {
                Dock = DockStyle.Left,
                Width = 250,
                BackColor = Color.FromArgb(45, 45, 60)
            };
            this.Controls.Add(sidebar);

            // === HEADER ===
            lblTitle = new Label()
            {
                Dock = DockStyle.Top,
                Height = 70,
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
                BackColor = Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(panelMain);
            panelMain.BringToFront();

            // === SIDEBAR BUTTONS ===
            AddSidebarButton("🏠 Trang chủ", TrangChu_Click);
            AddSidebarButton("📦 Quản lý sản phẩm", QuanLySanPham_Click);
            AddSidebarButton("🏗 Quản lý kho", QuanLyKho_Click);
            AddSidebarButton("🛒 Quản lý bán hàng", QuanLyBanHang_Click);
            AddSidebarButton("👥 Quản lý khách hàng", QuanLyKhachHang_Click);
            AddSidebarButton("💼 Quản lý nhân viên", QuanLyNhanVien_Click);
            AddSidebarButton("💰 Quản lý tài chính", QuanLyTaiChinh_Click);
            AddSidebarButton("📊 Báo cáo & thống kê", BaoCaoThongKe_Click);
            AddSidebarButton("🏭 Nhà cung cấp", NhaCungCap_Click);
            AddSidebarButton("⚙️ Cài đặt", CaiDat_Click);
            AddSidebarButton("🚪 Đăng xuất", DangXuat_Click);

            // Mặc định hiển thị Trang chủ
            LoadDashboardHome();
        }

        // ============ HÀM THÊM NÚT SIDEBAR ============ //
        private void AddSidebarButton(string text, EventHandler onClick)
        {
            Button btn = new Button()
            {
                Text = text,
                Dock = DockStyle.Top,
                Height = 50,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(25, 0, 0, 0),
                BackColor = Color.FromArgb(50, 50, 70),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(65, 65, 85);
            btn.Click += onClick;
            btn.Click += (s, e) => ActivateButton((Button)s);
            sidebar.Controls.Add(btn);
            sidebar.Controls.SetChildIndex(btn, 0);
        }

        // ============ KÍCH HOẠT NÚT ĐƯỢC CHỌN ============ //
        private void ActivateButton(Button btn)
        {
            if (currentButton != null)
            {
                currentButton.BackColor = Color.FromArgb(50, 50, 70);
                currentButton.Font = new Font(currentButton.Font, FontStyle.Regular);
            }

            currentButton = btn;
            currentButton.BackColor = Color.FromArgb(0, 122, 204);
            currentButton.Font = new Font(currentButton.Font, FontStyle.Bold);
        }

        // ============ HÀM LOAD USER CONTROL ============ //
        private void LoadControl(UserControl control)
        {
            try
            {
                panelMain.Controls.Clear();
                control.Dock = DockStyle.Fill;
                panelMain.Controls.Add(control);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải control: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadDashboardHome();
            }
        }

        // ============ DASHBOARD HOME ============ //
        private void LoadDashboardHome()
        {
            panelMain.Controls.Clear();

            Panel homePanel = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            // Tiêu đề chào mừng
            Label lblWelcome = new Label()
            {
                Text = "🎉 CHÀO MỪNG ĐẾN HỆ THỐNG QUẢN LÝ",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(70, 70, 90),
                AutoSize = true,
                Location = new Point(50, 50)
            };

            // Mô tả hệ thống
            Label lblDescription = new Label()
            {
                Text = "Hệ thống quản lý toàn diện cho cửa hàng nội thất\nQuản lý sản phẩm, kho, bán hàng, khách hàng, nhân viên và tài chính",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(50, 120)
            };

            // Thống kê nhanh
            Panel statsPanel = new Panel()
            {
                Location = new Point(50, 200),
                Size = new Size(700, 150),
                BackColor = Color.Transparent
            };

            AddStatCard(statsPanel, "📦", "Tổng sản phẩm", "150", 0);
            AddStatCard(statsPanel, "💰", "Doanh thu hôm nay", "12.5M", 180);
            AddStatCard(statsPanel, "👥", "Khách hàng", "45", 360);
            AddStatCard(statsPanel, "🛒", "Đơn hàng", "23", 540);

            homePanel.Controls.Add(lblWelcome);
            homePanel.Controls.Add(lblDescription);
            homePanel.Controls.Add(statsPanel);
            panelMain.Controls.Add(homePanel);

            lblTitle.Text = "Bảng Điều Khiển Chính";
        }

        private void AddStatCard(Panel parent, string icon, string title, string value, int x)
        {
            Panel card = new Panel()
            {
                Location = new Point(x, 0),
                Size = new Size(160, 120),
                BackColor = Color.FromArgb(250, 250, 255),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblIcon = new Label()
            {
                Text = icon,
                Font = new Font("Segoe UI", 20, FontStyle.Regular),
                Location = new Point(20, 15),
                AutoSize = true
            };

            Label lblValue = new Label()
            {
                Text = value,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 122, 204),
                Location = new Point(20, 50),
                AutoSize = true
            };

            Label lblTitle = new Label()
            {
                Text = title,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.Gray,
                Location = new Point(20, 85),
                AutoSize = true
            };

            card.Controls.Add(lblIcon);
            card.Controls.Add(lblValue);
            card.Controls.Add(lblTitle);
            parent.Controls.Add(card);
        }

        // ============ CÁC SỰ KIỆN MENU ============ //
        private void TrangChu_Click(object sender, EventArgs e)
        {
            LoadDashboardHome();
        }

        private void QuanLySanPham_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "QUẢN LÝ SẢN PHẨM";
            try
            {
                var uc = new UC_QuanLySanPham();
                LoadControl(uc);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải Quản lý sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadDashboardHome();
            }
        }

        private void QuanLyKho_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "QUẢN LÝ KHO";
            try
            {
                // Tạo UserControl Quản lý kho tạm thời
                UserControl ucKho = new UserControl()
                {
                    BackColor = Color.White
                };

                Label lblContent = new Label()
                {
                    Text = "📦 QUẢN LÝ KHO\n\nChức năng đang được phát triển...",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 14, FontStyle.Regular),
                    ForeColor = Color.Gray
                };

                ucKho.Controls.Add(lblContent);
                LoadControl(ucKho);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi Quản lý kho", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void QuanLyBanHang_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "QUẢN LÝ BÁN HÀNG";
            try
            {
                // Tạo UserControl Bán hàng tạm thời
                UserControl ucBanHang = new UserControl()
                {
                    BackColor = Color.White
                };

                Label lblContent = new Label()
                {
                    Text = "🛒 QUẢN LÝ BÁN HÀNG\n\nChức năng đang được phát triển...",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 14, FontStyle.Regular),
                    ForeColor = Color.Gray
                };

                ucBanHang.Controls.Add(lblContent);
                LoadControl(ucBanHang);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi Quản lý bán hàng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void QuanLyKhachHang_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "QUẢN LÝ KHÁCH HÀNG";
            try
            {
                // Tạo UserControl Khách hàng tạm thời
                UserControl ucKhachHang = new UserControl()
                {
                    BackColor = Color.White
                };

                Label lblContent = new Label()
                {
                    Text = "👥 QUẢN LÝ KHÁCH HÀNG\n\nChức năng đang được phát triển...",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 14, FontStyle.Regular),
                    ForeColor = Color.Gray
                };

                ucKhachHang.Controls.Add(lblContent);
                LoadControl(ucKhachHang);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi Quản lý khách hàng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void QuanLyNhanVien_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "QUẢN LÝ NHÂN VIÊN";
            try
            {
                // Tạo UserControl Nhân viên tạm thời
                UserControl ucNhanVien = new UserControl()
                {
                    BackColor = Color.White
                };

                Label lblContent = new Label()
                {
                    Text = "💼 QUẢN LÝ NHÂN VIÊN\n\nChức năng đang được phát triển...",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 14, FontStyle.Regular),
                    ForeColor = Color.Gray
                };

                ucNhanVien.Controls.Add(lblContent);
                LoadControl(ucNhanVien);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi Quản lý nhân viên", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void QuanLyTaiChinh_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "QUẢN LÝ TÀI CHÍNH";
            try
            {
                var uc = new UC_TaiChinh();
                LoadControl(uc);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải Quản lý tài chính: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadDashboardHome();
            }
        }

        private void BaoCaoThongKe_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "BÁO CÁO & THỐNG KÊ";
            try
            {
                // Tạo UserControl Báo cáo tạm thời
                UserControl ucBaoCao = new UserControl()
                {
                    BackColor = Color.White
                };

                Label lblContent = new Label()
                {
                    Text = "📊 BÁO CÁO & THỐNG KÊ\n\nChức năng đang được phát triển...",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 14, FontStyle.Regular),
                    ForeColor = Color.Gray
                };

                ucBaoCao.Controls.Add(lblContent);
                LoadControl(ucBaoCao);
            }
            catch (Exception) // ✅ Đã sửa: không sử dụng biến ex
            {
                MessageBox.Show("Lỗi khi tải báo cáo thống kê", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NhaCungCap_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "QUẢN LÝ NHÀ CUNG CẤP";
            try
            {
                // Tạo UserControl Nhà cung cấp tạm thời
                UserControl ucNhaCungCap = new UserControl()
                {
                    BackColor = Color.White
                };

                Label lblContent = new Label()
                {
                    Text = "🏭 QUẢN LÝ NHÀ CUNG CẤP\n\nChức năng đang được phát triển...",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 14, FontStyle.Regular),
                    ForeColor = Color.Gray
                };

                ucNhaCungCap.Controls.Add(lblContent);
                LoadControl(ucNhaCungCap);
            }
            catch (Exception) // ✅ Đã sửa: không sử dụng biến ex
            {
                MessageBox.Show("Lỗi khi tải nhà cung cấp", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CaiDat_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "CÀI ĐẶT HỆ THỐNG";
            try
            {
                // Tạo UserControl Cài đặt tạm thời
                UserControl ucCaiDat = new UserControl()
                {
                    BackColor = Color.White
                };

                Label lblContent = new Label()
                {
                    Text = "⚙️ CÀI ĐẶT HỆ THỐNG\n\nChức năng đang được phát triển...",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 14, FontStyle.Regular),
                    ForeColor = Color.Gray
                };

                ucCaiDat.Controls.Add(lblContent);
                LoadControl(ucCaiDat);
            }
            catch (Exception) // ✅ Đã sửa: không sử dụng biến ex
            {
                MessageBox.Show("Lỗi khi tải cài đặt", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát ứng dụng?", "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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