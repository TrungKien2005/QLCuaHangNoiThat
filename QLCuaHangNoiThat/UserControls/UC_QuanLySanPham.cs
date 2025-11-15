using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QLCuaHangNoiThat.Models;
using QLCuaHangNoiThat.Repositories;

namespace QLCuaHangNoiThat.UserControls
{
    public partial class UC_QuanLySanPham : UserControl
    {
        private readonly SanPhamRepository _repo = new SanPhamRepository();

        public UC_QuanLySanPham()
        {
            InitializeComponent();
            InitializePlaceholder();
            InitializeEvents();
        }

        // THÊM PHƯƠNG THỨC NÀY VÀO ĐÂY
        private void InitializeEvents()
        {
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
        }

        private void UC_QuanLySanPham_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = _repo.GetAll();

                // Đổi tên cột để khớp với UI
                if (dt.Columns.Contains("MaSanPham")) dt.Columns["MaSanPham"].ColumnName = "MaSP";
                if (dt.Columns.Contains("TenSanPham")) dt.Columns["TenSanPham"].ColumnName = "TenSP";
                if (dt.Columns.Contains("GiaBan")) dt.Columns["GiaBan"].ColumnName = "Gia";
                if (dt.Columns.Contains("SoLuongTon")) dt.Columns["SoLuongTon"].ColumnName = "SoLuong";
                if (dt.Columns.Contains("TenDanhMuc")) dt.Columns["TenDanhMuc"].ColumnName = "DanhMuc";

                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowSampleData();
            }
        }

        private void ShowSampleData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("MaSP", typeof(int));
                dt.Columns.Add("TenSP", typeof(string));
                dt.Columns.Add("Gia", typeof(decimal));
                dt.Columns.Add("SoLuong", typeof(int));
                dt.Columns.Add("DanhMuc", typeof(string));
                dt.Columns.Add("MoTa", typeof(string));

                dt.Rows.Add(1, "Ghế Sofa Gỗ", 5000000, 10, "Phòng khách", "Ghế sofa gỗ cao cấp");
                dt.Rows.Add(2, "Bàn Ăn 6 Ghế", 3500000, 5, "Phòng ăn", "Bàn ăn gỗ 6 chỗ ngồi");
                dt.Rows.Add(3, "Giường Ngủ Queen", 8000000, 3, "Phòng ngủ", "Giường ngủ size Queen");

                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                MessageBox.Show("Đang hiển thị dữ liệu mẫu. Vui lòng kiểm tra kết nối database!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nghiêm trọng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                txtMaSP.Text = row.Cells["MaSP"]?.Value?.ToString() ?? "";
                txtTenSP.Text = row.Cells["TenSP"]?.Value?.ToString() ?? "";
                txtGia.Text = row.Cells["Gia"]?.Value?.ToString() ?? "0";
                txtSoLuong.Text = row.Cells["SoLuong"]?.Value?.ToString() ?? "0";
                txtDanhMuc.Text = row.Cells["DanhMuc"]?.Value?.ToString() ?? "";
                txtMoTa.Text = row.Cells["MoTa"]?.Value?.ToString() ?? "";
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs()) return;

                // Tạo đối tượng SanPham mới với Model thực tế
                SanPham sp = new SanPham()
                {
                    TenSanPham = txtTenSP.Text.Trim(),
                    GiaBan = decimal.Parse(txtGia.Text.Trim()),
                    SoLuongTon = int.Parse(txtSoLuong.Text.Trim()),
                    MoTa = txtMoTa.Text.Trim(),
                    MaDanhMuc = GetMaDanhMucByName(txtDanhMuc.Text.Trim()), // Cần tạo hàm này
                    GiaNhap = decimal.Parse(txtGia.Text.Trim()) * 0.7m, // Giá nhập = 70% giá bán
                    DangKinhDoanh = true
                };

                if (_repo.Add(sp))
                {
                    MessageBox.Show("✅ Thêm sản phẩm thành công!");
                    LoadData();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaSP.Text))
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!");
                    return;
                }

                if (!ValidateInputs()) return;

                SanPham sp = new SanPham()
                {
                    MaSanPham = int.Parse(txtMaSP.Text.Trim()),
                    TenSanPham = txtTenSP.Text.Trim(),
                    GiaBan = decimal.Parse(txtGia.Text.Trim()),
                    SoLuongTon = int.Parse(txtSoLuong.Text.Trim()),
                    MoTa = txtMoTa.Text.Trim(),
                    MaDanhMuc = GetMaDanhMucByName(txtDanhMuc.Text.Trim())
                };

                if (_repo.Update(sp))
                {
                    MessageBox.Show("✏️ Cập nhật sản phẩm thành công!");
                    LoadData();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("❌ Sửa thất bại! Sản phẩm không tồn tại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSP.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!");
                return;
            }

            var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm {txtMaSP.Text}?",
                                       "Xác nhận xóa",
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (_repo.Delete(txtMaSP.Text))
                    {
                        MessageBox.Show("🗑️ Xóa sản phẩm thành công!");
                        LoadData();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("❌ Xóa thất bại! Sản phẩm không tồn tại.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ Lỗi khi xóa sản phẩm: {ex.Message}");
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTimKiem.Text.Trim();
                if (string.IsNullOrEmpty(keyword) || keyword == "Tìm kiếm sản phẩm...")
                {
                    LoadData();
                }
                else
                {
                    DataTable searchResult = _repo.Search(keyword);

                    // Đổi tên cột cho kết quả tìm kiếm
                    if (searchResult.Columns.Contains("MaSanPham")) searchResult.Columns["MaSanPham"].ColumnName = "MaSP";
                    if (searchResult.Columns.Contains("TenSanPham")) searchResult.Columns["TenSanPham"].ColumnName = "TenSP";
                    if (searchResult.Columns.Contains("GiaBan")) searchResult.Columns["GiaBan"].ColumnName = "Gia";
                    if (searchResult.Columns.Contains("SoLuongTon")) searchResult.Columns["SoLuongTon"].ColumnName = "SoLuong";
                    if (searchResult.Columns.Contains("TenDanhMuc")) searchResult.Columns["TenDanhMuc"].ColumnName = "DanhMuc";

                    dataGridView1.DataSource = searchResult;

                    if (searchResult.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy sản phẩm nào!", "Thông báo",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}");
            }
        }

        private void ClearInputs()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            txtGia.Clear();
            txtSoLuong.Clear();
            txtDanhMuc.Clear();
            txtMoTa.Clear();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(txtTenSP.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!");
                txtTenSP.Focus();
                return false;
            }

            if (!decimal.TryParse(txtGia.Text, out decimal gia) || gia <= 0)
            {
                MessageBox.Show("Vui lòng nhập giá hợp lệ!");
                txtGia.Focus();
                return false;
            }

            if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong < 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ!");
                txtSoLuong.Focus();
                return false;
            }

            return true;
        }

        // Hàm hỗ trợ: Lấy MaDanhMuc từ tên danh mục
        private int GetMaDanhMucByName(string tenDanhMuc)
        {
            // Tạm thời trả về giá trị mặc định
            // Bạn có thể tạo Repository cho DanhMuc để lấy chính xác
            switch (tenDanhMuc.ToLower())
            {
                case "phòng khách": return 1;
                case "phòng ngủ": return 2;
                case "phòng bếp": return 3;
                default: return 1; // Mặc định là Phòng khách
            }
        }

        private void InitializePlaceholder()
        {
            // Code placeholder giữ nguyên
            txtTimKiem.Text = "Tìm kiếm sản phẩm...";
            txtTimKiem.ForeColor = Color.Gray;

            txtTimKiem.GotFocus += (s, e) =>
            {
                if (txtTimKiem.Text == "Tìm kiếm sản phẩm...")
                {
                    txtTimKiem.Text = "";
                    txtTimKiem.ForeColor = SystemColors.WindowText;
                }
            };

            txtTimKiem.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    txtTimKiem.Text = "Tìm kiếm sản phẩm...";
                    txtTimKiem.ForeColor = Color.Gray;
                }
            };
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {

        }
    }
}