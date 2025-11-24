namespace QLCuaHangNoiThat.UserControls
{
    partial class UC_NhapKho
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cboNhaCungCap;
        private System.Windows.Forms.ComboBox cboKhoNhap;
        private System.Windows.Forms.ComboBox cboSanPhamNhap;
        private System.Windows.Forms.NumericUpDown nudSoLuongNhap;
        private System.Windows.Forms.TextBox txtDonGiaNhap;
        private System.Windows.Forms.TextBox txtGhiChuNhap;
        private System.Windows.Forms.Button btnNhap;
        private System.Windows.Forms.DataGridView dgvNhapKho;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cboNhaCungCap = new System.Windows.Forms.ComboBox();
            this.cboKhoNhap = new System.Windows.Forms.ComboBox();
            this.cboSanPhamNhap = new System.Windows.Forms.ComboBox();
            this.nudSoLuongNhap = new System.Windows.Forms.NumericUpDown();
            this.txtDonGiaNhap = new System.Windows.Forms.TextBox();
            this.txtGhiChuNhap = new System.Windows.Forms.TextBox();
            this.btnNhap = new System.Windows.Forms.Button();
            this.dgvNhapKho = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuongNhap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhapKho)).BeginInit();

            // TODO: Set Location, Size, Name, Text của các control

            this.btnNhap.Text = "Nhập kho";
            this.btnNhap.Click += new System.EventHandler(this.btnNhap_Click);

            this.Controls.Add(this.cboNhaCungCap);
            this.Controls.Add(this.cboKhoNhap);
            this.Controls.Add(this.cboSanPhamNhap);
            this.Controls.Add(this.nudSoLuongNhap);
            this.Controls.Add(this.txtDonGiaNhap);
            this.Controls.Add(this.txtGhiChuNhap);
            this.Controls.Add(this.btnNhap);
            this.Controls.Add(this.dgvNhapKho);

            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuongNhap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhapKho)).EndInit();
        }
    }
}
