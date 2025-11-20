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
    public partial class capnhatttdh : Form
    {
        private long maDonHang;
        private string trangThaiHienTai;

        // Constructor nhận tham số từ form chính
        public capnhatttdh(long maDonHang, string khachHang, string trangThaiHienTai)
        {
            InitializeComponent();

            this.maDonHang = maDonHang;
            this.trangThaiHienTai = trangThaiHienTai;

            LoadThongTin(khachHang);
        }

        // Load thông tin lên form
        private void LoadThongTin(string khachHang)
        {
            // Hiển thị thông tin đơn hàng
            madh.Text = maDonHang.ToString();
            tenkh.Text = khachHang;

            // Thêm các item vào ComboBox
            trangthai.Items.Clear();
            trangthai.Items.AddRange(new object[] {
                "Chờ xử lý",
                "Đã xác nhận",
                "Đang xử lý",
                "Đang giao hàng",
                "Hoàn thành",
                "Đã hủy"
            });

            // Set trạng thái hiện tại
            trangthai.SelectedItem = trangThaiHienTai;

            // Set màu cho ComboBox theo trạng thái
            SetMauTrangThai(trangThaiHienTai);
        }

        // Event khi thay đổi trạng thái trong ComboBox
        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (trangthai.SelectedItem != null)
            {
                string trangThai = trangthai.SelectedItem.ToString();
                SetMauTrangThai(trangThai);
            }
        }

        // Đổi màu ComboBox theo trạng thái
        private void SetMauTrangThai(string trangThai)
        {
            switch (trangThai)
            {
                case "Chờ xử lý":
                    trangthai.BackColor = ColorTranslator.FromHtml("#fef3c7");
                    trangthai.ForeColor = ColorTranslator.FromHtml("#92400e");
                    break;
                case "Đã xác nhận":
                    trangthai.BackColor = ColorTranslator.FromHtml("#dbeafe");
                    trangthai.ForeColor = ColorTranslator.FromHtml("#1e40af");
                    break;
                case "Đang xử lý":
                    trangthai.BackColor = ColorTranslator.FromHtml("#e9d5ff");
                    trangthai.ForeColor = ColorTranslator.FromHtml("#7c3aed");
                    break;
                case "Đang giao hàng":
                    trangthai.BackColor = ColorTranslator.FromHtml("#bfdbfe");
                    trangthai.ForeColor = ColorTranslator.FromHtml("#1e40af");
                    break;
                case "Hoàn thành":
                    trangthai.BackColor = ColorTranslator.FromHtml("#d1fae5");
                    trangthai.ForeColor = ColorTranslator.FromHtml("#065f46");
                    break;
                case "Đã hủy":
                    trangthai.BackColor = ColorTranslator.FromHtml("#fee2e2");
                    trangthai.ForeColor = ColorTranslator.FromHtml("#991b1b");
                    break;
            }
        }

        
       
        // Hoàn tồn kho khi hủy đơn hàng
        private void HoanTonKho(MySqlConnection conn, MySqlTransaction trans)
        {
            // Lấy danh sách sản phẩm trong đơn hàng
            string queryCT = @"SELECT MaSanPham, SoLuong 
                             FROM chitietdonhang 
                             WHERE MaDonHang = @MaDH";

            List<(string masp, int soluong)> dsSP = new List<(string, int)>();

            using (MySqlCommand cmd = new MySqlCommand(queryCT, conn, trans))
            {
                cmd.Parameters.AddWithValue("@MaDH", maDonHang);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dsSP.Add((
                            reader["MaSanPham"].ToString(),
                            Convert.ToInt32(reader["SoLuong"])
                        ));
                    }
                }
            }

            // Cộng lại tồn kho
            string queryUpdateKho = @"UPDATE sanpham
                                    SET SoLuongTon = SoLuongTon + @SL
                                    WHERE MaSanPham = @MaSP";

            foreach (var item in dsSP)
            {
                using (MySqlCommand cmd = new MySqlCommand(queryUpdateKho, conn, trans))
                {
                    cmd.Parameters.AddWithValue("@SL", item.soluong);
                    cmd.Parameters.AddWithValue("@MaSP", item.masp);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void btnluu_Click_1(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra chọn trạng thái
                if (trangthai.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn trạng thái!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    trangthai.Focus();
                    return;
                }

                string trangThaiMoi = trangthai.SelectedItem.ToString();

                // 2. Kiểm tra trùng trạng thái
                if (trangThaiMoi == trangThaiHienTai)
                {
                    MessageBox.Show("Trạng thái không thay đổi!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                // 3. Xác nhận
                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn cập nhật?\n\n" +
                    $"Đơn hàng: #{maDonHang}\n" +
                    $"Từ: {trangThaiHienTai}\n" +
                    $"Sang: {trangThaiMoi}",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                // 4. Cập nhật vào database
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    MySqlTransaction trans = conn.BeginTransaction();

                    try
                    {
                        // Update trạng thái đơn hàng
                        string queryUpdate = @"UPDATE donhang 
                                             SET TrangThai = @TrangThai 
                                             WHERE MaDonHang = @MaDH";

                        using (MySqlCommand cmd = new MySqlCommand(queryUpdate, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@TrangThai", trangThaiMoi);
                            cmd.Parameters.AddWithValue("@MaDH", maDonHang);
                            cmd.ExecuteNonQuery();
                        }

                        // Nếu chuyển sang "Đã hủy" → Hoàn tồn kho
                        if (trangThaiMoi == "Đã hủy" && trangThaiHienTai != "Đã hủy")
                        {
                            HoanTonKho(conn, trans);
                        }

                        trans.Commit();

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("Lỗi cập nhật: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnhuy_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
