using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.Repositories;
using QLCuaHangNoiThat.Services;
using QLCuaHangNoiThat.Models;
using QLCuaHangNoiThat.Forms;

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
            SetupButtonStyle(btnThemKho, Color.FromArgb(39, 174, 96));
            SetupButtonStyle(btnSuaKho, Color.FromArgb(243, 156, 18));
            SetupButtonStyle(btnXoaKho, Color.FromArgb(231, 76, 60));
            btnThemKho.Cursor = Cursors.Hand;
            btnSuaKho.Cursor = Cursors.Hand;
            btnXoaKho.Cursor = Cursors.Hand;
            // Đăng ký lắng nghe sự kiện tồn kho thay đổi
            KhoService.TonKhoChanged += KhoService_TonKhoChanged;
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
            FormKho frm = new FormKho();  // form thêm

            if (frm.ShowDialog() == DialogResult.OK)
            {
                bool ok = _khoService.ThemKho(frm.KhoInfo);

                if (ok)
                {
                    MessageBox.Show("Thêm kho thành công!");
                    LoadKho();
                }
                else
                {
                    MessageBox.Show("Không thể thêm kho!");
                }
            }
        }

        private void btnSuaKho_Click(object sender, EventArgs e)
        {
            if (dgvKho.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn kho cần sửa!");
                return;
            }

            var kho = new Kho
            {
                MaKho = Convert.ToInt32(dgvKho.CurrentRow.Cells["MaKho"].Value),
                TenKho = dgvKho.CurrentRow.Cells["TenKho"].Value.ToString(),
                DiaChi = dgvKho.CurrentRow.Cells["DiaChi"].Value.ToString()
            };

            FormKho frm = new FormKho(kho);  // form sửa

            if (frm.ShowDialog() == DialogResult.OK)
            {
                bool ok = _khoService.SuaKho(frm.KhoInfo);

                if (ok)
                {
                    MessageBox.Show("Cập nhật kho thành công!");
                    LoadKho();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật kho!");
                }
            }
        }

        private void btnXoaKho_Click(object sender, EventArgs e)
        {
            if (dgvKho.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn kho cần xóa!");
                return;
            }

            int maKho = Convert.ToInt32(dgvKho.CurrentRow.Cells["MaKho"].Value);

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa kho này?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                bool ok = _khoService.XoaKho(maKho);

                if (ok)
                {
                    MessageBox.Show("Xóa kho thành công!");
                    LoadKho();
                }
                else
                {
                    MessageBox.Show("Không thể xóa kho!");
                }
            }
        }

        private void dgvKho_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvKho.CurrentRow == null) return;

            
        }
     


        // Các hàm LoadKho, LoadNhaCungCap, CRUD NCC giữ nguyên như bạn đã viết
    }
}
