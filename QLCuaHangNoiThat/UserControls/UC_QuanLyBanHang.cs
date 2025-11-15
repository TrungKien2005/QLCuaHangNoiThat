using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.Models;

namespace QLCuaHangNoiThat.UserControls
{
    public partial class UC_QuanLyBanHang : UserControl
    {
        private string connectionString = "Server=localhost;Database=qlcuahangnoithat;Uid=root;Pwd=;";
        private MySqlConnection connection;
        private DataTable dtChiTietHD;

        public UC_QuanLyBanHang()
        {
            InitializeComponent();
            dtChiTietHD = new DataTable();
        }

        private void UC_QuanLyBanHang_Load(object sender, EventArgs e)
        {
            connection = new MySqlConnection(connectionString);
            LoadSanPham();
            LoadHoaDon();
            LoadKhachHang();
            LoadNhanVien();
            SetupChiTietHD();
        }

        private void SetupChiTietHD()
        {
            dtChiTietHD.Columns.Add("MaSP", typeof(int));
            dtChiTietHD.Columns.Add("TenSP", typeof(string));
            dtChiTietHD.Columns.Add("SoLuong", typeof(int));
            dtChiTietHD.Columns.Add("DonGia", typeof(decimal));
            dtChiTietHD.Columns.Add("ThanhTien", typeof(decimal), "SoLuong * DonGia");
            dgvChiTietHD.DataSource = dtChiTietHD;
        }

        private void LoadSanPham()
        {
            string query = "SELECT MaSanPham, TenSanPham, GiaBan, SoLuongTon FROM SanPham WHERE SoLuongTon > 0 AND DangKinhDoanh = 1";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cbSanPham.DataSource = dt;
            cbSanPham.DisplayMember = "TenSanPham";
            cbSanPham.ValueMember = "MaSanPham";
        }

        private void LoadKhachHang()
        {
            string query = "SELECT MaKhachHang, Ho + ' ' + Ten as TenKH FROM KhachHang";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cbKhachHang.DataSource = dt;
            cbKhachHang.DisplayMember = "TenKH";
            cbKhachHang.ValueMember = "MaKhachHang";
        }

        private void LoadNhanVien()
        {
            string query = "SELECT MaNhanVien, Ho + ' ' + Ten as TenNV FROM NhanVien WHERE TrangThai = 'Active'";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cbNhanVien.DataSource = dt;
            cbNhanVien.DisplayMember = "TenNV";
            cbNhanVien.ValueMember = "MaNhanVien";
        }

        private void LoadHoaDon()
        {
            string query = @"
                SELECT dh.MaDonHang, dh.NgayDatHang, kh.Ho + ' ' + kh.Ten as TenKH, 
                       nv.Ho + ' ' + nv.Ten as TenNV, dh.TongTien, dh.TrangThai 
                FROM DonHang dh
                LEFT JOIN KhachHang kh ON dh.MaKhachHang = kh.MaKhachHang
                LEFT JOIN NhanVien nv ON dh.MaNhanVien = nv.MaNhanVien
                ORDER BY dh.MaDonHang DESC";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvHoaDon.DataSource = dt;
        }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            if (cbSanPham.SelectedValue != null && numSoLuong.Value > 0)
            {
                int maSP = (int)cbSanPham.SelectedValue;
                string tenSP = cbSanPham.Text;
                int soLuong = (int)numSoLuong.Value;
                decimal donGia = GetGiaSanPham(maSP);

                DataRow newRow = dtChiTietHD.NewRow();
                newRow["MaSP"] = maSP;
                newRow["TenSP"] = tenSP;
                newRow["SoLuong"] = soLuong;
                newRow["DonGia"] = donGia;
                dtChiTietHD.Rows.Add(newRow);

                UpdateTongTien();
            }
        }

        private decimal GetGiaSanPham(int maSP)
        {
            string query = "SELECT GiaBan FROM SanPham WHERE MaSanPham = @MaSP";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@MaSP", maSP);

            connection.Open();
            decimal gia = Convert.ToDecimal(cmd.ExecuteScalar());
            connection.Close();
            return gia;
        }

        private void UpdateTongTien()
        {
            decimal tongTien = 0;
            foreach (DataRow row in dtChiTietHD.Rows)
            {
                tongTien += Convert.ToDecimal(row["ThanhTien"]);
            }
            txtTongTien.Text = tongTien.ToString("N0");
        }

        private void btnTaoHoaDon_Click(object sender, EventArgs e)
        {
            if (dtChiTietHD.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm sản phẩm vào hóa đơn");
                return;
            }

            try
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Thêm đơn hàng
                    string queryHD = @"INSERT INTO DonHang (MaKhachHang, MaNhanVien, TongTien, DiaChiGiaoHang) 
                                     VALUES (@MaKH, @MaNV, @TongTien, @DiaChi);
                                     SELECT LAST_INSERT_ID();";

                    MySqlCommand cmdHD = new MySqlCommand(queryHD, connection, transaction);
                    cmdHD.Parameters.AddWithValue("@MaKH", cbKhachHang.SelectedValue);
                    cmdHD.Parameters.AddWithValue("@MaNV", cbNhanVien.SelectedValue);
                    cmdHD.Parameters.AddWithValue("@TongTien", decimal.Parse(txtTongTien.Text.Replace(",", "")));
                    cmdHD.Parameters.AddWithValue("@DiaChi", txtDiaChiGiaoHang.Text);

                    int maHD = Convert.ToInt32(cmdHD.ExecuteScalar());

                    // Thêm chi tiết đơn hàng
                    foreach (DataRow row in dtChiTietHD.Rows)
                    {
                        string queryCT = @"INSERT INTO ChiTietDonHang (MaDonHang, MaSanPham, SoLuong, GiaBan) 
                                         VALUES (@MaHD, @MaSP, @SoLuong, @GiaBan)";

                        MySqlCommand cmdCT = new MySqlCommand(queryCT, connection, transaction);
                        cmdCT.Parameters.AddWithValue("@MaHD", maHD);
                        cmdCT.Parameters.AddWithValue("@MaSP", row["MaSP"]);
                        cmdCT.Parameters.AddWithValue("@SoLuong", row["SoLuong"]);
                        cmdCT.Parameters.AddWithValue("@GiaBan", row["DonGia"]);
                        cmdCT.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Tạo đơn hàng thành công!");
                    dtChiTietHD.Clear();
                    txtTongTien.Text = "0";
                    LoadHoaDon();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo đơn hàng: " + ex.Message);
            }
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            if (dgvChiTietHD.CurrentRow != null)
            {
                dtChiTietHD.Rows.RemoveAt(dgvChiTietHD.CurrentRow.Index);
                UpdateTongTien();
            }
        }
    }
}