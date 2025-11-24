using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.Repositories;
using QLCuaHangNoiThat.Services;
using QLCuaHangNoiThat.Models;

namespace QLCuaHangNoiThat.UserControls
{
    public partial class UC_QuanLyKho : UserControl
    {
        private string connectionString = "Server=localhost;Database=qlcuahangnoithat;Uid=root;Pwd=;";
        private MySqlConnection connection;
        private TonKhoRepository tonKhoRepo = new TonKhoRepository();

        // Thêm dòng này:
        private KhoService _khoService = new KhoService();
        public UC_QuanLyKho()
        {
            InitializeComponent();
            this.Load += UC_QuanLyKho_Load;

            // Đăng ký lắng nghe sự kiện tồn kho thay đổi
            KhoService.TonKhoChanged += KhoService_TonKhoChanged;
        }

        private void UC_QuanLyKho_Load(object sender, EventArgs e)
        {
            connection = new MySqlConnection(connectionString);
            LoadKho();
            LoadNhaCungCap();
            LoadTonKho();
        }
        private void LoadKho()
        {
            try
            {
                var dsKho =     _khoService.GetDanhSachKho(); // giả sử bạn có hàm này trong KhoService
                dgvKho.DataSource = dsKho;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load kho: " + ex.Message);
            }
        }
        private void LoadNhaCungCap()
        {
            try
            {
                var dsNCC = _khoService.GetDanhSachNhaCungCap(); // giả sử bạn có hàm này
                dgvNhaCungCap.DataSource = dsNCC;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load nhà cung cấp: " + ex.Message);
            }
        }
        private void KhoService_TonKhoChanged()
        {
            // Đảm bảo gọi trên UI thread
            if (this.InvokeRequired)
                this.Invoke(new Action(LoadTonKho));
            else
                LoadTonKho();
        }

        private void LoadTonKho()
        {
            try
            {
                DataTable dt = tonKhoRepo.GetAllTonKho();
                dgvTonKho.DataSource = dt;

                // Tô màu cảnh báo
                foreach (DataGridViewRow row in dgvTonKho.Rows)
                {
                    if (row.IsNewRow) continue;
                    int ton = Convert.ToInt32(row.Cells["SoLuong"].Value);
                    int tonMin = Convert.ToInt32(row.Cells["TonMin"].Value);

                    if (ton < tonMin)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải tồn kho: " + ex.Message);
            }
        }
        // Thêm vào class UC_QuanLyKho

        private void btnThemNCC_Click(object sender, EventArgs e)
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
                    MessageBox.Show("Thêm nhà cung cấp thành công!");
                    LoadNhaCungCap(); // reload DataGridView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }


        private void btnSuaNCC_Click(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.CurrentRow == null) return;

            try
            {
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
                    MessageBox.Show("Cập nhật nhà cung cấp thành công!");
                    LoadNhaCungCap(); // reload DataGridView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }


        private void btnXoaNCC_Click(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.CurrentRow == null) return;

            try
            {
                int maNCC = Convert.ToInt32(dgvNhaCungCap.CurrentRow.Cells["MaNCC"].Value);
                bool ok = _khoService.XoaNhaCungCap(maNCC);
                if (ok)
                {
                    MessageBox.Show("Xóa nhà cung cấp thành công!");
                    LoadNhaCungCap();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void dgvNhaCungCap_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.CurrentRow == null) return;

            txtTenNCC.Text = dgvNhaCungCap.CurrentRow.Cells["TenNhaCungCap"].Value.ToString();
            txtNguoiLienLac.Text = dgvNhaCungCap.CurrentRow.Cells["NguoiLienLac"].Value.ToString();
            txtEmailNCC.Text = dgvNhaCungCap.CurrentRow.Cells["Email"].Value.ToString();
            txtSDTNCC.Text = dgvNhaCungCap.CurrentRow.Cells["SoDienThoai"].Value.ToString();
            txtDiaChiNCC.Text = dgvNhaCungCap.CurrentRow.Cells["DiaChi"].Value.ToString();
        }
        private void btnThemKho_Click(object sender, EventArgs e)
        {
            // Code thêm kho
            MessageBox.Show("Thêm kho được click");
        }

        private void btnSuaKho_Click(object sender, EventArgs e)
        {
            // Code sửa kho
            MessageBox.Show("Sửa kho được click");
        }

        private void btnXoaKho_Click(object sender, EventArgs e)
        {
            // Code xóa kho
            MessageBox.Show("Xóa kho được click");
        }
        private void dgvKho_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvKho.CurrentRow == null) return;

            txtTenKho.Text = dgvKho.CurrentRow.Cells["TenKho"].Value.ToString();
            txtDiaChiKho.Text = dgvKho.CurrentRow.Cells["DiaChi"].Value.ToString();
        }

        // Các hàm LoadKho, LoadNhaCungCap, CRUD NCC giữ nguyên như bạn đã viết
    }
}
