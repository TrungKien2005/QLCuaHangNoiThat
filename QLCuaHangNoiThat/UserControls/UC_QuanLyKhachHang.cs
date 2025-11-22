using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.Models;
using QLCuaHangNoiThat.Properties;
using QLCuaHangNoiThat.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace QLCuaHangNoiThat.UserControls
{
    public partial class UC_QuanLyKhachHang : UserControl
    {
        // Loại bỏ kết nối trực tiếp (connection, connectionString)
        private KhachHangRepository _khachHangRepo = new KhachHangRepository();
        private List<KhachHang> _danhSachKhachHang;

        // Khai báo Label cho giá trị Ngày Tạo (nếu chưa có trong Designer)
        // private Label lblNgayTaoValue = new Label(); 

        public UC_QuanLyKhachHang()
        {
            InitializeComponent();
            this.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular);

            // --- Đăng ký Sự kiện (Fix lỗi NullReferenceException) ---
            this.Load += UC_QuanLyKhachHang_Load;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnXemLichSu.Click += btnXemLichSu_Click;
            dgvKhachHang.SelectionChanged += dgvKhachHang_SelectionChanged;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;

            // --- Áp dụng Style ---
            SetupButtonStyle(btnThem, Color.FromArgb(39, 174, 96));
            SetupButtonStyle(btnSua, Color.FromArgb(243, 156, 18));
            SetupButtonStyle(btnXoa, Color.FromArgb(231, 76, 60));
            SetupButtonStyle(btnXemLichSu, Color.FromArgb(41, 128, 185));
            btnThem.Cursor = Cursors.Hand;
            btnSua.Cursor = Cursors.Hand;
            btnXoa.Cursor = Cursors.Hand;
            dgvKhachHang.Cursor = Cursors.Hand;
            btnXemLichSu.Cursor = Cursors.Hand;
            btnXemLichSu.MouseEnter += btnXemLichSu_MouseEnter;
            btnXemLichSu.MouseLeave += btnXemLichSu_MouseLeave;
            SetupDataGridViewStyle();
            

            txtNgayTao.ReadOnly = true;
            txtNgayTao.TabStop = false;
            
        }
        private readonly Color PRIMARY_COLOR = Color.FromArgb(41, 128, 185);
        private readonly Color HOVER_COLOR = Color.FromArgb(60, 141, 203);
        private void btnXemLichSu_MouseEnter(object sender, EventArgs e)
        {
            btnXemLichSu.BackColor = HOVER_COLOR;
        }

        private void btnXemLichSu_MouseLeave(object sender, EventArgs e)
        {
            btnXemLichSu.BackColor = PRIMARY_COLOR;
        }
        private void SetupButtonStyle(Button btn, Color backColor)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = backColor;
            btn.ForeColor = Color.White;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold); // Nên thống nhất Font
            if (btn.Name == "btnXemLichSu")
            {
                // 1. Gán Icon (giả sử tên resource là HistoryIcon)
                // Thay Resources.HistoryIcon bằng tên tài nguyên bạn đã import
              
                // 2. Icon nằm ở bên trái
                btn.ImageAlign = ContentAlignment.MiddleLeft;

                // 3. Chữ nằm ở giữa bên phải
                btn.TextAlign = ContentAlignment.MiddleCenter;

                // 4. Thiết lập mối quan hệ giữa chữ và icon
                // ImageAndText: Icon và chữ hiển thị cạnh nhau
                btn.TextImageRelation = TextImageRelation.ImageBeforeText;

                // 5. Thêm padding (khoảng đệm) để Icon không quá sát viền và chữ không quá sát Icon
                // Padding(left, top, right, bottom)
                btn.Padding = new Padding(10, 0, 10, 0);
            }
        }
        private void SetupDataGridViewStyle()
        {
            // --- 1. Loại bỏ đường viền/Hiệu ứng cũ ---
            dgvKhachHang.RowHeadersVisible = false;      // Ẩn tiêu đề dòng (cột có mũi tên)
            dgvKhachHang.BorderStyle = BorderStyle.None;  // Loại bỏ viền ngoài
            dgvKhachHang.AllowUserToResizeRows = false;   // Không cho phép người dùng thay đổi chiều cao dòng
            dgvKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Chọn cả dòng

            // --- 2. Cấu hình Header (Tiêu đề cột) ---
            dgvKhachHang.EnableHeadersVisualStyles = false; // Bật tắt để dùng màu tùy chỉnh
            dgvKhachHang.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvKhachHang.ColumnHeadersHeight = 35; // Tăng chiều cao Header

            // Thiết lập Font và Màu cho Header
            dgvKhachHang.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185); // Xanh dương đậm
            dgvKhachHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvKhachHang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // --- 3. Cấu hình Dữ liệu và Chọn dòng ---
            // Màu xen kẽ cho hàng (Alternating Rows)
            dgvKhachHang.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(236, 240, 241);

            // Màu khi chọn dòng
            dgvKhachHang.DefaultCellStyle.SelectionBackColor = Color.FromArgb(179, 217, 255); // Xanh nhạt
            dgvKhachHang.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Đảm bảo dữ liệu căn giữa hoặc căn trái/phải hợp lý
            dgvKhachHang.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);

            // --- 4. Tự động kéo cột chiếm hết chiều rộng ---
            dgvKhachHang.AutoGenerateColumns = true;
            dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKhachHang.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;


        }

        // Cần gọi LoadKhachHang khi UserControl tải lên
        private void UC_QuanLyKhachHang_Load(object sender, EventArgs e)
        {
            LoadKhachHang();
        }


        // HÀM READ (Tải dữ liệu và làm sạch giao diện)

        private void LoadKhachHang()
        {
            try
            {
                // 1. Gọi Repository để tải dữ liệu
                _danhSachKhachHang = _khachHangRepo.GetAllKhachHang();

                // Gán List<KhachHang> vào DataGridView
                dgvKhachHang.DataSource = _danhSachKhachHang;

                // Tùy chỉnh FillWeight cho các cột (logic giữ nguyên)
                if (dgvKhachHang.Columns.Count > 0)
                {
                    if (dgvKhachHang.Columns.Contains("MaKhachHang")) dgvKhachHang.Columns["MaKhachHang"].FillWeight = 8;
                    if (dgvKhachHang.Columns.Contains("Ho")) dgvKhachHang.Columns["Ho"].FillWeight = 18;
                    if (dgvKhachHang.Columns.Contains("Ten")) dgvKhachHang.Columns["Ten"].FillWeight = 18;
                    if (dgvKhachHang.Columns.Contains("Email")) dgvKhachHang.Columns["Email"].FillWeight = 24;
                    if (dgvKhachHang.Columns.Contains("SoDienThoai")) dgvKhachHang.Columns["SoDienThoai"].FillWeight = 12;
                    if (dgvKhachHang.Columns.Contains("DiaChi")) dgvKhachHang.Columns["DiaChi"].FillWeight = 20;
                    if (dgvKhachHang.Columns.Contains("NgayTao")) dgvKhachHang.Columns["NgayTao"].FillWeight = 18;
                }

                if (dgvKhachHang.Columns.Contains("MaKhachHang"))
                {
                    // 💡 SỬA LỖI: Đặt chiều rộng tối thiểu cho cột Mã KH
                    dgvKhachHang.Columns["MaKhachHang"].MinimumWidth = 110; // Đặt tối thiểu 80 pixel

                    // (Giữ nguyên FillWeight của bạn)
                    dgvKhachHang.Columns["MaKhachHang"].FillWeight = 8;
                }
                if (dgvKhachHang.Rows.Count > 0)
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        dgvKhachHang.ClearSelection();
                        ClearFields(); // <-- Reset tất cả TextBox
                    });
                }
                else
                {
                    // Nếu không có dữ liệu, xóa trắng ngay lập tức
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải khách hàng: " + ex.Message);
            }
        }
        // Trong UC_QuanLyKhachHang.cs

        private void btnXemLichSu_Click(object sender, EventArgs e)
        {
            // 💡 SỬA ĐIỀU KIỆN KIỂM TRA:
            // Kiểm tra xem có dòng nào đang được người dùng CHỌN (SelectedRows) không.
            if (dgvKhachHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng từ danh sách để xem lịch sử mua hàng.", "Thiếu thông tin");
                return;
            }

            try
            {
                // 💡 LẤY DỮ LIỆU TỪ DÒNG ĐƯỢC CHỌN (SelectedRows[0])
                // Lấy dòng đầu tiên trong tập hợp các dòng được chọn (vì SelectionMode là FullRowSelect và MultiSelect là false)
                DataGridViewRow selectedDataRow = dgvKhachHang.SelectedRows[0];

                // 1. Lấy đối tượng KhachHang đang được chọn từ DataGridView
                KhachHang selectedKH = selectedDataRow.DataBoundItem as KhachHang;

                // Kiểm tra an toàn, mặc dù SelectedRows[0] thường đảm bảo đã có dữ liệu
                if (selectedKH == null) return;

                int maKH = selectedKH.MaKhachHang;
                // Ghép Ho và Ten để có tên đầy đủ
                string tenKH = selectedKH.Ho + " " + selectedKH.Ten;

                // 2. Gọi Repository để lấy lịch sử
                List<LichSuMuaHangView> lichSu = _khachHangRepo.GetLichSuMuaHangByMaKH(maKH);

                if (lichSu.Count == 0)
                {
                    MessageBox.Show($"Khách hàng {tenKH} chưa có đơn hàng nào.", "Không có dữ liệu");
                    return;
                }

                // 3. Hiển thị Form chi tiết lịch sử
                // Đảm bảo FormLichSuMuaHang được tham chiếu đúng namespace
                using (var formLichSu = new QLCuaHangNoiThat.Forms.FormLichSuMuaHang(lichSu, maKH, tenKH))
                {
                    formLichSu.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lịch sử mua hàng: " + ex.Message, "Lỗi CSDL");
            }
        }
        // HÀM CREATE (Thêm Khách hàng)
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Thêm kiểm tra bắt buộc
            if (string.IsNullOrWhiteSpace(txtHo.Text) || string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ họ và tên", "Thiếu thông tin");
                return;
            }

            // Xác nhận trước khi thêm
            var confirmAdd = MessageBox.Show(
        "Bạn có chắc chắn muốn thêm khách hàng này?",
        "Xác nhận thêm",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question);

            if (confirmAdd != DialogResult.Yes) return;

            try
            {
                // Tạo đối tượng KhachHang từ các TextBox
                KhachHang newKH = new KhachHang
                {
                    Ho = txtHo.Text.Trim(),
                    Ten = txtTen.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    SoDienThoai = txtSDT.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim()
                };

                // Gọi Repository để INSERT
                _khachHangRepo.AddKhachHang(newKH);

                MessageBox.Show("Thêm khách hàng thành công!");
                LoadKhachHang();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm khách hàng: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow == null) return;

            // Xác nhận trước khi sửa
            var confirmUpdate = MessageBox.Show(
        "Bạn có chắc chắn muốn cập nhật thông tin khách hàng này?",
        "Xác nhận cập nhật",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question);

            if (confirmUpdate != DialogResult.Yes) return;

            try
            {
                // Lấy MaKhachHang hiện tại từ DataGridView
                int maKH = Convert.ToInt32(dgvKhachHang.CurrentRow.Cells["MaKhachHang"].Value);

                // Tạo đối tượng KhachHang từ các TextBox
                KhachHang updatedKH = new KhachHang
                {
                    MaKhachHang = maKH,
                    Ho = txtHo.Text.Trim(),
                    Ten = txtTen.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    SoDienThoai = txtSDT.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim()
                };

                _khachHangRepo.UpdateKhachHang(updatedKH); // Gọi Repository

                MessageBox.Show("Cập nhật khách hàng thành công!");
                LoadKhachHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật khách hàng: " + ex.Message);
            }
        }

        
        // HÀM DELETE (Xóa Khách hàng)
        
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow == null) return;

            if (MessageBox.Show("Bạn có chắc muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    int maKH = Convert.ToInt32(dgvKhachHang.CurrentRow.Cells["MaKhachHang"].Value);

                    _khachHangRepo.DeleteKhachHang(maKH); // Gọi Repository

                    MessageBox.Show("Xóa khách hàng thành công!");
                    LoadKhachHang();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa khách hàng: " + ex.Message);
                }
            }
        }


        // ----------------------------------------------------
        // HÀM SELECTION CHANGED (Đổ dữ liệu lên UI)
        // ----------------------------------------------------
        private void dgvKhachHang_SelectionChanged(object sender, EventArgs e)
        {
            // BẢO VỆ CHỐNG LỖI/CHỐNG CHỌN MẶC ĐỊNH
            if (dgvKhachHang.CurrentRow == null || dgvKhachHang.CurrentRow.DataBoundItem == null)
            {
                ClearFields();
                return;
            }

            KhachHang selectedKH = dgvKhachHang.CurrentRow.DataBoundItem as KhachHang;

            if (selectedKH == null) return;

            // Đổ dữ liệu lên TextBox
            txtHo.Text = selectedKH.Ho ?? string.Empty;
            txtTen.Text = selectedKH.Ten ?? string.Empty;
            txtEmail.Text = selectedKH.Email ?? string.Empty;
            txtSDT.Text = selectedKH.SoDienThoai ?? string.Empty;
            txtDiaChi.Text = selectedKH.DiaChi ?? string.Empty;

            // Hiển thị Ngày Tạo vào TextBox txtNgayTao
            txtNgayTao.Text = selectedKH.NgayTao.HasValue
        ? selectedKH.NgayTao.Value.ToString("dd/MM/yyyy")
        : string.Empty;
        }

        // ----------------------------------------------------
        // HÀM CLEARFIELDS (Làm sạch UI)
        // ----------------------------------------------------
        private void ClearFields()
        {
            txtHo.Clear();
            txtTen.Clear();
            txtEmail.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtNgayTao.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            txtHo.Focus();
        }

        // ----------------------------------------------------
        // HÀM SEARCH (Tìm kiếm)
        // ----------------------------------------------------
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTimKiem.Text.Trim();

                // GỌI REPOSITORY CHO HÀM TÌM KIẾM
                _danhSachKhachHang = _khachHangRepo.SearchKhachHang(keyword);

                dgvKhachHang.DataSource = _danhSachKhachHang;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void InitializeComponent()
        {
            this.pnlInput = new System.Windows.Forms.Panel();
            this.btnXemLichSu = new System.Windows.Forms.Button();
            this.txtNgayTao = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtHo = new System.Windows.Forms.TextBox();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvKhachHang = new System.Windows.Forms.DataGridView();
            this.pnlInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlInput
            // 
            this.pnlInput.Controls.Add(this.btnXemLichSu);
            this.pnlInput.Controls.Add(this.txtNgayTao);
            this.pnlInput.Controls.Add(this.label7);
            this.pnlInput.Controls.Add(this.txtTimKiem);
            this.pnlInput.Controls.Add(this.txtSDT);
            this.pnlInput.Controls.Add(this.txtDiaChi);
            this.pnlInput.Controls.Add(this.txtTen);
            this.pnlInput.Controls.Add(this.txtEmail);
            this.pnlInput.Controls.Add(this.txtHo);
            this.pnlInput.Controls.Add(this.btnXoa);
            this.pnlInput.Controls.Add(this.btnSua);
            this.pnlInput.Controls.Add(this.btnThem);
            this.pnlInput.Controls.Add(this.label6);
            this.pnlInput.Controls.Add(this.label5);
            this.pnlInput.Controls.Add(this.label4);
            this.pnlInput.Controls.Add(this.label3);
            this.pnlInput.Controls.Add(this.label2);
            this.pnlInput.Controls.Add(this.label1);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInput.Location = new System.Drawing.Point(0, 0);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(1057, 349);
            this.pnlInput.TabIndex = 0;
            // 
            // btnXemLichSu
            // 
            this.btnXemLichSu.Location = new System.Drawing.Point(789, 267);
            this.btnXemLichSu.Name = "btnXemLichSu";
            this.btnXemLichSu.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.btnXemLichSu.Size = new System.Drawing.Size(113, 76);
            this.btnXemLichSu.TabIndex = 8;
            this.btnXemLichSu.Text = "Lịch Sử Mua Hàng";
            this.btnXemLichSu.UseVisualStyleBackColor = true;
            // 
            // txtNgayTao
            // 
            this.txtNgayTao.BackColor = System.Drawing.SystemColors.Window;
            this.txtNgayTao.Location = new System.Drawing.Point(461, 205);
            this.txtNgayTao.Name = "txtNgayTao";
            this.txtNgayTao.ReadOnly = true;
            this.txtNgayTao.Size = new System.Drawing.Size(215, 34);
            this.txtNgayTao.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 270);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 28);
            this.label7.TabIndex = 6;
            this.label7.Text = "Tìm kiếm:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimKiem.Location = new System.Drawing.Point(127, 267);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(273, 34);
            this.txtTimKiem.TabIndex = 5;
            // 
            // txtSDT
            // 
            this.txtSDT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSDT.Location = new System.Drawing.Point(461, 113);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(177, 34);
            this.txtSDT.TabIndex = 5;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiaChi.Location = new System.Drawing.Point(127, 205);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(177, 34);
            this.txtDiaChi.TabIndex = 5;
            // 
            // txtTen
            // 
            this.txtTen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTen.Location = new System.Drawing.Point(461, 46);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(177, 34);
            this.txtTen.TabIndex = 5;
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.Location = new System.Drawing.Point(127, 113);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(177, 34);
            this.txtEmail.TabIndex = 5;
            // 
            // txtHo
            // 
            this.txtHo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHo.Location = new System.Drawing.Point(127, 46);
            this.txtHo.Name = "txtHo";
            this.txtHo.Size = new System.Drawing.Size(177, 34);
            this.txtHo.TabIndex = 5;
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(789, 200);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(113, 45);
            this.btnXoa.TabIndex = 4;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(789, 116);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(113, 45);
            this.btnSua.TabIndex = 4;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(789, 41);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(113, 45);
            this.btnThem.TabIndex = 4;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(367, 208);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 28);
            this.label6.TabIndex = 3;
            this.label6.Text = "Ngày Tạo:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(367, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 28);
            this.label5.TabIndex = 3;
            this.label5.Text = "SDT:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 28);
            this.label4.TabIndex = 3;
            this.label4.Text = "Địa Chỉ:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 28);
            this.label3.TabIndex = 2;
            this.label3.Text = " Email:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(367, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 28);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tên:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Họ:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvKhachHang
            // 
            this.dgvKhachHang.BackgroundColor = System.Drawing.Color.White;
            this.dgvKhachHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKhachHang.Location = new System.Drawing.Point(0, 349);
            this.dgvKhachHang.Name = "dgvKhachHang";
            this.dgvKhachHang.RowHeadersWidth = 62;
            this.dgvKhachHang.RowTemplate.Height = 28;
            this.dgvKhachHang.Size = new System.Drawing.Size(1057, 316);
            this.dgvKhachHang.TabIndex = 1;
            // 
            // UC_QuanLyKhachHang
            // 
            this.Controls.Add(this.dgvKhachHang);
            this.Controls.Add(this.pnlInput);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UC_QuanLyKhachHang";
            this.Size = new System.Drawing.Size(1057, 665);
            this.pnlInput.ResumeLayout(false);
            this.pnlInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).EndInit();
            this.ResumeLayout(false);

        }

        private Panel pnlInput;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label6;
        private Label label5;
        private Label label4;
        private TextBox txtTimKiem;
        private TextBox txtSDT;
        private TextBox txtDiaChi;
        private TextBox txtTen;
        private TextBox txtEmail;
        private TextBox txtHo;
        private Button btnXoa;
        private Button btnSua;
        private Button btnThem;

        

        private Label label7;

        

        private DataGridView dgvKhachHang;
        private TextBox txtNgayTao;
        private Button btnXemLichSu;
    }
}
