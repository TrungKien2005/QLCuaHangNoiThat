using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLCuaHangNoiThat.DataAccess;
using System.Security.Cryptography;
using QLCuaHangNoiThat.Forms;

namespace QLCuaHangNoiThat.UserControls
{
    public partial class UC_QuanLyBanHang : UserControl
    {
        private string maKHChon = "";
        private bool isKhachHangMoi = true; // Flag để phân biệt khách hàng mới hay cũ
        private long maDonHangVuaTao = 0;
        public UC_QuanLyBanHang()
        {
            InitializeComponent();
        }
        //private void HideOrderButtons()
        //{
        //    // Kiểm tra xem các button có tồn tại không
        //    if (this.Controls.ContainsKey("btnInHoaDon"))
        //    {
        //        this.Controls["btnInHoaDon"].Visible = false;
        //    }
        //    if (this.Controls.ContainsKey("btnXemChiTiet"))
        //    {
        //        this.Controls["btnXemChiTiet"].Visible = false;
        //    }

        //    //maDonHangVuaTao = 0;
        //}


      


       
        private void LoadCombox_idnv()
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MaNhanVien FROM nhanvien";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            manv.Items.Clear();
                            while (reader.Read())
                            {
                                manv.Items.Add(reader["MaNhanVien"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải dữ liệu:" + ex.Message);
                }
            }
        }

        private void LoadCombox_ttsanpham()
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    String query = "SELECT MaSanPham,TenSanPham, SoLuongTon FROM sanpham";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            ttsanpham.Items.Clear();
                            while (reader.Read())
                            {
                                ttsanpham.Items.Add(reader["MaSanPham"].ToString() + "-" + reader["TenSanPham"].ToString() + "-" + reader["SoLuongTon"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối dữ liệu:" + ex.Message);
                }
            }
        }

        private void UC_QuanLyBanHang_Load(object sender, EventArgs e)
        {
            LoadCombox_idnv();
            LoadCombox_ttsanpham();
            load_ds_dh();
        }




          
        private void btn_themsp_Click(object sender, EventArgs e)
        {
            try
            {
                if (ttsanpham.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm", "thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(soluong.Text))
                {
                    MessageBox.Show("Vui lòng chọn số lượng", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string[] spInfor = ttsanpham.SelectedItem.ToString().Split('-');
                string masp = spInfor[0];
                string tenpsp = spInfor[1];
                int soluongton = int.Parse(spInfor[2]);

                int soluongmua = int.Parse(soluong.Text);

                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT GiaBan FROM sanpham where MaSanPham = @Masp";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Masp", masp);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                decimal giaban = Convert.ToDecimal(reader["GiaBan"]);
                                if (soluongmua > soluongton)
                                {
                                    MessageBox.Show($"Số lượng tồn kho chỉ còn:{soluongton}", "Thôngbao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                //Kiểm tra sản phẩm có trong danh sách chưa
                                bool found = false;
                                int rowIndex = -1;

                                foreach (DataGridViewRow row in dataGridView1.Rows)
                                {
                                    // Bỏ qua dòng trống (new row)
                                    if (row.IsNewRow) continue;

                                    // So sánh mã sản phẩm
                                    if (row.Cells["colMaSp"].Value != null &&
                                        row.Cells["colMaSp"].Value.ToString() == masp)
                                    {
                                        found = true;
                                        rowIndex = row.Index;
                                        break;
                                    }
                                }

                                if (found == true)
                                {
                                    //cập nhật số lượng
                                    int slcu = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["colSl"].Value);
                                    int slmoi = soluongmua + slcu;

                                    if (slmoi > soluongton)
                                    {
                                        MessageBox.Show($"Số lượng tồn kho chỉ còn: {soluongton}. Bạn chỉ có thể thêm {soluongton - slcu} sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }

                                    dataGridView1.Rows[rowIndex].Cells["colSL"].Value = slmoi;

                                    decimal thanhTien = giaban * slmoi;
                                    dataGridView1.Rows[rowIndex].Cells["colTT"].Value = thanhTien;
                                    MessageBox.Show($"✅ Cập nhật số lượng thành công! (Cũ: {slcu} → Mới: {slmoi})", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    // ✅ THÊM SẢN PHẨM MỚI NẾU CHƯA TỒN TẠI
                                    decimal thanhTien = giaban * soluongmua;
                                    dataGridView1.Rows.Add(
                                        masp,
                                        tenpsp,
                                        soluongmua,
                                        giaban,
                                        thanhTien
                                    );

                                    MessageBox.Show("✅ Thêm sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                soluong.Clear();
                                ttsanpham.SelectedIndex = -1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnxoasp_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DialogResult res = MessageBox.Show("Bạn có chắn muốn xóa", "xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        List<int> rowIndex = new List<int>();
                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            if (!row.IsNewRow)
                            {
                                rowIndex.Add(row.Index);
                            }
                        }

                        for (int i = rowIndex.Count - 1; i >= 0; i--)
                        {
                            dataGridView1.Rows.RemoveAt(rowIndex[i]);
                        }

                        MessageBox.Show("xóa thành công", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xóa thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();

                soluong.Clear();
                tenkhn.Clear();
                sdtkh.Clear();
                diachi.Clear();
                hokh.Clear();
                email.Clear();

                // Reset trạng thái khách hàng
                maKHChon = "";
                isKhachHangMoi = true;

                // Enable lại các textbox để có thể nhập khách hàng mới
                hokh.ReadOnly = false;
                tenkhn.ReadOnly = false;
                sdtkh.ReadOnly = false;
                diachi.ReadOnly = false;

                ttsanpham.SelectedIndex = -1;
                manv.SelectedIndex = -1;

                dateTimePicker1.Value = DateTime.Now;

                LoadCombox_idnv();
                LoadCombox_ttsanpham();


                MessageBox.Show("✅ Làm mới dữ liệu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi làm mới dữ liệu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private decimal TongTien()
        {
            decimal TongTien = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow && row.Cells["colTT"].Value != null)
                {
                    TongTien += Convert.ToDecimal(row.Cells["colTT"].Value);
                }
            }
            return TongTien;
        }

        private void btntaodh_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (manv.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(hokh.Text) || string.IsNullOrWhiteSpace(tenkhn.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ họ tên khách hàng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(sdtkh.Text))
                {
                    MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(diachi.Text))
                {
                    MessageBox.Show("Vui lòng nhập địa chỉ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dataGridView1.Rows.Count == 0 || (dataGridView1.Rows.Count == 1 && dataGridView1.Rows[0].IsNewRow))
                {
                    MessageBox.Show("Vui lòng thêm ít nhất một sản phẩm vào đơn hàng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal tongtien = TongTien();

                if (tongtien <= 0)
                {
                    MessageBox.Show("Tổng tiền không hợp lệ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xác nhận tạo đơn hàng
                DialogResult result = MessageBox.Show(
                    $"Xác nhận tạo đơn hàng?\n\n" +
                    $"Khách hàng: {hokh.Text} {tenkhn.Text}\n" +
                    $"Loại: {(isKhachHangMoi ? "Khách hàng MỚI" : "Khách hàng CŨ")}\n" +
                    $"Tổng tiền: {tongtien:N0} VNĐ",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    return;
                }

                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    MySqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        string maKH;

                        // ===== XỬ LÝ KHÁCH HÀNG =====
                        if (isKhachHangMoi)
                        {
                            // Thêm khách hàng mới vào database
                            string queryKH = @"INSERT INTO khachhang 
                                (Ho, Ten, SoDienThoai, Email, DiaChi, NgayTao) 
                                VALUES (@Ho, @Ten, @SDT, @Email, @DiaChi, @NgayTao)";

                            using (MySqlCommand cmdKH = new MySqlCommand(queryKH, conn, transaction))
                            {
                                cmdKH.Parameters.AddWithValue("@Ho", hokh.Text.Trim());
                                cmdKH.Parameters.AddWithValue("@Ten", tenkhn.Text.Trim());
                                cmdKH.Parameters.AddWithValue("@SDT", sdtkh.Text.Trim());
                                cmdKH.Parameters.AddWithValue("@Email", email.Text.Trim()); // Hoặc lấy từ textbox nếu có
                                cmdKH.Parameters.AddWithValue("@DiaChi", diachi.Text.Trim());
                                cmdKH.Parameters.AddWithValue("@NgayTao", DateTime.Now);

                                cmdKH.ExecuteNonQuery();
                                maKH = cmdKH.LastInsertedId.ToString();
                            }
                        }
                        else
                        {
                            // Sử dụng mã khách hàng đã chọn
                            maKH = maKHChon;
                        }

                        // ===== 1. Thêm đơn hàng =====
                        string queryDonHang = @"INSERT INTO donhang 
                            (MaKhachHang, MaNhanVien, NgayDatHang, TongTien, TrangThai, DiaChiGiaoHang) 
                            VALUES (@MaKH, @MaNV, @NgayDatHang, @TongTien, @TrangThai, @DiaChi)";

                        long maDonHang = 0;

                        using (MySqlCommand cmd = new MySqlCommand(queryDonHang, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@MaKH", maKH);
                            cmd.Parameters.AddWithValue("@MaNV", manv.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@NgayDatHang", dateTimePicker1.Value);
                            cmd.Parameters.AddWithValue("@TongTien", tongtien);
                            cmd.Parameters.AddWithValue("@TrangThai", "Chờ xử lý");
                            cmd.Parameters.AddWithValue("@DiaChi", diachi.Text.Trim());

                            cmd.ExecuteNonQuery();
                            maDonHang = cmd.LastInsertedId;
                        }

                        // ===== 2. Thêm chi tiết đơn hàng và cập nhật tồn kho =====
                        string queryChiTiet = @"INSERT INTO chitietdonhang 
                            (MaDonHang, MaSanPham, SoLuong, GiaBan, ThanhTien) 
                            VALUES (@MaDH, @MaSP, @SoLuong, @DonGia, @ThanhTien)";

                        string queryUpdateKho = @"UPDATE sanpham 
                            SET SoLuongTon = SoLuongTon - @SoLuong 
                            WHERE MaSanPham = @MaSP";

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.IsNewRow) continue;

                            string maSP = row.Cells["colMaSp"].Value.ToString();
                            int soLuong = Convert.ToInt32(row.Cells["colSL"].Value);
                            decimal giaBan = Convert.ToDecimal(row.Cells["colGia"].Value);
                            decimal thanhTien = Convert.ToDecimal(row.Cells["colTT"].Value);

                            // Thêm chi tiết đơn hàng
                            using (MySqlCommand cmdChiTiet = new MySqlCommand(queryChiTiet, conn, transaction))
                            {
                                cmdChiTiet.Parameters.AddWithValue("@MaDH", maDonHang);
                                cmdChiTiet.Parameters.AddWithValue("@MaSP", maSP);
                                cmdChiTiet.Parameters.AddWithValue("@SoLuong", soLuong);
                                cmdChiTiet.Parameters.AddWithValue("@DonGia", giaBan);
                                cmdChiTiet.Parameters.AddWithValue("@ThanhTien", thanhTien);
                                cmdChiTiet.ExecuteNonQuery();
                            }

                            // Cập nhật tồn kho
                            using (MySqlCommand cmdUpdateKho = new MySqlCommand(queryUpdateKho, conn, transaction))
                            {
                                cmdUpdateKho.Parameters.AddWithValue("@SoLuong", soLuong);
                                cmdUpdateKho.Parameters.AddWithValue("@MaSP", maSP);
                                cmdUpdateKho.ExecuteNonQuery();
                            }
                        }

                        // 3. Commit transaction
                        transaction.Commit();

                        maDonHangVuaTao = maDonHang;

                        MessageBox.Show(
                            $"✅ Tạo đơn hàng thành công!\n\n" +
                            $"Mã đơn hàng: {maDonHang}\n" +
                            $"Mã khách hàng: {maKH}\n" +
                            $"Tổng tiền: {tongtien:N0} VNĐ",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        LoadCombox_ttsanpham();
                        load_ds_dh();

                    }
                    catch (Exception ex)
                    {
                        // Rollback nếu có lỗi
                        transaction.Rollback();
                        throw new Exception("Lỗi trong quá trình tạo đơn hàng: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo đơn hàng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chon_kh_Click(object sender, EventArgs e)
        {
            dskh ds = new dskh();

            if (ds.ShowDialog() == DialogResult.OK)
            {
                // Khách hàng đã tồn tại - đánh dấu là khách hàng cũ
                maKHChon = ds.MaKhachHang;
                isKhachHangMoi = false;

                // Lấy dữ liệu khách hàng đã chọn từ form dskh
                hokh.Text = ds.Ho;
                tenkhn.Text = ds.Ten;
                sdtkh.Text = ds.SoDienThoai;
                diachi.Text = ds.DiaChi;
                email.Text = ds.Email;

                // Khóa các textbox để không chỉnh sửa thông tin khách hàng cũ
                hokh.ReadOnly = true;
                tenkhn.ReadOnly = true;
                sdtkh.ReadOnly = true;
                diachi.ReadOnly = true;

                MessageBox.Show($"✅ Chọn khách hàng thành công!\n\nMã KH: {ds.MaKhachHang}\nTên: {ds.TenKhachHang}\nSĐT: {ds.SoDienThoai}\nĐịa chỉ: {ds.DiaChi}",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnInhd_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra có dòng nào được chọn không
                if (dgvDonHang.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn đơn hàng cần in!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Lấy mã đơn hàng từ dòng được chọn
                long maDonHang = Convert.ToInt64(dgvDonHang.SelectedRows[0].Cells["MaDonHang"].Value);

                // Mở form in hóa đơn và truyền mã đơn hàng
                inhoadon1 formInHD = new inhoadon1(maDonHang);
                formInHD.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnchitiethd_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra có dòng nào được chọn không
                if (dgvDonHang.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn đơn hàng cần in!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Lấy mã đơn hàng từ dòng được chọn
                long maDonHang = Convert.ToInt64(dgvDonHang.SelectedRows[0].Cells["MaDonHang"].Value);

                // Mở form in hóa đơn và truyền mã đơn hàng
                chitiethd ct = new chitiethd(maDonHang);
                ct.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }





        private void load_ds_dh()
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT dh.MaDonHang, dh.NgayDatHang, 
                    CONCAT(kh.Ho, ' ', kh.Ten) AS TenKhachHang,
                    kh.SoDienThoai, dh.DiaChiGiaoHang, dh.TongTien, dh.TrangThai
             FROM donhang dh
             JOIN khachhang kh ON dh.MaKhachHang = kh.MaKhachHang
             ORDER BY dh.NgayDatHang DESC";

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // ✅ Xóa tất cả cột cũ
                        dgvDonHang.Columns.Clear();

                        // ✅ Cho phép tự động tạo cột
                        dgvDonHang.AutoGenerateColumns = true;

                        // Gán DataSource
                        dgvDonHang.DataSource = dt;

                        // Đặt tiêu đề cột
                        dgvDonHang.Columns["MaDonHang"].HeaderText = "Mã Đơn Hàng";
                        dgvDonHang.Columns["NgayDatHang"].HeaderText = "Ngày Đặt";
                        dgvDonHang.Columns["TenKhachHang"].HeaderText = "Khách Hàng";
                        dgvDonHang.Columns["SoDienThoai"].HeaderText = "SĐT";
                        dgvDonHang.Columns["DiaChiGiaoHang"].HeaderText = "Địa Chỉ";
                        dgvDonHang.Columns["TongTien"].HeaderText = "Tổng Tiền";
                        dgvDonHang.Columns["TrangThai"].HeaderText = "Trạng Thái";

                        // Format cột Tổng tiền
                        dgvDonHang.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải danh sách đơn hàng: " + ex.Message,
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnhuydh_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra chọn dòng
                if (dgvDonHang.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn đơn hàng cần hủy!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                long maDonHang = Convert.ToInt64(
                    dgvDonHang.SelectedRows[0].Cells["MaDonHang"].Value);

                string trangThai = dgvDonHang.SelectedRows[0].Cells["TrangThai"].Value.ToString();

                // 2. Kiểm tra nếu đã hủy
                if (trangThai == "Đã hủy")
                {
                    MessageBox.Show("Đơn hàng này đã được hủy trước đó.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 3. Xác nhận
                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc muốn hủy đơn hàng {maDonHang} không?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                    return;

                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    MySqlTransaction trans = conn.BeginTransaction();

                    try
                    {
                        // 4. Lấy danh sách sản phẩm trong đơn hàng
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

                        // 5. Cộng lại tồn kho
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

                        // 6. Cập nhật trạng thái đơn hàng
                        string queryHuy = @"UPDATE donhang 
                                    SET TrangThai = 'Đã hủy' 
                                    WHERE MaDonHang = @MaDH";

                        using (MySqlCommand cmd = new MySqlCommand(queryHuy, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@MaDH", maDonHang);
                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();

                        MessageBox.Show("Hủy đơn hàng thành công!",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        load_ds_dh(); // load lại danh sách
                        LoadCombox_ttsanpham();

                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Lỗi khi hủy đơn hàng: " + ex.Message,
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message,
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void capnhattt_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra có chọn đơn hàng không
                if (dgvDonHang.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn đơn hàng cần cập nhật trạng thái!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Lấy thông tin đơn hàng được chọn
                long maDonHang = Convert.ToInt64(dgvDonHang.SelectedRows[0].Cells["MaDonHang"].Value);
                string tenKhachHang = dgvDonHang.SelectedRows[0].Cells["TenKhachHang"].Value.ToString();
                string trangThaiHienTai = dgvDonHang.SelectedRows[0].Cells["TrangThai"].Value.ToString();

                // 3. Mở form cập nhật trạng thái
                capnhatttdh formCapNhat = new capnhatttdh(
                    maDonHang,
                    tenKhachHang,
                    trangThaiHienTai
                );

                // 4. Nếu người dùng nhấn Lưu thành công
                if (formCapNhat.ShowDialog() == DialogResult.OK)
                {
                    // Reload lại danh sách đơn hàng
                    load_ds_dh();
                    LoadCombox_ttsanpham(); // Refresh sản phẩm nếu có thay đổi tồn kho

                    MessageBox.Show("✅ Cập nhật trạng thái thành công!",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}