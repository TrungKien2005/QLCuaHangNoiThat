using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using QLCuaHangNoiThat.Models;
using QLCuaHangNoiThat.Services;

namespace QLCuaHangNoiThat.UserControls
{
    public partial class UC_QuanLyNhaCungCap : UserControl
    {
        private DataTable dtNCC; // DataTable gốc để tìm kiếm
        private readonly KhoService _khoService = new KhoService();

        public UC_QuanLyNhaCungCap()
        {
            InitializeComponent();
            this.Load += UC_QuanLyNhaCungCap_Load;
        }

        private void UC_QuanLyNhaCungCap_Load(object sender, EventArgs e)
        {
            LoadDanhSachNCC();

            // Đăng ký sự kiện
            btnThemNCC.Click += BtnThemNCC_Click;
            btnSuaNCC.Click += BtnSuaNCC_Click;
            btnXoaNCC.Click += BtnXoaNCC_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            dgvNhaCungCap.SelectionChanged += DgvNhaCungCap_SelectionChanged;
        }

        private void LoadDanhSachNCC()
        {
            dtNCC = _khoService.GetDanhSachNhaCungCap(); // giả lập DataTable
            dgvNhaCungCap.DataSource = dtNCC;
            dgvNhaCungCap.ClearSelection();
            ClearInput();
        }

        private void BtnThemNCC_Click(object sender, EventArgs e)
        {
            try
            {
                var ncc = new NhaCungCap
                {
                    TenNhaCungCap = txtTenNCC.Text,
                    NguoiLienLac = txtNguoiLienLac.Text,
                    Email = txtEmailNCC.Text,
                    SoDienThoai = txtSDTNCC.Text,
                    DiaChi = txtDiaChiNCC.Text
                };
                bool ok = _khoService.ThemNhaCungCap(ncc);
                if (ok)
                {
                    MessageBox.Show("✅ Thêm nhà cung cấp thành công!");
                    LoadDanhSachNCC();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi: " + ex.Message);
            }
        }

        private void BtnSuaNCC_Click(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.CurrentRow == null) return;
            int maNCC = Convert.ToInt32(dgvNhaCungCap.CurrentRow.Cells["MaNhaCungCap"].Value);

            var ncc = new NhaCungCap
            {
                MaNhaCungCap = maNCC,
                TenNhaCungCap = txtTenNCC.Text,
                NguoiLienLac = txtNguoiLienLac.Text,
                Email = txtEmailNCC.Text,
                SoDienThoai = txtSDTNCC.Text,
                DiaChi = txtDiaChiNCC.Text
            };
            bool ok = _khoService.SuaNhaCungCap(ncc);
            if (ok)
            {
                MessageBox.Show("✅ Cập nhật nhà cung cấp thành công!");
                LoadDanhSachNCC();
            }
        }

        private void BtnXoaNCC_Click(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.CurrentRow == null) return;
            int maNCC = Convert.ToInt32(dgvNhaCungCap.CurrentRow.Cells["MaNhaCungCap"].Value);

            var confirm = MessageBox.Show("Bạn có chắc muốn xóa nhà cung cấp này?", "Xác nhận", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            bool ok = _khoService.XoaNhaCungCap(maNCC);
            if (ok)
            {
                MessageBox.Show("✅ Xóa nhà cung cấp thành công!");
                LoadDanhSachNCC();
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                dgvNhaCungCap.DataSource = dtNCC;
                return;
            }

            var filtered = dtNCC.AsEnumerable()
                .Where(r =>
                    r["TenNhaCungCap"].ToString().ToLower().Contains(keyword) ||
                    r["NguoiLienLac"].ToString().ToLower().Contains(keyword) ||
                    r["Email"].ToString().ToLower().Contains(keyword) ||
                    r["SoDienThoai"].ToString().ToLower().Contains(keyword)
                );

            if (filtered.Any())
                dgvNhaCungCap.DataSource = filtered.CopyToDataTable();
            else
                dgvNhaCungCap.DataSource = null;
        }

        private void DgvNhaCungCap_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.CurrentRow == null) return;

            txtTenNCC.Text = dgvNhaCungCap.CurrentRow.Cells["TenNhaCungCap"].Value.ToString();
            txtNguoiLienLac.Text = dgvNhaCungCap.CurrentRow.Cells["NguoiLienLac"].Value.ToString();
            txtEmailNCC.Text = dgvNhaCungCap.CurrentRow.Cells["Email"].Value.ToString();
            txtSDTNCC.Text = dgvNhaCungCap.CurrentRow.Cells["SoDienThoai"].Value.ToString();
            txtDiaChiNCC.Text = dgvNhaCungCap.CurrentRow.Cells["DiaChi"].Value.ToString();
        }

        private void ClearInput()
        {
            txtTenNCC.Clear();
            txtNguoiLienLac.Clear();
            txtEmailNCC.Clear();
            txtSDTNCC.Clear();
            txtDiaChiNCC.Clear();
        }
    }
}
