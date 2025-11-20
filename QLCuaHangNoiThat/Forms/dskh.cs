
using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.DataAccess;
using QLCuaHangNoiThat.Forms;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLCuaHangNoiThat.Forms
{
    public partial class dskh : Form
    {
        // Properties để truyền dữ liệu
        public string MaKhachHang { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string TenKhachHang { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }

        public string Email { get; set; }
        public dskh()
        {
            InitializeComponent();
        }


        private void dskh_Load_1(object sender, EventArgs e)
        {
            LoadKhachHangData();
        }

        private void LoadKhachHangData()
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT 
                            MaKhachHang,
                            Ho,
                            Ten,
                            Email,
                            SoDienThoai,
                            DiaChi,
                            NgayTao
                            FROM khachhang 
                            ORDER BY MaKhachHang DESC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Debug: In số dòng tải được
                    System.Diagnostics.Debug.WriteLine($"Số khách hàng tải được: {dt.Rows.Count}");

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.Rows.Clear();

                        foreach (DataRow row in dt.Rows)
                        {
                            try
                            {
                                dataGridView1.Rows.Add(
                                    row["MaKhachHang"],      // ColMakh
                                    row["Ho"],                // colHokh
                                    row["Ten"],               // colTenkh
                                    row["Email"],             // colEmail
                                    row["SoDienThoai"],       // colsdt
                                    row["DiaChi"],            // coldiachi
                                    row["NgayTao"]            // colngaytao
                                );
                            }
                            catch (Exception rowEx)
                            {
                                System.Diagnostics.Debug.WriteLine($"Lỗi thêm dòng: {rowEx.Message}");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("⚠️ Không có khách hàng nào trong database!\n\nHãy kiểm tra:\n1. Database có tồn tại không?\n2. Bảng 'khachhang' có dữ liệu không?",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi kết nối:\n{ex.Message}\n\nHãy kiểm tra:\n1. MySQL Server đang chạy không?\n2. Connection string đúng không?",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                try
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    string maKH = row.Cells["ColMakh"].Value?.ToString() ?? "";
                    string hoKH = row.Cells["colHokh"].Value?.ToString() ?? "";
                    string tenKH = row.Cells["colTenkh"].Value?.ToString() ?? "";
                    string soDT = row.Cells["colsdt"].Value?.ToString() ?? "";
                    string diaChi = row.Cells["coldiachi"].Value?.ToString() ?? "";
                    string email = row.Cells["colEmail"].Value?.ToString() ?? "";

                    System.Diagnostics.Debug.WriteLine($"✓ Chọn khách hàng: {maKH} - {tenKH}");

                    // Gán dữ liệu vào properties
                    this.MaKhachHang = maKH;
                    this.Ho = hoKH;
                    this.Ten = tenKH;
                    this.TenKhachHang = $"{hoKH} {tenKH}".Trim();
                    this.SoDienThoai = soDT;
                    this.DiaChi = diaChi;
                    this.Email = email;

                    // Đóng form với DialogResult.OK
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"✗ Lỗi trong CellContentClick: {ex.Message}");
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}