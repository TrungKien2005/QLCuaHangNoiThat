using QLCuaHangNoiThat.DataAccess;
using QLCuaHangNoiThat.Models;
using QLCuaHangNoiThat.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLCuaHangNoiThat.UserControls
{
    public partial class UC_QuanLyNhanVien : UserControl
    {
        private NhanVienRepository _nhanVienRepo = new NhanVienRepository();
        private List<NhanVien> _danhSachNhanVien;
        public UC_QuanLyNhanVien()
        {
            InitializeComponent();
            this.dgvNhanVien.SelectionChanged += new System.EventHandler(this.dgvNhanVien_SelectionChanged);
            // Áp dụng trong Constructor
            btnThem.Cursor = Cursors.Hand;
            btnSua.Cursor = Cursors.Hand;
            btnXoa.Cursor = Cursors.Hand;
            dgvNhanVien.Cursor = Cursors.Hand;
            SetupDataGridViewStyle();
            this.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular);
            InitializeFilterButton();

            // Đăng ký events
            btnLoc.MouseEnter += btnLoc_MouseEnter;
            btnLoc.MouseLeave += btnLoc_MouseLeave;
            btnLoc.MouseDown += btnLoc_MouseDown;
            btnLoc.MouseUp += btnLoc_MouseUp;
            btnLoc.Click += (s, e) => FilterNhanVien();
            // In the constructor, wire the new reset button (add this line)
            btnHuyLoc.Click += btnHuyLoc_Click;
            btnHuyLoc.Cursor = Cursors.Hand;
            btnHuyLoc.FlatStyle = FlatStyle.Flat;
            btnHuyLoc.FlatAppearance.BorderSize = 0;
            btnHuyLoc.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            // Make its size identical to the other action buttons (btnLoc/btnThem/btnSua/btnXoa)
            btnHuyLoc.Size = btnLoc.Size;

        }
        // Add this method near other event handlers / helpers
        private void btnHuyLoc_Click(object sender, EventArgs e)
        {
            // Clear text filters
            txtTimKiem.Clear();

            // Reset comboboxes to placeholder / "no filter"
            if (cbChucVu.Items.Count > 0) cbChucVu.SelectedIndex = 0;
            if (cbTrangThai.Items.Count > 0) cbTrangThai.SelectedIndex = 0;

            // Disable date filter and reset DateTimePickers
            dtpNgayVaoLam.Checked = false;
            dtpNgayVaoLam.Value = DateTime.Now;
            dtpNgaySinh.Value = DateTime.Now;

            
            LoadNhanVien();
            dgvNhanVien.ClearSelection();
            ClearInputFields();
        }
        // Add this method (place near other helpers like LoadNhanVien/LoadChucVu)
        private void FilterNhanVien()
        {
            try
            {
                // 1. keyword (optional)
                string keyword = txtTimKiem.Text?.Trim() ?? string.Empty;

                // 2. MaChucVu (0 = All / placeholder)
                int maChucVu = 0;
                if (cbChucVu.SelectedValue != null)
                    int.TryParse(cbChucVu.SelectedValue.ToString(), out maChucVu);

                // 3. TrangThai ('All' means no filter)
                string trangThai = "All";
                if (cbTrangThai.SelectedItem != null && cbTrangThai.SelectedIndex != 0)
                    trangThai = cbTrangThai.SelectedItem.ToString();

                // 4. NgayVaoLam (nullable if checkbox not checked)
                DateTime? ngayVaoLam = null;
                if (dtpNgayVaoLam.Checked)
                    ngayVaoLam = dtpNgayVaoLam.Value.Date;

                // 5. Call repository with chosen criteria (SearchNhanVien handles combinations)
                _danhSachNhanVien = _nhanVienRepo.SearchNhanVien(keyword, maChucVu, trangThai, ngayVaoLam);

                // 6. Update grid safely
                dgvNhanVien.DataSource = _danhSachNhanVien;

                if (dgvNhanVien.Rows.Count > 0)
                {
                    this.BeginInvoke((MethodInvoker)delegate {
                        dgvNhanVien.ClearSelection();
                        ClearInputFields();
                    });
                }
                else
                {
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lọc dữ liệu: " + ex.Message, "Lỗi");
            }
        }
        private void SetupDataGridViewStyle()
        {
            // --- 1. Loại bỏ đường viền/Hiệu ứng cũ ---
            dgvNhanVien.RowHeadersVisible = false;      // Ẩn tiêu đề dòng (cột có mũi tên)
            dgvNhanVien.BorderStyle = BorderStyle.None;  // Loại bỏ viền ngoài
            dgvNhanVien.AllowUserToResizeRows = false;   // Không cho phép người dùng thay đổi chiều cao dòng
            dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Chọn cả dòng

            // --- 2. Cấu hình Header (Tiêu đề cột) ---
            dgvNhanVien.EnableHeadersVisualStyles = false; // Bật tắt để dùng màu tùy chỉnh
            dgvNhanVien.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvNhanVien.ColumnHeadersHeight = 35; // Tăng chiều cao Header

            // Thiết lập Font và Màu cho Header
            dgvNhanVien.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185); // Xanh dương đậm
            dgvNhanVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvNhanVien.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // --- 3. Cấu hình Dữ liệu và Chọn dòng ---
            // Màu xen kẽ cho hàng (Alternating Rows)
            dgvNhanVien.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(236, 240, 241);

            // Màu khi chọn dòng
            dgvNhanVien.DefaultCellStyle.SelectionBackColor = Color.FromArgb(179, 217, 255); // Xanh nhạt
            dgvNhanVien.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Đảm bảo dữ liệu căn giữa hoặc căn trái/phải hợp lý
            dgvNhanVien.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);

            // --- 4. Tự động kéo cột chiếm hết chiều rộng ---
            dgvNhanVien.AutoGenerateColumns = true;
            dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNhanVien.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;


        }
        // Trong Form_Load hoặc constructor
        private void InitializeFilterButton()
        {
            // Thiết lập màu sắc ban đầu
            btnLoc.BackColor = ColorTranslator.FromHtml("#3B82F6");
            btnLoc.ForeColor = Color.White;
            btnLoc.FlatStyle = FlatStyle.Flat;
            btnLoc.FlatAppearance.BorderSize = 0;
            btnLoc.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            btnLoc.Cursor = Cursors.Hand;

            // Thêm bo góc (optional - nếu muốn)
            btnLoc.FlatAppearance.BorderSize = 0;
        }

        // Event khi hover vào button
        private void btnLoc_MouseEnter(object sender, EventArgs e)
        {
            btnLoc.BackColor = ColorTranslator.FromHtml("#2563EB"); // Màu đậm hơn khi hover
        }

        // Event khi chuột rời khỏi button
        private void btnLoc_MouseLeave(object sender, EventArgs e)
        {
            btnLoc.BackColor = ColorTranslator.FromHtml("#3B82F6"); // Trở về màu ban đầu
        }

        // Event khi nhấn xuống (MouseDown)
        private void btnLoc_MouseDown(object sender, MouseEventArgs e)
        {
            btnLoc.BackColor = ColorTranslator.FromHtml("#1D4ED8"); // Màu đậm nhất khi active
                                                                    // Thêm hiệu ứng shadow bằng cách dịch chuyển nhẹ
            btnLoc.Location = new Point(btnLoc.Location.X + 1, btnLoc.Location.Y + 1);
        }

        // Event khi nhả chuột (MouseUp)
        private void btnLoc_MouseUp(object sender, MouseEventArgs e)
        {
            btnLoc.BackColor = ColorTranslator.FromHtml("#2563EB"); // Về màu hover
            btnLoc.Location = new Point(btnLoc.Location.X - 1, btnLoc.Location.Y - 1); // Trở về vị trí ban đầu
        }
        private void UC_QuanLyNhanVien_Load(object sender, EventArgs e)
        {

            LoadNhanVien();
            LoadChucVu();
            LoadTrangThai();
        }
        private void LoadTrangThai()
        {
            // Populate cbTrangThai with a placeholder + real options (same UX as cbChucVu)
            cbTrangThai.Items.Clear();
            cbTrangThai.Items.Add("--Chọn trạng thái"); // index 0 = placeholder (no filter)
            cbTrangThai.Items.Add("Active");
            cbTrangThai.Items.Add("Inactive");
            cbTrangThai.SelectedIndex = 0;
        }

        private void LoadNhanVien()
        {
            try
            {
                // 1. Tải dữ liệu như bình thường
                _danhSachNhanVien = _nhanVienRepo.SearchNhanVien("", 0, "All", null);

                dgvNhanVien.DataSource = null;
                dgvNhanVien.DataSource = _danhSachNhanVien;

                // 2. >>> KỸ THUẬT QUAN TRỌNG: SỬ DỤNG BeginInvoke <<<
                // Buộc việc hủy chọn và làm sạch UI chạy sau khi DataGridView đã hoàn tất gán DataSource và hiển thị.
                if (dgvNhanVien.Rows.Count > 0)
                {
                    // Dùng BeginInvoke để đảm bảo lệnh này chạy sau khi DGV đã hoàn tất việc chọn hàng mặc định
                    this.BeginInvoke((MethodInvoker)delegate {
                        dgvNhanVien.ClearSelection();
                        ClearInputFields();
                    });
                }
                else
                {
                    // Trường hợp không có dữ liệu
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải nhân viên: " + ex.Message, "Lỗi");
            }
        }
        private void LoadChucVu()
        {
            try
            {
                string query = "SELECT MaChucVu, TenChucVu FROM ChucVu";

                // Gọi DatabaseHelper để lấy DataTable chức vụ thực tế
                DataTable dt = DatabaseHelper.ExecuteDataTable(query);

                // BƯỚC 1: TẠO MỤC GIỮ CHỖ (PLACEHOLDER ROW)
                DataRow placeholderRow = dt.NewRow();

                // Gán giá trị Mã Chức vụ = 0 hoặc -1 (để kiểm tra là chưa chọn)
                placeholderRow["MaChucVu"] = 0;

                // Gán Tên Chức vụ là Placeholder text
                placeholderRow["TenChucVu"] = "--- Chọn chức vụ ---";

                // BƯỚC 2: CHÈN MỤC GIỮ CHỖ VÀO ĐẦU DANH SÁCH
                dt.Rows.InsertAt(placeholderRow, 0);

                // BƯỚC 3: Gán Data Source (bao gồm cả placeholder)
                cbChucVu.DataSource = dt;
                cbChucVu.DisplayMember = "TenChucVu";
                cbChucVu.ValueMember = "MaChucVu";

                // Đặt mục mặc định là mục giữ chỗ (Index 0)
                cbChucVu.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chức vụ: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // 1. KIỂM TRA NHẬP LIỆU BẮT BUỘC
            if (string.IsNullOrWhiteSpace(txtHo.Text) ||
        string.IsNullOrWhiteSpace(txtTen.Text) ||
        string.IsNullOrWhiteSpace(txtEmail.Text) ||
        string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
        string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ các thông tin bắt buộc (Họ, Tên, Email, Tài khoản, Mật khẩu).", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng hàm lại
            }
            int maChucVuDuocChon = (int)cbChucVu.SelectedValue;

            if (maChucVuDuocChon == 0) // Kiểm tra nếu giá trị là 0 (Mã của mục giữ chỗ)
            {
                MessageBox.Show("Vui lòng chọn chức vụ hợp lệ cho nhân viên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng hàm lại
            }
            if (cbTrangThai.SelectedIndex == 0 || cbTrangThai.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn trạng thái (Active/Inactive).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. THÊM BƯỚC XÁC NHẬN NGƯỜI DÙNG (MỚI)
            DialogResult confirmResult = MessageBox.Show(
        "Bạn có chắc chắn muốn thêm nhân viên này vào hệ thống?",
        "Xác nhận Thêm",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
      );

            // Chỉ thực hiện khi người dùng chọn YES
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    // VÔ HIỆU HÓA nút chỉ khi đã qua validation và xác nhận
                    btnThem.Enabled = false;

                    // 1. Thu thập dữ liệu và tạo đối tượng NhanVien Model
                    NhanVien newNV = new NhanVien
                    {
                        MaChucVu = (int)cbChucVu.SelectedValue,
                        Ho = txtHo.Text,
                        Ten = txtTen.Text,
                        Email = txtEmail.Text,
                        SoDienThoai = txtSDT.Text,

                        // Lấy giá trị từ DateTimePicker và TextBox
                        NgaySinh = dtpNgaySinh.Value,
                        DiaChi = txtDiaChi.Text,
                        NgayVaoLam = dtpNgayVaoLam.Value,

                        TrangThai = cbTrangThai.SelectedItem.ToString()
                    };
                    string loaiTaiKhoanString = GetLoaiTaiKhoanString(newNV.MaChucVu);
                    // 2. Gọi Repository
                    _nhanVienRepo.AddNhanVien(newNV, txtTenDangNhap.Text, txtMatKhau.Text, loaiTaiKhoanString);

                    // 3. THÀNH CÔNG
                    MessageBox.Show("✅ Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadNhanVien();
                    ClearFields();
                }
                catch (MySql.Data.MySqlClient.MySqlException mySqlEx)
                {
                    // Mã lỗi MySQL cho Duplicate Entry là 1062
                    if (mySqlEx.Number == 1062)
                    {
                        MessageBox.Show("❌ Lỗi trùng lặp: Tài khoản hoặc Email đã tồn tại. Vui lòng kiểm tra lại.", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("❌ Lỗi SQL: " + mySqlEx.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Bắt các lỗi chung khác
                    MessageBox.Show("❌ Lỗi thêm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // KÍCH HOẠT LẠI nút "Thêm" sau khi xử lý xong
                    btnThem.Enabled = true;
                }
            }
            // Nếu người dùng chọn NO, hàm kết thúc mà không làm gì cả.
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow != null)
            {
                // THÊM BƯỚC XÁC NHẬN NGƯỜI DÙNG
                DialogResult result = MessageBox.Show(
          "Bạn có chắc chắn muốn cập nhật thông tin nhân viên này?",
          "Xác nhận Cập nhật",
          MessageBoxButtons.YesNo,
          MessageBoxIcon.Question
        );

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // LẤY ĐỐI TƯỢNG NHANVIEN ĐANG CHỌN TỪ LƯỚI
                        NhanVien selectedItem = dgvNhanVien.CurrentRow.DataBoundItem as NhanVien;

                        if (selectedItem == null) return; // Kiểm tra an toàn

                        // TẠO ĐỐI TƯỢNG UPDATEDNV
                        NhanVien updatedNV = new NhanVien
                        {
                            // 1. Lấy MaNhanVien từ đối tượng được chọn
                            MaNhanVien = selectedItem.MaNhanVien,

                            // 2. Tiếp tục lấy các giá trị mới từ UI Controls (NHƯ CODE CŨ CỦA BẠN)
                            MaChucVu = (int)cbChucVu.SelectedValue,
                            Ho = txtHo.Text,
                            Ten = txtTen.Text,
                            Email = txtEmail.Text,
                            SoDienThoai = txtSDT.Text,
                            NgaySinh = dtpNgaySinh.Value,
                            DiaChi = txtDiaChi.Text,
                            NgayVaoLam = dtpNgayVaoLam.Value,
                            TrangThai = cbTrangThai.SelectedItem.ToString()
                        };

                        // 3. GỌI REPOSITORY (UPDATE)
                        _nhanVienRepo.UpdateNhanVien(updatedNV);

                        // 4. HIỂN THỊ VÀ REFRESH
                        MessageBox.Show("Cập nhật nhân viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadNhanVien();
                    }
                    catch (Exception ex)
                    {
                        // ... (Xử lý lỗi)
                        string chiTietLoi = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                        MessageBox.Show($"Lỗi cập nhật: {chiTietLoi}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // Nếu người dùng chọn No, chương trình không làm gì cả (thoát khỏi hàm).
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow != null)
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Xác nhận",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        int maNV = Convert.ToInt32(dgvNhanVien.CurrentRow.Cells["MaNhanVien"].Value);

                        // 1. Gọi Repository
                        _nhanVienRepo.DeleteNhanVien(maNV);

                        MessageBox.Show("Xóa nhân viên thành công!");
                        LoadNhanVien();
                        ClearFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xóa nhân viên: " + ex.Message);
                    }
                }
            }
        }

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            // BẢO VỆ CHÍNH: Nếu không có hàng hợp lệ nào được chọn, BUỘC làm trắng và thoát.
            if (dgvNhanVien.CurrentRow == null || dgvNhanVien.CurrentRow.DataBoundItem == null)
            {
                ClearInputFields(); // <<< Nếu không chọn, UI phải trắng
                return;
            }

            // --- ĐÃ VƯỢT QUA KIỂM TRA (Người dùng đã chọn một hàng HỢP LỆ) ---

            // Lấy đối tượng NhanVien trực tiếp từ hàng được chọn
            NhanVien selectedNV = dgvNhanVien.CurrentRow.DataBoundItem as NhanVien;

            // Kiểm tra an toàn lần cuối
            if (selectedNV == null)
            {
                ClearInputFields();
                return;
            }

            // 1. Đổ dữ liệu cơ bản
            txtHo.Text = selectedNV.Ho ?? string.Empty;
            txtTen.Text = selectedNV.Ten ?? string.Empty;
            txtEmail.Text = selectedNV.Email ?? string.Empty;
            txtSDT.Text = selectedNV.SoDienThoai ?? string.Empty;

            // 2. Đổ dữ liệu NgaySinh (DateTime?)
            if (selectedNV.NgaySinh.HasValue)
            {
                dtpNgaySinh.Value = selectedNV.NgaySinh.Value;
            }
            else
            {
                dtpNgaySinh.Value = dtpNgaySinh.MinDate;
            }

            // 3. Đổ dữ liệu Địa Chỉ (string)
            txtDiaChi.Text = selectedNV.DiaChi ?? string.Empty;

            // 4. Đổ dữ liệu Ngày Vào Làm (DateTime?)
            if (selectedNV.NgayVaoLam.HasValue)
            {
                dtpNgayVaoLam.Value = selectedNV.NgayVaoLam.Value;
            }
            else
            {
                dtpNgayVaoLam.Value = dtpNgayVaoLam.MinDate;
            }

            // 5. Đổ ComboBox
            cbChucVu.SelectedValue = selectedNV.MaChucVu;
            cbTrangThai.SelectedItem = selectedNV.TrangThai;
        }
        private void ClearFields()
        {
            txtHo.Clear();
            txtTen.Clear();
            txtEmail.Clear();
            txtSDT.Clear();
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            
        }
        private string GetLoaiTaiKhoanString(int maChucVu)
        {
            switch (maChucVu)
            {
                // THAY THẾ CÁC SỐ NÀY BẰNG MÃ CSDL CỦA BẠN
                case 1:
                    return "Admin";
                case 2:
                    return "NhanVien";
                case 3:
                    return "QuanLy";
                default:
                    return "NhanVien";
            }
        }
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTimKiem.Text;

                // 1. Gọi Repository để tìm kiếm (Cung cấp đủ 4 tham số)
                // Mặc định lọc: keyword, MaChucVu=0, TrangThai='All', ngayVaoLam=null
                _danhSachNhanVien = _nhanVienRepo.SearchNhanVien(keyword, 0, "All", null);

                // 2. Cập nhật DataGridView
                dgvNhanVien.DataSource = _danhSachNhanVien;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void ClearInputFields()
        {
            // Xóa trắng các TextBox
            txtHo.Text = string.Empty;
            txtTen.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtTenDangNhap.Text = string.Empty;
            txtMatKhau.Text = string.Empty;

            // --- KIỂM TRA VÀ ĐẶT COMBOBOX AN TOÀN ---

            // 1. Chức vụ: Chỉ đặt Index = 0 nếu ComboBox đã được tải dữ liệu (có ít nhất 1 mục)
            if (cbChucVu.Items.Count > 0)
            {
                cbChucVu.SelectedIndex = 0; // Chọn mục giữ chỗ "--- Chọn chức vụ ---"
            }
            else
            {
                cbChucVu.SelectedIndex = -1; // Đặt về trạng thái chưa chọn nếu trống
            }

            // 2. Trạng thái: Chỉ đặt Index = 0 nếu ComboBox đã được tải dữ liệu
            if (cbTrangThai.Items.Count > 0)
            {
                cbTrangThai.SelectedIndex = 0; // Giả sử mục 0 là '--- Chọn trạng thái ---'
            }
            else
            {
                cbTrangThai.SelectedIndex = -1; // Đặt về trạng thái chưa chọn nếu trống
            }

            // --- Đặt lại DateTimePicker ---
            dtpNgaySinh.Value = DateTime.Now;
            dtpNgayVaoLam.Value = DateTime.Now;
            if (dtpNgayVaoLam.ShowCheckBox)
            {
                dtpNgayVaoLam.Checked = false;
            }

            // Đặt con trỏ chuột về ô đầu tiên
            txtHo.Focus();
        }
        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
