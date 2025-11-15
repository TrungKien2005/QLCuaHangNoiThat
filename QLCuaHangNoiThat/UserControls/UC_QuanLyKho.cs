using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace QLCuaHangNoiThat.UserControls
{
    public partial class UC_QuanLyKho : UserControl
    {
        private string connectionString = "Server=localhost;Database=qlcuahangnoithat;Uid=root;Pwd=;";
        private MySqlConnection connection;

        public UC_QuanLyKho()
        {
            InitializeComponent();
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
                string query = "SELECT * FROM Kho";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvKho.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu kho: " + ex.Message);
            }
        }

        private void LoadTonKho()
        {
            try
            {
                string query = @"
                    SELECT sp.MaSanPham, sp.TenSanPham, sp.SoLuongTon, k.TenKho
                    FROM SanPham sp
                    LEFT JOIN TonKho tk ON sp.MaSanPham = tk.MaSanPham
                    LEFT JOIN Kho k ON tk.MaKho = k.MaKho
                    ORDER BY sp.SoLuongTon ASC";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvTonKho.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải tồn kho: " + ex.Message);
            }
        }

        private void LoadNhaCungCap()
        {
            try
            {
                string query = "SELECT * FROM NhaCungCap";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvNhaCungCap.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải nhà cung cấp: " + ex.Message);
            }
        }

        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenNCC.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp");
                return;
            }

            try
            {
                string query = "INSERT INTO NhaCungCap (TenNhaCungCap, NguoiLienLac, Email, SoDienThoai, DiaChi) VALUES (@TenNCC, @NguoiLienLac, @Email, @SDT, @DiaChi)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@TenNCC", txtTenNCC.Text);
                cmd.Parameters.AddWithValue("@NguoiLienLac", txtNguoiLienLac.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmailNCC.Text);
                cmd.Parameters.AddWithValue("@SDT", txtSDTNCC.Text);
                cmd.Parameters.AddWithValue("@DiaChi", txtDiaChiNCC.Text);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Thêm nhà cung cấp thành công!");
                LoadNhaCungCap();
                ClearNCCFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm nhà cung cấp: " + ex.Message);
            }
        }

        private void btnSuaNCC_Click(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.CurrentRow != null)
            {
                try
                {
                    string query = "UPDATE NhaCungCap SET TenNhaCungCap=@TenNCC, NguoiLienLac=@NguoiLienLac, Email=@Email, SoDienThoai=@SDT, DiaChi=@DiaChi WHERE MaNhaCungCap=@MaNCC";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@TenNCC", txtTenNCC.Text);
                    cmd.Parameters.AddWithValue("@NguoiLienLac", txtNguoiLienLac.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmailNCC.Text);
                    cmd.Parameters.AddWithValue("@SDT", txtSDTNCC.Text);
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiaChiNCC.Text);
                    cmd.Parameters.AddWithValue("@MaNCC", dgvNhaCungCap.CurrentRow.Cells["MaNhaCungCap"].Value);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Cập nhật nhà cung cấp thành công!");
                    LoadNhaCungCap();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi cập nhật nhà cung cấp: " + ex.Message);
                }
            }
        }

        private void btnXoaNCC_Click(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.CurrentRow != null)
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa nhà cung cấp này?", "Xác nhận",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        string query = "DELETE FROM NhaCungCap WHERE MaNhaCungCap=@MaNCC";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@MaNCC", dgvNhaCungCap.CurrentRow.Cells["MaNhaCungCap"].Value);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();

                        MessageBox.Show("Xóa nhà cung cấp thành công!");
                        LoadNhaCungCap();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xóa nhà cung cấp: " + ex.Message);
                    }
                }
            }
        }

        private void dgvNhaCungCap_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.CurrentRow != null)
            {
                txtTenNCC.Text = dgvNhaCungCap.CurrentRow.Cells["TenNhaCungCap"].Value.ToString();
                txtNguoiLienLac.Text = dgvNhaCungCap.CurrentRow.Cells["NguoiLienLac"]?.ToString() ?? "";
                txtEmailNCC.Text = dgvNhaCungCap.CurrentRow.Cells["Email"]?.ToString() ?? "";
                txtSDTNCC.Text = dgvNhaCungCap.CurrentRow.Cells["SoDienThoai"]?.ToString() ?? "";
                txtDiaChiNCC.Text = dgvNhaCungCap.CurrentRow.Cells["DiaChi"]?.ToString() ?? "";
            }
        }

        private void ClearNCCFields()
        {
            txtTenNCC.Clear();
            txtNguoiLienLac.Clear();
            txtEmailNCC.Clear();
            txtSDTNCC.Clear();
            txtDiaChiNCC.Clear();
        }
    }
}