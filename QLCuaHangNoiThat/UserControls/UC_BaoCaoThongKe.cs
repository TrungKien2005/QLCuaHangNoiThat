using System;
using System.Windows.Forms;

namespace QLCuaHangNoiThat.UserControls
{
    public partial class UC_BaoCaoThongKe : UserControl
    {
        public UC_BaoCaoThongKe()
        {
            InitializeComponent();
        }

        private void UC_BaoCaoThongKe_Load(object sender, EventArgs e)
        {
            // Code load dữ liệu
            LoadBaoCao();
        }

        private void LoadBaoCao()
        {
            try
            {
                // TODO: Thêm code load báo cáo
            }
            catch (Exception ex) // ✅ Đang sử dụng biến ex
            {
                MessageBox.Show($"Lỗi khi tải báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXuatBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO: Thêm code xuất báo cáo
                MessageBox.Show("Chức năng đang phát triển...", "Thông báo");
            }
            catch (Exception) // ✅ Không sử dụng biến ex
            {
                MessageBox.Show("Lỗi khi xuất báo cáo", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}