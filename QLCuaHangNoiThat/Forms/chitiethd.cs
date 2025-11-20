using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangNoiThat.Forms
{
    public partial class chitiethd : Form
    {
        public chitiethd()
        {
            InitializeComponent();
        }


        private long maDonHang;

        public chitiethd(long maDH) : this()
        {
            maDonHang = maDH;
            LoadChiTietDonHang();
        }



        private void LoadChiTietDonHang()
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // ===== LẤY THÔNG TIN ĐƠN HÀNG =====
                    string query = @"SELECT 
                    dh.MaDonHang, 
                    dh.NgayDatHang, 
                    dh.MaNhanVien,
                    kh.Ho, 
                    kh.Ten, 
                    kh.SoDienThoai, 
                    kh.DiaChi
                 FROM donhang dh
                 INNER JOIN khachhang kh ON dh.MaKhachHang = kh.MaKhachHang
                 WHERE dh.MaDonHang = @MaDonHang";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                label6.Text = reader["MaDonHang"].ToString();
                                label5.Text = reader["MaNhanVien"].ToString();

                                string hoTen = reader["Ho"].ToString() + " " + reader["Ten"].ToString();
                                label8.Text = hoTen;

                                label10.Text = reader["SoDienThoai"].ToString();
                                label11.Text = reader["DiaChi"].ToString();

                                label13.Text = Convert.ToDateTime(reader["NgayDatHang"]).ToString("dd/MM/yyyy");
                            }
                        }
                    }


                    // ===== LẤY CHI TIẾT SẢN PHẨM =====
                    string queryChiTiet = @"SELECT 
                                        ct.MaSanPham,
                                        sp.TenSanPham,
                                        ct.SoLuong,
                                        ct.GiaBan,
                                        ct.ThanhTien
                                    FROM chitietdonhang ct
                                    INNER JOIN sanpham sp ON ct.MaSanPham = sp.MaSanPham
                                    WHERE ct.MaDonHang = @MaDonHang";

                    using (MySqlCommand cmd = new MySqlCommand(queryChiTiet, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            dataGridView1.Rows.Clear();

                            while (reader.Read())
                            {
                                int index = dataGridView1.Rows.Add();

                                dataGridView1.Rows[index].Cells[0].Value = reader["MaSanPham"];
                                dataGridView1.Rows[index].Cells[1].Value = reader["TenSanPham"];
                                dataGridView1.Rows[index].Cells[2].Value = reader["SoLuong"];
                                dataGridView1.Rows[index].Cells[3].Value = Convert.ToDecimal(reader["GiaBan"]).ToString("N0");
                                dataGridView1.Rows[index].Cells[4].Value = Convert.ToDecimal(reader["ThanhTien"]).ToString("N0");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
    
