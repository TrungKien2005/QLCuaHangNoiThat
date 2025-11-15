using System;
using System.Data;
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
        }

        private void UC_QuanLySanPham_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dataGridView1.DataSource = _repo.GetAll();
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
                dt.Columns.Add("MaSP", typeof(string));
                dt.Columns.Add("TenSP", typeof(string));
                dt.Columns.Add("Gia", typeof(decimal));
                dt.Columns.Add("SoLuong", typeof(int));
                dt.Columns.Add("DanhMuc", typeof(string));
                dt.Columns.Add("MoTa", typeof(string));

                dt.Rows.Add("SP001", "Ghế Sofa Gỗ", 5000000, 10, "Phòng khách", "Ghế sofa gỗ cao cấp");
                dt.Rows.Add("SP002", "Bàn Ăn 6 Ghế", 3500000, 5, "Phòng ăn", "Bàn ăn gỗ 6 chỗ ngồi");
                dt.Rows.Add("SP003", "Giường Ngủ Queen", 8000000, 3, "Phòng ngủ", "Giường ngủ size Queen");

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

                SanPham sp = new SanPham()
                {
                    MaSP = txtMaSP.Text.Trim(),
                    TenSP = txtTenSP.Text.Trim(),
                    Gia = decimal.Parse(txtGia.Text.Trim()),
                    SoLuong = int.Parse(txtSoLuong.Text.Trim()),
                    DanhMuc = txtDanhMuc.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim()
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
                    MaSP = txtMaSP.Text.Trim(),
                    TenSP = txtTenSP.Text.Trim(),
                    Gia = decimal.Parse(txtGia.Text.Trim()),
                    SoLuong = int.Parse(txtSoLuong.Text.Trim()),
                    DanhMuc = txtDanhMuc.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim()
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
                if (string.IsNullOrEmpty(keyword))
                {
                    LoadData();
                }
                else
                {
                    DataTable searchResult = _repo.Search(keyword);
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
            if (string.IsNullOrEmpty(txtMaSP.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sản phẩm!");
                txtMaSP.Focus();
                return false;
            }

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
    }
}