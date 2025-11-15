using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace QLCuaHangNoiThat.UserControls
{
    public partial class UC_QuanLyNhanVien : UserControl
    {
        private string connectionString = "Server=localhost;Database=qlcuahangnoithat;Uid=root;Pwd=;";
        private MySqlConnection connection;

        public UC_QuanLyNhanVien()
        {
            InitializeComponent();
        }

        private void UC_QuanLyNhanVien_Load(object sender, EventArgs e)
        {
            connection = new MySqlConnection(connectionString);
            LoadNhanVien();
            LoadChucVu();
        }

        private void LoadNhanVien()
        {
            try
            {
                string query = @"
                    SELECT nv.MaNhanVien, nv.Ho, nv.Ten, nv.Email, nv.SoDienThoai, 
                           nv.TrangThai, cv.TenChucVu
                    FROM NhanVien nv
                    JOIN ChucVu cv ON nv.MaChucVu = cv.MaChucVu";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvNhanVien.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải nhân viên: " + ex.Message);
            }
        }

        private void LoadChucVu()
        {
            try
            {
                string query = "SELECT MaChucVu, TenChucVu FROM ChucVu";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                cbChucVu.DataSource = dt;
                cbChucVu.DisplayMember = "TenChucVu";
                cbChucVu.ValueMember = "MaChucVu";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chức vụ: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHo.Text) || string.IsNullOrEmpty(txtTen.Text) ||
                string.IsNullOrEmpty(txtTenDangNhap.Text) || string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc");
                return;
            }

            try
            {
                // Thêm nhân viên
                string queryNV = @"INSERT INTO NhanVien (MaChucVu, Ho, Ten, Email, SoDienThoai, TrangThai) 
                                 VALUES (@MaChucVu, @Ho, @Ten, @Email, @SDT, @TrangThai);
                                 SELECT LAST_INSERT_ID();";

                MySqlCommand cmdNV = new MySqlCommand(queryNV, connection);
                cmdNV.Parameters.AddWithValue("@MaChucVu", cbChucVu.SelectedValue);
                cmdNV.Parameters.AddWithValue("@Ho", txtHo.Text);
                cmdNV.Parameters.AddWithValue("@Ten", txtTen.Text);
                cmdNV.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmdNV.Parameters.AddWithValue("@SDT", txtSDT.Text);
                cmdNV.Parameters.AddWithValue("@TrangThai", cbTrangThai.SelectedItem.ToString());

                connection.Open();
                int maNV = Convert.ToInt32(cmdNV.ExecuteScalar());

                // Thêm tài khoản
                string queryTK = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, LoaiTaiKhoan, MaNhanVien) VALUES (@TenDangNhap, @MatKhau, @LoaiTK, @MaNV)";
                MySqlCommand cmdTK = new MySqlCommand(queryTK, connection);
                cmdTK.Parameters.AddWithValue("@TenDangNhap", txtTenDangNhap.Text);
                cmdTK.Parameters.AddWithValue("@MatKhau", txtMatKhau.Text);
                cmdTK.Parameters.AddWithValue("@LoaiTK", "NhanVien");
                cmdTK.Parameters.AddWithValue("@MaNV", maNV);
                cmdTK.ExecuteNonQuery();

                connection.Close();

                MessageBox.Show("Thêm nhân viên thành công!");
                LoadNhanVien();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm nhân viên: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow != null)
            {
                try
                {
                    string query = "UPDATE NhanVien SET MaChucVu=@MaChucVu, Ho=@Ho, Ten=@Ten, Email=@Email, SoDienThoai=@SDT, TrangThai=@TrangThai WHERE MaNhanVien=@MaNV";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MaChucVu", cbChucVu.SelectedValue);
                    cmd.Parameters.AddWithValue("@Ho", txtHo.Text);
                    cmd.Parameters.AddWithValue("@Ten", txtTen.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@SDT", txtSDT.Text);
                    cmd.Parameters.AddWithValue("@TrangThai", cbTrangThai.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@MaNV", dgvNhanVien.CurrentRow.Cells["MaNhanVien"].Value);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Cập nhật nhân viên thành công!");
                    LoadNhanVien();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi cập nhật nhân viên: " + ex.Message);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow != null)
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Xác nhận",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        string query = "DELETE FROM NhanVien WHERE MaNhanVien=@MaNV";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@MaNV", dgvNhanVien.CurrentRow.Cells["MaNhanVien"].Value);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();

                        MessageBox.Show("Xóa nhân viên thành công!");
                        LoadNhanVien();
                        ClearFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xóa nhân viên: " + ex.Message);
                    }
                }
            }
        }

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow != null)
            {
                txtHo.Text = dgvNhanVien.CurrentRow.Cells["Ho"].Value?.ToString() ?? "";
                txtTen.Text = dgvNhanVien.CurrentRow.Cells["Ten"].Value?.ToString() ?? "";
                txtEmail.Text = dgvNhanVien.CurrentRow.Cells["Email"]?.ToString() ?? "";
                txtSDT.Text = dgvNhanVien.CurrentRow.Cells["SoDienThoai"]?.ToString() ?? "";

                string chucVu = dgvNhanVien.CurrentRow.Cells["TenChucVu"]?.ToString() ?? "";
                foreach (DataRowView item in cbChucVu.Items)
                {
                    if (item["TenChucVu"].ToString() == chucVu)
                    {
                        cbChucVu.SelectedValue = item["MaChucVu"];
                        break;
                    }
                }

                string trangThai = dgvNhanVien.CurrentRow.Cells["TrangThai"]?.ToString() ?? "Active";
                cbTrangThai.SelectedItem = trangThai;
            }
        }

        private void ClearFields()
        {
            txtHo.Clear();
            txtTen.Clear();
            txtEmail.Clear();
            txtSDT.Clear();
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query = @"
                    SELECT nv.MaNhanVien, nv.Ho, nv.Ten, nv.Email, nv.SoDienThoai, 
                           nv.TrangThai, cv.TenChucVu
                    FROM NhanVien nv
                    JOIN ChucVu cv ON nv.MaChucVu = cv.MaChucVu
                    WHERE nv.Ho LIKE @Search OR nv.Ten LIKE @Search OR nv.Email LIKE @Search";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@Search", "%" + txtTimKiem.Text + "%");
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvNhanVien.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }
    }
}