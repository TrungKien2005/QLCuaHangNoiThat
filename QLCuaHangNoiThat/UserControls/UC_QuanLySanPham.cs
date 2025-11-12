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
            LoadData();
            dataGridView1.CellClick += DataGridView1_CellClick;
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
        }

        private void LoadData()
        {
            dataGridView1.DataSource = _repo.GetAll();
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

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtMaSP.Text = row.Cells["MaSP"].Value.ToString();
                txtTenSP.Text = row.Cells["TenSP"].Value.ToString();
                txtGia.Text = row.Cells["Gia"].Value.ToString();
                txtSoLuong.Text = row.Cells["SoLuong"].Value.ToString();
                txtDanhMuc.Text = row.Cells["DanhMuc"].Value.ToString();
                txtMoTa.Text = row.Cells["MoTa"].Value.ToString();
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
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
            else MessageBox.Show("❌ Thêm thất bại!");
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
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
            else MessageBox.Show("❌ Sửa thất bại!");
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSP.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!");
                return;
            }

            if (_repo.Delete(txtMaSP.Text))
            {
                MessageBox.Show("🗑️ Xóa sản phẩm thành công!");
                LoadData();
                ClearInputs();
            }
            else MessageBox.Show("❌ Xóa thất bại!");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
