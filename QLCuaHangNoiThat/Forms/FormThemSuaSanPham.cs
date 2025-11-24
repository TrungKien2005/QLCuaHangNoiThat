using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data;
using QLCuaHangNoiThat.Models;
using QLCuaHangNoiThat.Repositories;
using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.DataAccess;

namespace QLCuaHangNoiThat.UserControls
{
    public partial class FormThemSuaSanPham : Form
    {
        private SanPhamRepository sanPhamRepo = new SanPhamRepository();
        private string imagePath = "";
        private bool isEditMode = false;
        private SanPham currentSanPham = null;

        public FormThemSuaSanPham()
        {
            InitializeComponent();
            LoadDanhMuc();
        }

        public FormThemSuaSanPham(SanPham sanPham) : this()
        {
            currentSanPham = sanPham;
            isEditMode = true;
            LoadSanPhamData();
        }

        private void LoadDanhMuc()
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT MaDanhMuc, TenDanhMuc FROM danhmuc ORDER BY TenDanhMuc";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Thiết lập ComboBox
                    cboDanhMuc.DisplayMember = "TenDanhMuc";  // Hiển thị tên danh mục
                    cboDanhMuc.ValueMember = "MaDanhMuc";     // Giá trị thực là mã danh mục
                    cboDanhMuc.DataSource = dt;

                    // Thêm item mặc định (tùy chọn)
                    // DataTable newDt = dt.Copy();
                    // DataRow newRow = newDt.NewRow();
                    // newRow["MaDanhMuc"] = 0;
                    // newRow["TenDanhMuc"] = "-- Chọn danh mục --";
                    // newDt.Rows.InsertAt(newRow, 0);
                    // cboDanhMuc.DataSource = newDt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh mục: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSanPhamData()
        {
            if (currentSanPham != null)
            {
                txtTenSP.Text = currentSanPham.TenSanPham;
                txtGia.Text = currentSanPham.GiaBan.ToString();
                txtSoLuong.Text = currentSanPham.SoLuongTon.ToString();
                txtMoTa.Text = currentSanPham.MoTa;
                chkDangKinhDoanh.Checked = currentSanPham.DangKinhDoanh;

                // Chọn đúng danh mục từ ComboBox
                if (currentSanPham.MaDanhMuc > 0)
                {
                    foreach (DataRowView item in cboDanhMuc.Items)
                    {
                        if (Convert.ToInt32(item["MaDanhMuc"]) == currentSanPham.MaDanhMuc)
                        {
                            cboDanhMuc.SelectedItem = item;
                            break;
                        }
                    }
                }

                // Load hình ảnh nếu có
                if (!string.IsNullOrEmpty(currentSanPham.HinhAnhURL) && File.Exists(currentSanPham.HinhAnhURL))
                {
                    picHinhAnh.Image = Image.FromFile(currentSanPham.HinhAnhURL);
                    imagePath = currentSanPham.HinhAnhURL;
                }

                this.Text = "SỬA SẢN PHẨM";
            }
        }

        private void btnChonHinh_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    imagePath = openFileDialog.FileName;
                    picHinhAnh.Image = Image.FromFile(imagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải hình ảnh: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateForm())
                {
                    SanPham sp = new SanPham();

                    if (isEditMode && currentSanPham != null)
                    {
                        sp.MaSanPham = currentSanPham.MaSanPham;
                    }

                    sp.TenSanPham = txtTenSP.Text.Trim();
                    sp.GiaBan = decimal.Parse(txtGia.Text);
                    sp.SoLuongTon = int.Parse(txtSoLuong.Text);
                    sp.MoTa = txtMoTa.Text.Trim();
                    sp.DangKinhDoanh = chkDangKinhDoanh.Checked;

                    // Lấy MaDanhMuc từ ComboBox
                    if (cboDanhMuc.SelectedValue != null && cboDanhMuc.SelectedValue != DBNull.Value)
                    {
                        sp.MaDanhMuc = Convert.ToInt32(cboDanhMuc.SelectedValue);
                    }

                    // Xử lý hình ảnh
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        string destPath = SaveImageToDirectory(imagePath);
                        sp.HinhAnhURL = destPath;
                    }
                    else if (isEditMode && currentSanPham != null)
                    {
                        sp.HinhAnhURL = currentSanPham.HinhAnhURL;
                    }

                    bool result;
                    if (isEditMode)
                    {
                        // GỌI PHƯƠNG THỨC UPDATE THỰC TẾ
                        result = sanPhamRepo.UpdateSanPham(sp);
                        if (result)
                        {
                            MessageBox.Show("Cập nhật sản phẩm thành công!", "Thành công",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật sản phẩm thất bại!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // THÊM MỚI
                        result = sanPhamRepo.AddSanPham(sp);
                        if (result)
                        {
                            MessageBox.Show("Thêm sản phẩm thành công!", "Thành công",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Thêm sản phẩm thất bại!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string SaveImageToDirectory(string sourcePath)
        {
            try
            {
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string imageDirectory = Path.Combine(appDirectory, "img", "sanpham");

                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                string fileName = $"SP_{DateTime.Now:yyyyMMddHHmmss}_{Path.GetFileName(sourcePath)}";
                string destPath = Path.Combine(imageDirectory, fileName);

                File.Copy(sourcePath, destPath, true);
                return destPath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lưu hình ảnh: {ex.Message}");
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(txtTenSP.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSP.Focus();
                return false;
            }

            if (!decimal.TryParse(txtGia.Text, out decimal gia) || gia <= 0)
            {
                MessageBox.Show("Giá sản phẩm phải là số lớn hơn 0!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGia.Focus();
                return false;
            }

            if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong < 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên không âm!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return false;
            }

            if (cboDanhMuc.SelectedValue == null || Convert.ToInt32(cboDanhMuc.SelectedValue) == 0)
            {
                MessageBox.Show("Vui lòng chọn danh mục!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboDanhMuc.Focus();
                return false;
            }

            return true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}