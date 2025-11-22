using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLCuaHangNoiThat.Models;
namespace QLCuaHangNoiThat.Forms
{
    public partial class FormLichSuMuaHang : Form
    {
        private List<LichSuMuaHangView> _lichSuMuaHang;
        public FormLichSuMuaHang(List<LichSuMuaHangView> lichSu, int maKhachHang, string tenKhachHang)
        {
            InitializeComponent();
            _lichSuMuaHang = lichSu;
            SetupDataGridViewStyle(dgvDonHang);
            SetupDataGridViewStyle(dgvChiTiet);
            // 1. Đặt tiêu đề (Sử dụng tên Khách hàng đã truyền vào)
            lblTieuDe.Text = $"LỊCH SỬ MUA HÀNG CỦA KHÁCH HÀNG: {tenKhachHang} (Mã: {maKhachHang})";
            this.Text = "Chi tiết lịch sử đơn hàng"; // Tiêu đề cửa sổ
            this.Load += FormLichSuMuaHang_Load;
            LoadMasterData();
        }
        // Đặt hàm này trong FormLichSuMuaHang.cs
        private void SetupDataGridViewStyle(DataGridView dgv)
        {
            // --- 1. Loại bỏ đường viền/Hiệu ứng cũ ---
            dgv.RowHeadersVisible = false;
            dgv.BorderStyle = BorderStyle.None;
            dgv.AllowUserToResizeRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // --- 2. Cấu hình Header (Tiêu đề cột) ---
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersHeight = 35;

            // Thiết lập Font và Màu cho Header
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185); // Xanh dương đậm
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // --- 3. Cấu hình Dữ liệu và Chọn dòng ---
            // Màu xen kẽ cho hàng (Alternating Rows)
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(236, 240, 241);

            // Màu khi chọn dòng
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(179, 217, 255); // Xanh nhạt
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Đảm bảo dữ liệu căn giữa hoặc căn trái/phải hợp lý
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);

            // --- 4. Tự động kéo cột chiếm hết chiều rộng ---
            dgv.AutoGenerateColumns = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        }
        private void LoadMasterData()
        {
            // --- 2. Gán danh sách Đơn hàng (Master) vào dgvDonHang ---
            dgvDonHang.DataSource = _lichSuMuaHang;
            dgvDonHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Ẩn cột chứa List ChiTiet vì nó là đối tượng phức tạp
            if (dgvDonHang.Columns.Contains("ChiTiet"))
            {
                dgvDonHang.Columns["ChiTiet"].Visible = false;
            }

            // Định dạng ngày tháng
            if (dgvDonHang.Columns.Contains("NgayDatHang"))
            {
                dgvDonHang.Columns["NgayDatHang"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            // Định dạng tiền tệ
            if (dgvDonHang.Columns.Contains("TongTien"))
            {
                dgvDonHang.Columns["TongTien"].DefaultCellStyle.Format = "N0"; // Ví dụ: 100.000
            }

            // Cấu hình UI chung (Đảm bảo chỉ có một đơn hàng được chọn)
            dgvDonHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDonHang.MultiSelect = false;

            // 3. Đăng ký sự kiện khi người dùng chọn một dòng đơn hàng mới
            dgvDonHang.SelectionChanged += dgvDonHang_SelectionChanged;

            // Kích hoạt sự kiện lần đầu để tự động hiển thị chi tiết đơn hàng đầu tiên
            
        }
        private void FormLichSuMuaHang_Load(object sender, EventArgs e)
        {
            // 💡 CHỌN DÒNG ĐẦU TIÊN Ở ĐÂY
            if (_lichSuMuaHang != null && _lichSuMuaHang.Any())
            {
                // Tắt DataBinding tạm thời để tránh lỗi/nhấp nháy
                dgvDonHang.BindingContext = new BindingContext();

                // Chọn dòng đầu tiên
                dgvDonHang.Rows[0].Selected = true;

                // Kích hoạt sự kiện SelectionChanged để đổ Chi tiết sản phẩm
                dgvDonHang_SelectionChanged(dgvDonHang, EventArgs.Empty);

                // Khôi phục BindingContext (Nếu cần)
                dgvDonHang.BindingContext = new BindingContext();
            }
        }   
        private void dgvDonHang_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDonHang.CurrentRow == null || dgvDonHang.CurrentRow.Index < 0 || dgvDonHang.CurrentRow.DataBoundItem == null)
            {
                dgvChiTiet.DataSource = null;
                return;
            }
            // Đảm bảo có dòng đang được chọn
            if (dgvDonHang.CurrentRow == null || dgvDonHang.CurrentRow.DataBoundItem == null)
            {
                dgvChiTiet.DataSource = null; // Xóa dữ liệu cũ
                return;
            }

            // Lấy đối tượng LichSuMuaHangView (Đơn hàng) đang được chọn
            LichSuMuaHangView selectedOrder = dgvDonHang.CurrentRow.DataBoundItem as LichSuMuaHangView;

            if (selectedOrder != null)
            {
                // --- 4. Gán chi tiết (Detail) vào DataGridView thứ hai ---
                dgvChiTiet.DataSource = selectedOrder.ChiTiet;
                dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                // Định dạng tiền tệ cho Chi tiết sản phẩm
                if (dgvChiTiet.Columns.Contains("GiaBan"))
                {
                    dgvChiTiet.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
                }
                if (dgvChiTiet.Columns.Contains("ThanhTien"))
                {
                    dgvChiTiet.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
                }
            }
            else
            {
                dgvChiTiet.DataSource = null;
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
