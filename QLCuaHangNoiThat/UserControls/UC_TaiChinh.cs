using System;
using System.Data;
using System.Windows.Forms;
using QLCuaHangNoiThat.Repositories;
using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.DataAccess;

namespace QLCuaHangNoiThat.UserControls
{
    public partial class UC_TaiChinh : UserControl
    {
        private readonly TaiChinhRepository _repo = new TaiChinhRepository();

        public UC_TaiChinh()
        {
            InitializeComponent();
        }

        private void UC_TaiChinh_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadNhanVienComboBox();
            UpdateFinancialSummary();

            // Thiết lập ngày mặc định
            dtpTuNgay.Value = DateTime.Now.AddDays(-30);
            dtpDenNgay.Value = DateTime.Now;
            dtpNgayGD.Value = DateTime.Now;
        }

        private void LoadData()
        {
            try
            {
                dgvTaiChinh.DataSource = _repo.GetAllGiaoDich();
                dgvTaiChinh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadNhanVienComboBox()
        {
            try
            {
                DataTable dtNhanVien = _repo.GetNhanVienList();
                cbNhanVien.Items.Clear();

                if (dtNhanVien.Rows.Count > 0)
                {
                    foreach (DataRow row in dtNhanVien.Rows)
                    {
                        cbNhanVien.Items.Add($"{row["MaNV"]} - {row["TenNV"]}");
                    }
                }
                else
                {
                    // Thêm dữ liệu mẫu nếu chưa có
                    cbNhanVien.Items.AddRange(new string[] {
                        "NV001 - Nguyễn Văn A",
                        "NV002 - Trần Thị B",
                        "NV003 - Lê Văn C"
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhân viên: {ex.Message}");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs()) return;

                string loaiGD = rbThu.Checked ? "Thu" : "Chi";
                decimal soTien = numSoTien.Value;
                string noiDung = txtNoiDung.Text.Trim();
                DateTime ngayGD = dtpNgayGD.Value;
                string nhanVien = cbNhanVien.SelectedItem?.ToString();

                if (_repo.ThemGiaoDich(loaiGD, soTien, noiDung, ngayGD, nhanVien))
                {
                    MessageBox.Show("✅ Thêm giao dịch thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    UpdateFinancialSummary();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("❌ Thêm giao dịch thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi khi thêm giao dịch: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime tuNgay = dtpTuNgay.Value;
                DateTime denNgay = dtpDenNgay.Value;
                string loaiGD = cbLoaiGD.SelectedItem?.ToString() ?? "Tất cả";

                if (tuNgay > denNgay)
                {
                    MessageBox.Show("❌ Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable filteredData = _repo.LocGiaoDich(tuNgay, denNgay, loaiGD);
                dgvTaiChinh.DataSource = filteredData;

                UpdateFinancialSummary(tuNgay, denNgay);

                MessageBox.Show($"✅ Đã lọc {filteredData.Rows.Count} giao dịch!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi khi lọc dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateFinancialSummary(DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            try
            {
                decimal tongThu = _repo.TongThu(tuNgay, denNgay);
                decimal tongChi = _repo.TongChi(tuNgay, denNgay);
                decimal conLai = tongThu - tongChi;

                lblTongThu.Text = $"{tongThu:N0} đ";
                lblTongChi.Text = $"{tongChi:N0} đ";
                lblConLai.Text = $"{conLai:N0} đ";

                // Đổi màu
                lblTongThu.ForeColor = System.Drawing.Color.Green;
                lblTongChi.ForeColor = System.Drawing.Color.Red;
                lblConLai.ForeColor = conLai >= 0 ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi khi cập nhật thống kê: {ex.Message}");
            }
        }

        private void ClearInputs()
        {
            txtNoiDung.Clear();
            numSoTien.Value = 0;
            rbThu.Checked = true;
            cbNhanVien.SelectedIndex = -1;
            dtpNgayGD.Value = DateTime.Now;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(txtNoiDung.Text.Trim()))
            {
                MessageBox.Show("❌ Vui lòng nhập nội dung giao dịch!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNoiDung.Focus();
                return false;
            }

            if (numSoTien.Value <= 0)
            {
                MessageBox.Show("❌ Số tiền phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numSoTien.Focus();
                return false;
            }

            return true;
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            dtpTuNgay.Value = DateTime.Now.AddDays(-30);
            dtpDenNgay.Value = DateTime.Now;
            cbLoaiGD.SelectedIndex = 0;
            LoadData();
            UpdateFinancialSummary();
        }
    }
}