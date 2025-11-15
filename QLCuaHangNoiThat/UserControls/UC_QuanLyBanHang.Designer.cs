namespace QLCuaHangNoiThat.UserControls
{
    partial class UC_QuanLyBanHang
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.DataGridView dgvHoaDon;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.DataGridView dgvChiTietHD;
        private System.Windows.Forms.ComboBox cbSanPham;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.Button btnThemSP;
        private System.Windows.Forms.Button btnTaoHoaDon;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbKhachHang;
        private System.Windows.Forms.ComboBox cbNhanVien;
        private System.Windows.Forms.Button btnXoaSP;
        private System.Windows.Forms.TextBox txtDiaChiGiaoHang;

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
            this.panelLeft = new System.Windows.Forms.Panel();
            this.dgvHoaDon = new System.Windows.Forms.DataGridView();
            this.panelRight = new System.Windows.Forms.Panel();
            this.txtDiaChiGiaoHang = new System.Windows.Forms.TextBox();
            this.btnTaoHoaDon = new System.Windows.Forms.Button();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvChiTietHD = new System.Windows.Forms.DataGridView();
            this.btnXoaSP = new System.Windows.Forms.Button();
            this.btnThemSP = new System.Windows.Forms.Button();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.cbSanPham = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbNhanVien = new System.Windows.Forms.ComboBox();
            this.cbKhachHang = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietHD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            this.SuspendLayout();

            // panelLeft
            this.panelLeft.Controls.Add(this.dgvHoaDon);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(400, 450);
            this.panelLeft.TabIndex = 0;

            // dgvHoaDon
            this.dgvHoaDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHoaDon.Location = new System.Drawing.Point(0, 0);
            this.dgvHoaDon.Name = "dgvHoaDon";
            this.dgvHoaDon.Size = new System.Drawing.Size(400, 450);
            this.dgvHoaDon.TabIndex = 0;

            // panelRight
            this.panelRight.Controls.Add(this.txtDiaChiGiaoHang);
            this.panelRight.Controls.Add(this.btnTaoHoaDon);
            this.panelRight.Controls.Add(this.txtTongTien);
            this.panelRight.Controls.Add(this.label5);
            this.panelRight.Controls.Add(this.dgvChiTietHD);
            this.panelRight.Controls.Add(this.btnXoaSP);
            this.panelRight.Controls.Add(this.btnThemSP);
            this.panelRight.Controls.Add(this.numSoLuong);
            this.panelRight.Controls.Add(this.cbSanPham);
            this.panelRight.Controls.Add(this.label4);
            this.panelRight.Controls.Add(this.label3);
            this.panelRight.Controls.Add(this.label2);
            this.panelRight.Controls.Add(this.cbNhanVien);
            this.panelRight.Controls.Add(this.cbKhachHang);
            this.panelRight.Controls.Add(this.label1);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(400, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(400, 450);
            this.panelRight.TabIndex = 1;

            // txtDiaChiGiaoHang
            this.txtDiaChiGiaoHang.Location = new System.Drawing.Point(100, 92);
            this.txtDiaChiGiaoHang.Name = "txtDiaChiGiaoHang";
            this.txtDiaChiGiaoHang.Size = new System.Drawing.Size(290, 20);
            this.txtDiaChiGiaoHang.TabIndex = 15;

            // btnTaoHoaDon
            this.btnTaoHoaDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnTaoHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaoHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnTaoHoaDon.Location = new System.Drawing.Point(250, 410);
            this.btnTaoHoaDon.Name = "btnTaoHoaDon";
            this.btnTaoHoaDon.Size = new System.Drawing.Size(140, 30);
            this.btnTaoHoaDon.TabIndex = 14;
            this.btnTaoHoaDon.Text = "Tạo đơn hàng";
            this.btnTaoHoaDon.UseVisualStyleBackColor = false;
            this.btnTaoHoaDon.Click += new System.EventHandler(this.btnTaoHoaDon_Click);

            // txtTongTien
            this.txtTongTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.txtTongTien.ForeColor = System.Drawing.Color.Red;
            this.txtTongTien.Location = new System.Drawing.Point(100, 412);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.ReadOnly = true;
            this.txtTongTien.Size = new System.Drawing.Size(140, 23);
            this.txtTongTien.TabIndex = 13;
            this.txtTongTien.Text = "0";
            this.txtTongTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            // label5
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 417);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Tổng tiền:";

            // dgvChiTietHD
            this.dgvChiTietHD.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTietHD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietHD.Location = new System.Drawing.Point(10, 160);
            this.dgvChiTietHD.Name = "dgvChiTietHD";
            this.dgvChiTietHD.Size = new System.Drawing.Size(380, 240);
            this.dgvChiTietHD.TabIndex = 11;

            // btnXoaSP
            this.btnXoaSP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXoaSP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaSP.ForeColor = System.Drawing.Color.White;
            this.btnXoaSP.Location = new System.Drawing.Point(315, 125);
            this.btnXoaSP.Name = "btnXoaSP";
            this.btnXoaSP.Size = new System.Drawing.Size(75, 25);
            this.btnXoaSP.TabIndex = 10;
            this.btnXoaSP.Text = "Xóa";
            this.btnXoaSP.UseVisualStyleBackColor = false;
            this.btnXoaSP.Click += new System.EventHandler(this.btnXoaSP_Click);

            // btnThemSP
            this.btnThemSP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnThemSP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemSP.ForeColor = System.Drawing.Color.White;
            this.btnThemSP.Location = new System.Drawing.Point(234, 125);
            this.btnThemSP.Name = "btnThemSP";
            this.btnThemSP.Size = new System.Drawing.Size(75, 25);
            this.btnThemSP.TabIndex = 9;
            this.btnThemSP.Text = "Thêm";
            this.btnThemSP.UseVisualStyleBackColor = false;
            this.btnThemSP.Click += new System.EventHandler(this.btnThemSP_Click);

            // numSoLuong
            this.numSoLuong.Location = new System.Drawing.Point(234, 97);
            this.numSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(80, 20);
            this.numSoLuong.TabIndex = 7;
            this.numSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });

            // cbSanPham
            this.cbSanPham.FormattingEnabled = true;
            this.cbSanPham.Location = new System.Drawing.Point(100, 97);
            this.cbSanPham.Name = "cbSanPham";
            this.cbSanPham.Size = new System.Drawing.Size(128, 21);
            this.cbSanPham.TabIndex = 6;

            // label4
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Sản phẩm";

            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nhân viên";

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Khách hàng";

            // cbNhanVien
            this.cbNhanVien.FormattingEnabled = true;
            this.cbNhanVien.Location = new System.Drawing.Point(100, 62);
            this.cbNhanVien.Name = "cbNhanVien";
            this.cbNhanVien.Size = new System.Drawing.Size(290, 21);
            this.cbNhanVien.TabIndex = 2;

            // cbKhachHang
            this.cbKhachHang.FormattingEnabled = true;
            this.cbKhachHang.Location = new System.Drawing.Point(100, 32);
            this.cbKhachHang.Name = "cbKhachHang";
            this.cbKhachHang.Size = new System.Drawing.Size(290, 21);
            this.cbKhachHang.TabIndex = 1;

            // label1
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "THÔNG TIN BÁN HÀNG";

            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.Name = "UC_QuanLyBanHang";
            this.Size = new System.Drawing.Size(800, 450);
            this.Load += new System.EventHandler(this.UC_QuanLyBanHang_Load);
            this.panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietHD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            this.ResumeLayout(false);
        }
    }
}