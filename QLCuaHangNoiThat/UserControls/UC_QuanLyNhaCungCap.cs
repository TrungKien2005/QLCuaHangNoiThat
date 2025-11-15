using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLCuaHangNoiThat.UserControls
{
    public partial class UC_QuanLyNhaCungCap : UserControl
    {
        public UC_QuanLyNhaCungCap()
        {
            InitializeComponent();
        }

        private void UC_QuanLyNhaCungCap_Load(object sender, EventArgs e)
        {
            LoadData();
            SetupPlaceholder();
            SetupEvents();
        }

        private void LoadData()
        {
            try
            {
                // Tạo dữ liệu mẫu
                DataTable dt = new DataTable();
                dt.Columns.Add("MaNhaCungCap", typeof(int));
                dt.Columns.Add("TenNhaCungCap", typeof(string));
                dt.Columns.Add("NguoiLienLac", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("SoDienThoai", typeof(string));
                dt.Columns.Add("DiaChi", typeof(string));

                // Thêm dữ liệu mẫu
                dt.Rows.Add(1, "Công ty Gỗ Đẹp", "Mr. Đẹp", "godep@email.com", "0912345678", "123 Nguyễn Trãi, Hà Nội");
                dt.Rows.Add(2, "Xưởng Gỗ Việt", "Ms. Việt", "goviet@email.com", "0923456789", "456 Lê Lợi, TP.HCM");
                dt.Rows.Add(3, "Nội Thất Cao Cấp", "Mr. Cao", "caocap@email.com", "0934567890", "789 Trần Hưng Đạo, Đà Nẵng");

                dataGridViewNhaCungCap.DataSource = dt;

                // Định dạng DataGridView
                dataGridViewNhaCungCap.Columns["MaNhaCungCap"].HeaderText = "Mã NCC";
                dataGridViewNhaCungCap.Columns["TenNhaCungCap"].HeaderText = "Tên nhà cung cấp";
                dataGridViewNhaCungCap.Columns["NguoiLienLac"].HeaderText = "Người liên lạc";
                dataGridViewNhaCungCap.Columns["Email"].HeaderText = "Email";
                dataGridViewNhaCungCap.Columns["SoDienThoai"].HeaderText = "Số điện thoại";
                dataGridViewNhaCungCap.Columns["DiaChi"].HeaderText = "Địa chỉ";

                // Định dạng cột
                dataGridViewNhaCungCap.Columns["MaNhaCungCap"].Width = 80;
                dataGridViewNhaCungCap.Columns["SoDienThoai"].Width = 120;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupPlaceholder()
        {
            // Thiết lập placeholder cho ô tìm kiếm
            txtTimKiem.ForeColor = Color.Gray;
            txtTimKiem.GotFocus += (s, e) =>
            {
                if (txtTimKiem.Text == "Tìm kiếm nhà cung cấp...")
                {
                    txtTimKiem.Text = "";
                    txtTimKiem.ForeColor = Color.Black;
                }
            };

            txtTimKiem.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    txtTimKiem.Text = "Tìm kiếm nhà cung cấp...";
                    txtTimKiem.ForeColor = Color.Gray;
                }
            };
        }

        private void SetupEvents()
        {
            // Sự kiện click cho các button
            btnThem.Click += (s, e) => ThemNhaCungCap();
            btnSua.Click += (s, e) => SuaNhaCungCap();
            btnXoa.Click += (s, e) => XoaNhaCungCap();
            btnRefresh.Click += (s, e) => RefreshData();
            btnTimKiem.Click += (s, e) => TimKiemNhaCungCap();

            // Sự kiện double click trên DataGridView
            dataGridViewNhaCungCap.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    HienThiThongTinNhaCungCap(e.RowIndex);
                }
            };

            // Sự kiện Enter cho ô tìm kiếm
            txtTimKiem.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    TimKiemNhaCungCap();
                    e.Handled = true;
                }
            };
        }

        private void ThemNhaCungCap()
        {
            try
            {
                if (!ValidateInputs()) return;

                // Lấy thông tin từ form
                string tenNCC = txtTenNhaCungCap.Text.Trim();
                string nguoiLienLac = txtNguoiLienLac.Text.Trim();
                string email = txtEmail.Text.Trim();
                string soDienThoai = txtSoDienThoai.Text.Trim();
                string diaChi = txtDiaChi.Text.Trim();

                // TODO: Thêm code insert vào database ở đây
                MessageBox.Show($"Đã thêm nhà cung cấp: {tenNCC}", "Thành công",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm nhà cung cấp: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SuaNhaCungCap()
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaNhaCungCap.Text) || txtMaNhaCungCap.Text == "Tự động tạo")
                {
                    MessageBox.Show("Vui lòng chọn nhà cung cấp cần sửa!", "Thông báo",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateInputs()) return;

                // TODO: Thêm code update vào database ở đây
                MessageBox.Show($"Đã cập nhật nhà cung cấp: {txtTenNhaCungCap.Text}", "Thành công",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa nhà cung cấp: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaNhaCungCap()
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaNhaCungCap.Text) || txtMaNhaCungCap.Text == "Tự động tạo")
                {
                    MessageBox.Show("Vui lòng chọn nhà cung cấp cần xóa!", "Thông báo",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhà cung cấp {txtTenNhaCungCap.Text}?",
                                           "Xác nhận xóa",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // TODO: Thêm code delete từ database ở đây
                    MessageBox.Show($"Đã xóa nhà cung cấp: {txtTenNhaCungCap.Text}", "Thành công",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa nhà cung cấp: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TimKiemNhaCungCap()
        {
            string keyword = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(keyword) || keyword == "Tìm kiếm nhà cung cấp...")
            {
                LoadData();
                return;
            }

            // TODO: Thêm code tìm kiếm thực tế
            MessageBox.Show($"Tìm kiếm: {keyword}", "Thông báo",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RefreshData()
        {
            ClearForm();
            LoadData();
        }

        private void HienThiThongTinNhaCungCap(int rowIndex)
        {
            try
            {
                DataGridViewRow row = dataGridViewNhaCungCap.Rows[rowIndex];
                txtMaNhaCungCap.Text = row.Cells["MaNhaCungCap"].Value?.ToString() ?? "";
                txtTenNhaCungCap.Text = row.Cells["TenNhaCungCap"].Value?.ToString() ?? "";
                txtNguoiLienLac.Text = row.Cells["NguoiLienLac"].Value?.ToString() ?? "";
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value?.ToString() ?? "";
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị thông tin: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(txtTenNhaCungCap.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp!", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhaCungCap.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtNguoiLienLac.Text))
            {
                MessageBox.Show("Vui lòng nhập người liên lạc!", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNguoiLienLac.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtSoDienThoai.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDienThoai.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            txtMaNhaCungCap.Text = "Tự động tạo";
            txtTenNhaCungCap.Clear();
            txtNguoiLienLac.Clear();
            txtEmail.Clear();
            txtSoDienThoai.Clear();
            txtDiaChi.Clear();
        }
    }
}