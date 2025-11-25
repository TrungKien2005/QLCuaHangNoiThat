namespace QLCuaHangNoiThat.UserControls
{
    partial class UC_QuanLyKho
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvTonKho;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvKho;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvNhaCungCap;
        private System.Windows.Forms.TextBox txtTenNCC;
        private System.Windows.Forms.TextBox txtNguoiLienLac;
        private System.Windows.Forms.TextBox txtEmailNCC;
        private System.Windows.Forms.TextBox txtSDTNCC;
        private System.Windows.Forms.TextBox txtDiaChiNCC;
        private System.Windows.Forms.Button btnThemNCC;
        private System.Windows.Forms.Button btnSuaNCC;
        private System.Windows.Forms.Button btnXoaNCC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelKho;
        private System.Windows.Forms.Button btnThemKho;
        private System.Windows.Forms.Button btnSuaKho;
        private System.Windows.Forms.Button btnXoaKho;


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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvTonKho = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvKho = new System.Windows.Forms.DataGridView();
            this.panelKho = new System.Windows.Forms.Panel();
            this.btnThemKho = new System.Windows.Forms.Button();
            this.btnSuaKho = new System.Windows.Forms.Button();
            this.btnXoaKho = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvNhaCungCap = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnXoaNCC = new System.Windows.Forms.Button();
            this.btnSuaNCC = new System.Windows.Forms.Button();
            this.btnThemNCC = new System.Windows.Forms.Button();
            this.txtDiaChiNCC = new System.Windows.Forms.TextBox();
            this.txtSDTNCC = new System.Windows.Forms.TextBox();
            this.txtEmailNCC = new System.Windows.Forms.TextBox();
            this.txtNguoiLienLac = new System.Windows.Forms.TextBox();
            this.txtTenNCC = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKho)).BeginInit();
            this.panelKho.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhaCungCap)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvTonKho);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 417);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tồn kho";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvTonKho
            // 
            this.dgvTonKho.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTonKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTonKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTonKho.Location = new System.Drawing.Point(3, 3);
            this.dgvTonKho.Name = "dgvTonKho";
            this.dgvTonKho.RowHeadersWidth = 62;
            this.dgvTonKho.Size = new System.Drawing.Size(786, 411);
            this.dgvTonKho.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvKho);
            this.tabPage2.Controls.Add(this.panelKho);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 417);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Danh sách kho";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvKho
            // 
            this.dgvKho.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKho.Location = new System.Drawing.Point(3, 83);
            this.dgvKho.Name = "dgvKho";
            this.dgvKho.RowHeadersWidth = 62;
            this.dgvKho.Size = new System.Drawing.Size(786, 331);
            this.dgvKho.TabIndex = 0;
            // 
            // panelKho
            // 
            this.panelKho.Controls.Add(this.btnThemKho);
            this.panelKho.Controls.Add(this.btnSuaKho);
            this.panelKho.Controls.Add(this.btnXoaKho);
            this.panelKho.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelKho.Location = new System.Drawing.Point(3, 3);
            this.panelKho.Name = "panelKho";
            this.panelKho.Size = new System.Drawing.Size(786, 80);
            this.panelKho.TabIndex = 1;
            // 
            // btnThemKho
            // 
            this.btnThemKho.Location = new System.Drawing.Point(273, 20);
            this.btnThemKho.Name = "btnThemKho";
            this.btnThemKho.Size = new System.Drawing.Size(110, 40);
            this.btnThemKho.TabIndex = 2;
            this.btnThemKho.Text = "Thêm";
            this.btnThemKho.Click += new System.EventHandler(this.btnThemKho_Click);
            // 
            // btnSuaKho
            // 
            this.btnSuaKho.Location = new System.Drawing.Point(416, 20);
            this.btnSuaKho.Name = "btnSuaKho";
            this.btnSuaKho.Size = new System.Drawing.Size(110, 40);
            this.btnSuaKho.TabIndex = 3;
            this.btnSuaKho.Text = "Sửa";
            this.btnSuaKho.Click += new System.EventHandler(this.btnSuaKho_Click);
            // 
            // btnXoaKho
            // 
            this.btnXoaKho.Location = new System.Drawing.Point(560, 20);
            this.btnXoaKho.Name = "btnXoaKho";
            this.btnXoaKho.Size = new System.Drawing.Size(110, 40);
            this.btnXoaKho.TabIndex = 4;
            this.btnXoaKho.Text = "Xóa";
            this.btnXoaKho.Click += new System.EventHandler(this.btnXoaKho_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvNhaCungCap);
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(792, 417);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Nhà cung cấp";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvNhaCungCap
            // 
            this.dgvNhaCungCap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNhaCungCap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNhaCungCap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNhaCungCap.Location = new System.Drawing.Point(3, 123);
            this.dgvNhaCungCap.Name = "dgvNhaCungCap";
            this.dgvNhaCungCap.RowHeadersWidth = 62;
            this.dgvNhaCungCap.Size = new System.Drawing.Size(786, 291);
            this.dgvNhaCungCap.TabIndex = 1;
            this.dgvNhaCungCap.SelectionChanged += new System.EventHandler(this.dgvNhaCungCap_SelectionChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnXoaNCC);
            this.panel1.Controls.Add(this.btnSuaNCC);
            this.panel1.Controls.Add(this.btnThemNCC);
            this.panel1.Controls.Add(this.txtDiaChiNCC);
            this.panel1.Controls.Add(this.txtSDTNCC);
            this.panel1.Controls.Add(this.txtEmailNCC);
            this.panel1.Controls.Add(this.txtNguoiLienLac);
            this.panel1.Controls.Add(this.txtTenNCC);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(786, 120);
            this.panel1.TabIndex = 0;
            // 
            // btnXoaNCC
            // 
            this.btnXoaNCC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXoaNCC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaNCC.ForeColor = System.Drawing.Color.White;
            this.btnXoaNCC.Location = new System.Drawing.Point(600, 80);
            this.btnXoaNCC.Name = "btnXoaNCC";
            this.btnXoaNCC.Size = new System.Drawing.Size(75, 30);
            this.btnXoaNCC.TabIndex = 12;
            this.btnXoaNCC.Text = "Xóa";
            this.btnXoaNCC.UseVisualStyleBackColor = false;
            this.btnXoaNCC.Click += new System.EventHandler(this.btnXoaNCC_Click);
            // 
            // btnSuaNCC
            // 
            this.btnSuaNCC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnSuaNCC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuaNCC.ForeColor = System.Drawing.Color.White;
            this.btnSuaNCC.Location = new System.Drawing.Point(519, 80);
            this.btnSuaNCC.Name = "btnSuaNCC";
            this.btnSuaNCC.Size = new System.Drawing.Size(75, 30);
            this.btnSuaNCC.TabIndex = 11;
            this.btnSuaNCC.Text = "Sửa";
            this.btnSuaNCC.UseVisualStyleBackColor = false;
            this.btnSuaNCC.Click += new System.EventHandler(this.btnSuaNCC_Click);
            // 
            // btnThemNCC
            // 
            this.btnThemNCC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnThemNCC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemNCC.ForeColor = System.Drawing.Color.White;
            this.btnThemNCC.Location = new System.Drawing.Point(438, 80);
            this.btnThemNCC.Name = "btnThemNCC";
            this.btnThemNCC.Size = new System.Drawing.Size(75, 30);
            this.btnThemNCC.TabIndex = 10;
            this.btnThemNCC.Text = "Thêm";
            this.btnThemNCC.UseVisualStyleBackColor = false;
            this.btnThemNCC.Click += new System.EventHandler(this.btnThemNCC_Click);
            // 
            // txtDiaChiNCC
            // 
            this.txtDiaChiNCC.Location = new System.Drawing.Point(300, 74);
            this.txtDiaChiNCC.Name = "txtDiaChiNCC";
            this.txtDiaChiNCC.Size = new System.Drawing.Size(120, 26);
            this.txtDiaChiNCC.TabIndex = 9;
            // 
            // txtSDTNCC
            // 
            this.txtSDTNCC.Location = new System.Drawing.Point(300, 48);
            this.txtSDTNCC.Name = "txtSDTNCC";
            this.txtSDTNCC.Size = new System.Drawing.Size(120, 26);
            this.txtSDTNCC.TabIndex = 8;
            // 
            // txtEmailNCC
            // 
            this.txtEmailNCC.Location = new System.Drawing.Point(300, 22);
            this.txtEmailNCC.Name = "txtEmailNCC";
            this.txtEmailNCC.Size = new System.Drawing.Size(120, 26);
            this.txtEmailNCC.TabIndex = 7;
            // 
            // txtNguoiLienLac
            // 
            this.txtNguoiLienLac.Location = new System.Drawing.Point(80, 74);
            this.txtNguoiLienLac.Name = "txtNguoiLienLac";
            this.txtNguoiLienLac.Size = new System.Drawing.Size(120, 26);
            this.txtNguoiLienLac.TabIndex = 6;
            // 
            // txtTenNCC
            // 
            this.txtTenNCC.Location = new System.Drawing.Point(80, 22);
            this.txtTenNCC.Name = "txtTenNCC";
            this.txtTenNCC.Size = new System.Drawing.Size(120, 26);
            this.txtTenNCC.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(260, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Địa chỉ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(260, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "SĐT";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(260, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Email";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Người liên lạc";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên";
            // 
            // UC_QuanLyKho
            // 
            this.Controls.Add(this.tabControl1);
            this.Name = "UC_QuanLyKho";
            this.Size = new System.Drawing.Size(800, 450);
            this.Load += new System.EventHandler(this.UC_QuanLyKho_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKho)).EndInit();
            this.panelKho.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhaCungCap)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}