using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QLCuaHangNoiThat.Models;
using QLCuaHangNoiThat.Repositories;

namespace QLCuaHangNoiThat.UserControls
{
    public partial class FormQuanLyDanhMuc : Form
    {
        private readonly DanhMucRepository _repo = new DanhMucRepository();
        private int _currentDanhMucId = -1;

        public FormQuanLyDanhMuc()
        {
            try
            {
                InitializeComponent();
                if (!DesignMode)
                {
                    SetupDataGridView();
                    LoadDanhMuc();
                    SetupButtonStyles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo form: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            dgvDanhMuc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDanhMuc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhMuc.MultiSelect = false;
            dgvDanhMuc.ReadOnly = true;
            dgvDanhMuc.RowHeadersVisible = false;
            dgvDanhMuc.AllowUserToAddRows = false;
            dgvDanhMuc.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvDanhMuc.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(44, 62, 80),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            dgvDanhMuc.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void SetupButtonStyles()
        {
            SetupButtonStyle(btnThem, Color.FromArgb(46, 204, 113));
            SetupButtonStyle(btnSua, Color.FromArgb(52, 152, 219));
            SetupButtonStyle(btnXoa, Color.FromArgb(231, 76, 60));
            SetupButtonStyle(btnClear, Color.FromArgb(241, 196, 15));
            SetupButtonStyle(btnDong, Color.FromArgb(149, 165, 166));
        }

        private void SetupButtonStyle(Button button, Color backColor)
        {
            button.BackColor = backColor;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            button.Size = new Size(80, 30);
        }

        private void LoadDanhMuc()
        {
            try
            {
                DataTable dt = _repo.GetAll();

                // Setup columns
                dgvDanhMuc.Columns.Clear();

                DataGridViewTextBoxColumn colMaDM = new DataGridViewTextBoxColumn();
                colMaDM.Name = "MaDanhMuc";
                colMaDM.HeaderText = "Mã DM";
                colMaDM.DataPropertyName = "MaDanhMuc";
                colMaDM.Width = 80;
                dgvDanhMuc.Columns.Add(colMaDM);

                DataGridViewTextBoxColumn colTenDM = new DataGridViewTextBoxColumn();
                colTenDM.Name = "TenDanhMuc";
                colTenDM.HeaderText = "Tên Danh Mục";
                colTenDM.DataPropertyName = "TenDanhMuc";
                colTenDM.Width = 200;
                dgvDanhMuc.Columns.Add(colTenDM);

                DataGridViewTextBoxColumn colMoTa = new DataGridViewTextBoxColumn();
                colMoTa.Name = "MoTa";
                colMoTa.HeaderText = "Mô Tả";
                colMoTa.DataPropertyName = "MoTa";
                colMoTa.Width = 250;
                dgvDanhMuc.Columns.Add(colMoTa);

                dgvDanhMuc.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh mục: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtTenDanhMuc.Text = "";
            txtMoTa.Text = "";
            _currentDanhMucId = -1;
            dgvDanhMuc.ClearSelection();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTenDanhMuc.Text))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục!", "Cảnh báo",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDanhMuc.Focus();
                return false;
            }
            return true;
        }

        // ========== EVENT HANDLERS ==========

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                DanhMuc danhMuc = new DanhMuc
                {
                    TenDanhMuc = txtTenDanhMuc.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim()
                };

                bool result = _repo.Add(danhMuc);

                if (result)
                {
                    MessageBox.Show("Đã thêm danh mục thành công!", "Thành công",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadDanhMuc();
                }
                else
                {
                    MessageBox.Show("Không thể thêm danh mục!", "Lỗi",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm danh mục: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_currentDanhMucId == -1)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần sửa!", "Cảnh báo",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput()) return;

            try
            {
                DanhMuc danhMuc = new DanhMuc
                {
                    MaDanhMuc = _currentDanhMucId,
                    TenDanhMuc = txtTenDanhMuc.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim()
                };

                bool result = _repo.Update(danhMuc);

                if (result)
                {
                    MessageBox.Show("Đã cập nhật danh mục thành công!", "Thành công",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadDanhMuc();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật danh mục!", "Lỗi",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật danh mục: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_currentDanhMucId == -1)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa!", "Cảnh báo",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Kiểm tra danh mục có đang được sử dụng bởi sản phẩm nào không
                bool isInUse = _repo.IsDanhMucInUse(_currentDanhMucId);

                if (isInUse)
                {
                    MessageBox.Show("Không thể xóa danh mục này!\n\nDanh mục đang được sử dụng bởi một hoặc nhiều sản phẩm.",
                                  "Không thể xóa",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string tenDanhMuc = txtTenDanhMuc.Text;
                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa danh mục '{tenDanhMuc}'?\n\nHành động này không thể hoàn tác!",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    bool deleteResult = _repo.Delete(_currentDanhMucId);

                    if (deleteResult)
                    {
                        MessageBox.Show("Đã xóa danh mục thành công!", "Thành công",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        LoadDanhMuc();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa danh mục!", "Lỗi",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa danh mục: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dgvDanhMuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhMuc.Rows[e.RowIndex];
                _currentDanhMucId = Convert.ToInt32(row.Cells["MaDanhMuc"].Value);
                txtTenDanhMuc.Text = row.Cells["TenDanhMuc"].Value?.ToString() ?? "";
                txtMoTa.Text = row.Cells["MoTa"].Value?.ToString() ?? "";
            }
        }
    }
}