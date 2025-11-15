using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace QLCuaHangNoiThat.UserControls
{
    public partial class UC_QuanLyKhachHang : UserControl
    {
        private string connectionString = "Server=localhost;Database=qlcuahangnoithat;Uid=root;Pwd=;";
        private MySqlConnection connection;

        public UC_QuanLyKhachHang()
        {
            InitializeComponent();
        }

        private void UC_QuanLyKhachHang_Load(object sender, EventArgs e)
        {
            connection = new MySqlConnection(connectionString);
            LoadKhachHang();
        }

        private void LoadKhachHang()
        {
            try
            {
                string query = "SELECT * FROM KhachHang";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvKhachHang.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải khách hàng: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHo.Text) || string.IsNullOrEmpty(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ họ và tên");
                return;
            }

            try
            {
                string query = "INSERT INTO KhachHang (Ho, Ten, Email, SoDienThoai, DiaChi) VALUES (@Ho, @Ten, @Email, @SDT, @DiaChi)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Ho", txtHo.Text);
                cmd.Parameters.AddWithValue("@Ten", txtTen.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@SDT", txtSDT.Text);
                cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Thêm khách hàng thành công!");
                LoadKhachHang();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm khách hàng: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow != null)
            {
                try
                {
                    string query = "UPDATE KhachHang SET Ho=@Ho, Ten=@Ten, Email=@Email, SoDienThoai=@SDT, DiaChi=@DiaChi WHERE MaKhachHang=@MaKH";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Ho", txtHo.Text);
                    cmd.Parameters.AddWithValue("@Ten", txtTen.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@SDT", txtSDT.Text);
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    cmd.Parameters.AddWithValue("@MaKH", dgvKhachHang.CurrentRow.Cells["MaKhachHang"].Value);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Cập nhật khách hàng thành công!");
                    LoadKhachHang();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi cập nhật khách hàng: " + ex.Message);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow != null)
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa khách hàng này?", "Xác nhận",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        string query = "DELETE FROM KhachHang WHERE MaKhachHang=@MaKH";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@MaKH", dgvKhachHang.CurrentRow.Cells["MaKhachHang"].Value);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();

                        MessageBox.Show("Xóa khách hàng thành công!");
                        LoadKhachHang();
                        ClearFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xóa khách hàng: " + ex.Message);
                    }
                }
            }
        }

        private void dgvKhachHang_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow != null)
            {
                txtHo.Text = dgvKhachHang.CurrentRow.Cells["Ho"].Value?.ToString() ?? "";
                txtTen.Text = dgvKhachHang.CurrentRow.Cells["Ten"].Value?.ToString() ?? "";
                txtEmail.Text = dgvKhachHang.CurrentRow.Cells["Email"]?.ToString() ?? "";
                txtSDT.Text = dgvKhachHang.CurrentRow.Cells["SoDienThoai"]?.ToString() ?? "";
                txtDiaChi.Text = dgvKhachHang.CurrentRow.Cells["DiaChi"]?.ToString() ?? "";
            }
        }

        private void ClearFields()
        {
            txtHo.Clear();
            txtTen.Clear();
            txtEmail.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT * FROM KhachHang WHERE Ho LIKE @Search OR Ten LIKE @Search OR SoDienThoai LIKE @Search";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@Search", "%" + txtTimKiem.Text + "%");
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvKhachHang.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }
    }
}