namespace QLCuaHangNoiThat.UserControls
{
    partial class UC_XuatKho
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.cboKhachHang = new System.Windows.Forms.ComboBox();
            this.cboKhoXuat = new System.Windows.Forms.ComboBox();
            this.cboSanPhamXuat = new System.Windows.Forms.ComboBox();
            this.nudSoLuongXuat = new System.Windows.Forms.NumericUpDown();
            this.txtDonGiaXuat = new System.Windows.Forms.TextBox();
            this.txtGhiChuXuat = new System.Windows.Forms.TextBox();
            this.btnThemPhieuXuat = new System.Windows.Forms.Button();
            this.dgvXuatKho = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuongXuat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvXuatKho)).BeginInit();

            // --- cboKhachHang ---
            this.cboKhachHang.Location = new System.Drawing.Point(20, 20);
            this.cboKhachHang.Name = "cboKhachHang";
            this.cboKhachHang.Size = new System.Drawing.Size(200, 24);

            // --- cboKhoXuat ---
            this.cboKhoXuat.Location = new System.Drawing.Point(240, 20);
            this.cboKhoXuat.Name = "cboKhoXuat";
            this.cboKhoXuat.Size = new System.Drawing.Size(200, 24);

            // --- cboSanPhamXuat ---
            this.cboSanPhamXuat.Location = new System.Drawing.Point(460, 20);
            this.cboSanPhamXuat.Name = "cboSanPhamXuat";
            this.cboSanPhamXuat.Size = new System.Drawing.Size(200, 24);

            // --- nudSoLuongXuat ---
            this.nudSoLuongXuat.Location = new System.Drawing.Point(680, 20);
            this.nudSoLuongXuat.Name = "nudSoLuongXuat";
            this.nudSoLuongXuat.Size = new System.Drawing.Size(80, 22);

            // --- txtDonGiaXuat ---
            this.txtDonGiaXuat.Location = new System.Drawing.Point(780, 20);
            this.txtDonGiaXuat.Name = "txtDonGiaXuat";
            this.txtDonGiaXuat.Size = new System.Drawing.Size(100, 22);

            // --- txtGhiChuXuat ---
            this.txtGhiChuXuat.Location = new System.Drawing.Point(900, 20);
            this.txtGhiChuXuat.Name = "txtGhiChuXuat";
            this.txtGhiChuXuat.Size = new System.Drawing.Size(200, 22);

            // --- btnThemPhieuXuat ---
            this.btnThemPhieuXuat.Location = new System.Drawing.Point(1120, 20);
            this.btnThemPhieuXuat.Name = "btnThemPhieuXuat";
            this.btnThemPhieuXuat.Size = new System.Drawing.Size(120, 30);
            this.btnThemPhieuXuat.Text = "Thêm phiếu xuất";

            // --- dgvXuatKho ---
            this.dgvXuatKho.Location = new System.Drawing.Point(20, 60);
            this.dgvXuatKho.Name = "dgvXuatKho";
            this.dgvXuatKho.Size = new System.Drawing.Size(1220, 400);
            this.dgvXuatKho.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // --- Add controls to UserControl ---
            this.Controls.Add(this.cboKhachHang);
            this.Controls.Add(this.cboKhoXuat);
            this.Controls.Add(this.cboSanPhamXuat);
            this.Controls.Add(this.nudSoLuongXuat);
            this.Controls.Add(this.txtDonGiaXuat);
            this.Controls.Add(this.txtGhiChuXuat);
            this.Controls.Add(this.btnThemPhieuXuat);
            this.Controls.Add(this.dgvXuatKho);

            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuongXuat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvXuatKho)).EndInit();
        }

        #endregion

        private System.Windows.Forms.ComboBox cboKhachHang;
        private System.Windows.Forms.ComboBox cboKhoXuat;
        private System.Windows.Forms.ComboBox cboSanPhamXuat;
        private System.Windows.Forms.NumericUpDown nudSoLuongXuat;
        private System.Windows.Forms.TextBox txtDonGiaXuat;
        private System.Windows.Forms.TextBox txtGhiChuXuat;
        private System.Windows.Forms.Button btnThemPhieuXuat;
        private System.Windows.Forms.DataGridView dgvXuatKho;
    }
}
