namespace QLCuaHangNoiThat.UserControls
{
    partial class UC_BaoCaoThongKe
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageDoanhThu;
        private System.Windows.Forms.TabPage tabPageTopSP;
        private System.Windows.Forms.TabPage tabPageTonKho;

        private System.Windows.Forms.DataGridView dgvDoanhThu;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThu;
        private System.Windows.Forms.DataGridView dgvTopSP;
        private System.Windows.Forms.DataGridView dgvTonKho;

        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Button btnXuatPDF;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageDoanhThu = new System.Windows.Forms.TabPage();
            this.chartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvDoanhThu = new System.Windows.Forms.DataGridView();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.btnLoc = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.btnXuatPDF = new System.Windows.Forms.Button();
            this.tabPageTopSP = new System.Windows.Forms.TabPage();
            this.dgvTopSP = new System.Windows.Forms.DataGridView();
            this.tabPageTonKho = new System.Windows.Forms.TabPage();
            this.dgvTonKho = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPageDoanhThu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoanhThu)).BeginInit();
            this.tabPageTopSP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopSP)).BeginInit();
            this.tabPageTonKho.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageDoanhThu);
            this.tabControl1.Controls.Add(this.tabPageTopSP);
            this.tabControl1.Controls.Add(this.tabPageTonKho);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageDoanhThu
            // 
            this.tabPageDoanhThu.Controls.Add(this.chartDoanhThu);
            this.tabPageDoanhThu.Controls.Add(this.dgvDoanhThu);
            this.tabPageDoanhThu.Controls.Add(this.dtpTuNgay);
            this.tabPageDoanhThu.Controls.Add(this.dtpDenNgay);
            this.tabPageDoanhThu.Controls.Add(this.btnLoc);
            this.tabPageDoanhThu.Controls.Add(this.btnXuatExcel);
            this.tabPageDoanhThu.Controls.Add(this.btnXuatPDF);
            this.tabPageDoanhThu.Location = new System.Drawing.Point(4, 25);
            this.tabPageDoanhThu.Name = "tabPageDoanhThu";
            this.tabPageDoanhThu.Size = new System.Drawing.Size(792, 421);
            this.tabPageDoanhThu.TabIndex = 0;
            this.tabPageDoanhThu.Text = "Doanh thu";
            // 
            // chartDoanhThu
            // 
            this.chartDoanhThu.Dock = System.Windows.Forms.DockStyle.Left;
            this.chartDoanhThu.Location = new System.Drawing.Point(0, 0);
            this.chartDoanhThu.Name = "chartDoanhThu";
            this.chartDoanhThu.Size = new System.Drawing.Size(500, 421);
            this.chartDoanhThu.TabIndex = 0;
            // 
            // dgvDoanhThu
            // 
            this.dgvDoanhThu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDoanhThu.ColumnHeadersHeight = 29;
            this.dgvDoanhThu.Dock = System.Windows.Forms.DockStyle.Right;
            this.dgvDoanhThu.Location = new System.Drawing.Point(512, 0);
            this.dgvDoanhThu.Name = "dgvDoanhThu";
            this.dgvDoanhThu.RowHeadersWidth = 51;
            this.dgvDoanhThu.Size = new System.Drawing.Size(280, 421);
            this.dgvDoanhThu.TabIndex = 1;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Location = new System.Drawing.Point(10, 10);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(200, 22);
            this.dtpTuNgay.TabIndex = 2;
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Location = new System.Drawing.Point(180, 10);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(200, 22);
            this.dtpDenNgay.TabIndex = 3;
            // 
            // btnLoc
            // 
            this.btnLoc.Location = new System.Drawing.Point(350, 10);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(75, 23);
            this.btnLoc.TabIndex = 4;
            this.btnLoc.Text = "Lọc";
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.Location = new System.Drawing.Point(420, 10);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(75, 23);
            this.btnXuatExcel.TabIndex = 5;
            this.btnXuatExcel.Text = "Xuất Excel";
            // 
            // btnXuatPDF
            // 
            this.btnXuatPDF.Location = new System.Drawing.Point(520, 10);
            this.btnXuatPDF.Name = "btnXuatPDF";
            this.btnXuatPDF.Size = new System.Drawing.Size(75, 23);
            this.btnXuatPDF.TabIndex = 6;
            this.btnXuatPDF.Text = "Xuất PDF";
            // 
            //// tabPageTopSP
            //// 
            //this.tabPageTopSP.Controls.Add(this.dgvTopSP);
            //this.tabPageTopSP.Location = new System.Drawing.Point(4, 25);
            //this.tabPageTopSP.Name = "tabPageTopSP";
            //this.tabPageTopSP.Size = new System.Drawing.Size(792, 421);
            //this.tabPageTopSP.TabIndex = 1;
            
            // 
            // dgvTopSP
            //// 
            //this.dgvTopSP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            //this.dgvTopSP.ColumnHeadersHeight = 29;
            //this.dgvTopSP.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.dgvTopSP.Location = new System.Drawing.Point(0, 0);
            //this.dgvTopSP.Name = "dgvTopSP";
            //this.dgvTopSP.RowHeadersWidth = 51;
            //this.dgvTopSP.Size = new System.Drawing.Size(792, 421);
            //this.dgvTopSP.TabIndex = 0;
            // 
            // tabPageTonKho
            // 
            this.tabPageTonKho.Controls.Add(this.dgvTonKho);
            this.tabPageTonKho.Location = new System.Drawing.Point(4, 25);
            this.tabPageTonKho.Name = "tabPageTonKho";
            this.tabPageTonKho.Size = new System.Drawing.Size(792, 421);
            this.tabPageTonKho.TabIndex = 2;
            this.tabPageTonKho.Text = "Sản Phẩm Bán Chạy";
            // 
            // dgvTonKho
            // 
            this.dgvTonKho.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTonKho.ColumnHeadersHeight = 29;
            this.dgvTonKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTonKho.Location = new System.Drawing.Point(0, 0);
            this.dgvTonKho.Name = "dgvTonKho";
            this.dgvTonKho.RowHeadersWidth = 51;
            this.dgvTonKho.Size = new System.Drawing.Size(792, 421);
            this.dgvTonKho.TabIndex = 0;
            // 
            // UC_BaoCaoThongKe
            // 
            this.Controls.Add(this.tabControl1);
            this.Name = "UC_BaoCaoThongKe";
            this.Size = new System.Drawing.Size(800, 450);
            this.Load += new System.EventHandler(this.UC_BaoCaoThongKe_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageDoanhThu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoanhThu)).EndInit();
            this.tabPageTopSP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopSP)).EndInit();
            this.tabPageTonKho.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
