using System;
using System.Windows.Forms;
using QLCuaHangNoiThat.Models;

namespace QLCuaHangNoiThat.Forms
{
    public partial class FormKho : Form
    {
        public Kho KhoInfo { get; private set; }

        // Form thêm
        public FormKho()
        {
            InitializeComponent();
        }

        // Form sửa
        public FormKho(Kho kho) : this()
        {
            KhoInfo = kho;

            txtTenKho.Text = kho.TenKho;
            txtDiaChiKho.Text = kho.DiaChi;
            txtSoDienThoaiLienHe.Text = kho.SoDienThoaiLienHe;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            KhoInfo = new Kho
            {
                MaKho = KhoInfo?.MaKho ?? 0,
                TenKho = txtTenKho.Text.Trim(),
                DiaChi = txtDiaChiKho.Text.Trim(),
                SoDienThoaiLienHe = txtSoDienThoaiLienHe.Text.Trim()
            };

            this.DialogResult = DialogResult.OK;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
