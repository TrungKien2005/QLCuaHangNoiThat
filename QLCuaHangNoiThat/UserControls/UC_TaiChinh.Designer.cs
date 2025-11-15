using System.Windows.Forms;

namespace QLCuaHangNoiThat.UserControls
{
    partial class UC_TaiChinh
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvTaiChinh = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLoc = new System.Windows.Forms.Button();
            this.cbLoaiGD = new System.Windows.Forms.ComboBox();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnThem = new System.Windows.Forms.Button();
            this.cbNhanVien = new System.Windows.Forms.ComboBox();
            this.dtpNgayGD = new System.Windows.Forms.DateTimePicker();
            this.txtNoiDung = new System.Windows.Forms.TextBox();
            this.numSoTien = new System.Windows.Forms.NumericUpDown();
            this.rbChi = new System.Windows.Forms.RadioButton();
            this.rbThu = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblConLai = new System.Windows.Forms.Label();
            this.lblTongChi = new System.Windows.Forms.Label();
            this.lblTongThu = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiChinh)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoTien)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTaiChinh
            // 
            this.dgvTaiChinh.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTaiChinh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaiChinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTaiChinh.Location = new System.Drawing.Point(0, 280);
            this.dgvTaiChinh.Name = "dgvTaiChinh";
            this.dgvTaiChinh.Size = new System.Drawing.Size(800, 170);
            this.dgvTaiChinh.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnLoc);
            this.panel1.Controls.Add(this.cbLoaiGD);
            this.panel1.Controls.Add(this.dtpDenNgay);
            this.panel1.Controls.Add(this.dtpTuNgay);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.btnThem);
            this.panel1.Controls.Add(this.cbNhanVien);
            this.panel1.Controls.Add(this.dtpNgayGD);
            this.panel1.Controls.Add(this.txtNoiDung);
            this.panel1.Controls.Add(this.numSoTien);
            this.panel1.Controls.Add(this.rbChi);
            this.panel1.Controls.Add(this.rbThu);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 180);
            this.panel1.TabIndex = 3;
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.btnLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoc.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Location = new System.Drawing.Point(600, 140);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(75, 30);
            this.btnLoc.TabIndex = 19;
            this.btnLoc.Text = "Lọc";
            this.btnLoc.UseVisualStyleBackColor = false;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // cbLoaiGD
            // 
            this.cbLoaiGD.FormattingEnabled = true;
            this.cbLoaiGD.Items.AddRange(new object[] {
            "Tất cả",
            "Thu",
            "Chi"});
            this.cbLoaiGD.Location = new System.Drawing.Point(450, 145);
            this.cbLoaiGD.Name = "cbLoaiGD";
            this.cbLoaiGD.Size = new System.Drawing.Size(100, 21);
            this.cbLoaiGD.TabIndex = 18;
            this.cbLoaiGD.SelectedIndex = 0;
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Location = new System.Drawing.Point(250, 145);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(120, 20);
            this.dtpDenNgay.TabIndex = 17;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Location = new System.Drawing.Point(80, 145);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(120, 20);
            this.dtpTuNgay.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(210, 148);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Đến";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(380, 148);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Loại GD:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 148);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Từ ngày:";
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(600, 100);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 30);
            this.btnThem.TabIndex = 12;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // cbNhanVien
            // 
            this.cbNhanVien.FormattingEnabled = true;
            this.cbNhanVien.Location = new System.Drawing.Point(450, 75);
            this.cbNhanVien.Name = "cbNhanVien";
            this.cbNhanVien.Size = new System.Drawing.Size(150, 21);
            this.cbNhanVien.TabIndex = 11;
            // 
            // dtpNgayGD
            // 
            this.dtpNgayGD.Location = new System.Drawing.Point(450, 45);
            this.dtpNgayGD.Name = "dtpNgayGD";
            this.dtpNgayGD.Size = new System.Drawing.Size(150, 20);
            this.dtpNgayGD.TabIndex = 10;
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.Location = new System.Drawing.Point(450, 15);
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Size = new System.Drawing.Size(225, 20);
            this.txtNoiDung.TabIndex = 9;
            // 
            // numSoTien
            // 
            this.numSoTien.DecimalPlaces = 0;
            this.numSoTien.Location = new System.Drawing.Point(80, 75);
            this.numSoTien.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numSoTien.Name = "numSoTien";
            this.numSoTien.Size = new System.Drawing.Size(120, 20);
            this.numSoTien.TabIndex = 8;
            this.numSoTien.ThousandsSeparator = true;
            // 
            // rbChi
            // 
            this.rbChi.AutoSize = true;
            this.rbChi.Location = new System.Drawing.Point(130, 45);
            this.rbChi.Name = "rbChi";
            this.rbChi.Size = new System.Drawing.Size(41, 17);
            this.rbChi.TabIndex = 7;
            this.rbChi.Text = "Chi";
            this.rbChi.UseVisualStyleBackColor = true;
            // 
            // rbThu
            // 
            this.rbThu.AutoSize = true;
            this.rbThu.Checked = true;
            this.rbThu.Location = new System.Drawing.Point(80, 45);
            this.rbThu.Name = "rbThu";
            this.rbThu.Size = new System.Drawing.Size(44, 17);
            this.rbThu.TabIndex = 6;
            this.rbThu.TabStop = true;
            this.rbThu.Text = "Thu";
            this.rbThu.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(380, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Nhân viên:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(380, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Ngày GD:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(380, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Nội dung:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Số tiền:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Loại GD:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(20, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "THÊM GIAO DỊCH MỚI";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblConLai);
            this.panel2.Controls.Add(this.lblTongChi);
            this.panel2.Controls.Add(this.lblTongThu);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 180);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 100);
            this.panel2.TabIndex = 4;
            // 
            // lblConLai
            // 
            this.lblConLai.AutoSize = true;
            this.lblConLai.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblConLai.Location = new System.Drawing.Point(550, 50);
            this.lblConLai.Name = "lblConLai";
            this.lblConLai.Size = new System.Drawing.Size(57, 20);
            this.lblConLai.TabIndex = 5;
            this.lblConLai.Text = "0 đ";
            // 
            // lblTongChi
            // 
            this.lblTongChi.AutoSize = true;
            this.lblTongChi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTongChi.Location = new System.Drawing.Point(550, 20);
            this.lblTongChi.Name = "lblTongChi";
            this.lblTongChi.Size = new System.Drawing.Size(57, 20);
            this.lblTongChi.TabIndex = 4;
            this.lblTongChi.Text = "0 đ";
            // 
            // lblTongThu
            // 
            this.lblTongThu.AutoSize = true;
            this.lblTongThu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTongThu.Location = new System.Drawing.Point(200, 50);
            this.lblTongThu.Name = "lblTongThu";
            this.lblTongThu.Size = new System.Drawing.Size(57, 20);
            this.lblTongThu.TabIndex = 3;
            this.lblTongThu.Text = "0 đ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(400, 50);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(144, 17);
            this.label12.TabIndex = 2;
            this.label12.Text = "TỔNG CÒN LẠI:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(400, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(144, 17);
            this.label11.TabIndex = 1;
            this.label11.Text = "TỔNG CHI PHÍ:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(50, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(144, 17);
            this.label10.TabIndex = 0;
            this.label10.Text = "TỔNG THU NHẬP:";
            // 
            // UC_TaiChinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvTaiChinh);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "UC_TaiChinh";
            this.Size = new System.Drawing.Size(800, 450);
            this.Load += new System.EventHandler(this.UC_TaiChinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiChinh)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoTien)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTaiChinh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.ComboBox cbLoaiGD;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.ComboBox cbNhanVien;
        private System.Windows.Forms.DateTimePicker dtpNgayGD;
        private System.Windows.Forms.TextBox txtNoiDung;
        private System.Windows.Forms.NumericUpDown numSoTien;
        private System.Windows.Forms.RadioButton rbChi;
        private System.Windows.Forms.RadioButton rbThu;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblConLai;
        private System.Windows.Forms.Label lblTongChi;
        private System.Windows.Forms.Label lblTongThu;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
    }
}