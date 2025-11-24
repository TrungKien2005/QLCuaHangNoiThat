using System.Windows.Forms;
using System.Drawing;
namespace QLCuaHangNoiThat.UserControls
{
    partial class UC_QuanLyNhaCungCap
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvNhaCungCap;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtTenNCC, txtNguoiLienLac, txtEmailNCC, txtSDTNCC, txtDiaChiNCC;
        private System.Windows.Forms.Label lblTen, lblNguoiLienLac, lblEmail, lblSDT, lblDiaChi;
        private System.Windows.Forms.Button btnThemNCC, btnSuaNCC, btnXoaNCC;
        private System.Windows.Forms.Panel panelInputs;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvNhaCungCap = new System.Windows.Forms.DataGridView();
            this.panelInputs = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.txtTenNCC = new System.Windows.Forms.TextBox();
            this.txtNguoiLienLac = new System.Windows.Forms.TextBox();
            this.txtEmailNCC = new System.Windows.Forms.TextBox();
            this.txtSDTNCC = new System.Windows.Forms.TextBox();
            this.txtDiaChiNCC = new System.Windows.Forms.TextBox();
            this.lblTen = new System.Windows.Forms.Label();
            this.lblNguoiLienLac = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.btnThemNCC = new System.Windows.Forms.Button();
            this.btnSuaNCC = new System.Windows.Forms.Button();
            this.btnXoaNCC = new System.Windows.Forms.Button();

            this.panelHeader.SuspendLayout();
            this.panelInputs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhaCungCap)).BeginInit();
            this.SuspendLayout();

            // panelHeader
            this.panelHeader.BackColor = System.Drawing.Color.SteelBlue;
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Height = 60;
            this.panelHeader.Controls.Add(this.lblTitle);

            // lblTitle
            this.lblTitle.Text = "QUẢN LÝ NHÀ CUNG CẤP";
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // panelInputs
            this.panelInputs.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInputs.Height = 180;
            this.panelInputs.Padding = new System.Windows.Forms.Padding(20);
            this.panelInputs.BackColor = System.Drawing.Color.WhiteSmoke;

            // Search
            this.lblSearch.Text = "Tìm kiếm:";
            this.lblSearch.Location = new System.Drawing.Point(20, 20);
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.txtSearch.Location = new System.Drawing.Point(100, 18);
            this.txtSearch.Width = 250;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);

            // Labels & TextBox
            int xLabel = 20, xText = 140, yStart = 60, spacingY = 30;
            int txtWidth = 250;

            this.lblTen.Text = "Tên NCC:";
            this.lblTen.Location = new System.Drawing.Point(xLabel, yStart);
            this.lblTen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenNCC.Location = new System.Drawing.Point(xText, yStart);
            this.txtTenNCC.Width = txtWidth;

            this.lblNguoiLienLac.Text = "Người liên lạc:";
            this.lblNguoiLienLac.Location = new System.Drawing.Point(xLabel, yStart + spacingY);
            this.txtNguoiLienLac.Location = new System.Drawing.Point(xText, yStart + spacingY);
            this.txtNguoiLienLac.Width = txtWidth;

            this.lblEmail.Text = "Email:";
            this.lblEmail.Location = new System.Drawing.Point(xLabel, yStart + spacingY * 2);
            this.txtEmailNCC.Location = new System.Drawing.Point(xText, yStart + spacingY * 2);
            this.txtEmailNCC.Width = txtWidth;

            this.lblSDT.Text = "SĐT:";
            this.lblSDT.Location = new System.Drawing.Point(xLabel + 400, yStart);
            this.txtSDTNCC.Location = new System.Drawing.Point(xLabel + 500, yStart);
            this.txtSDTNCC.Width = 200;

            this.lblDiaChi.Text = "Địa chỉ:";
            this.lblDiaChi.Location = new System.Drawing.Point(xLabel + 400, yStart + spacingY);
            this.txtDiaChiNCC.Location = new System.Drawing.Point(xLabel + 500, yStart + spacingY);
            this.txtDiaChiNCC.Width = 200;

            // Buttons
            int btnWidth = 120, btnHeight = 35, btnY = yStart + spacingY * 2;
            this.btnThemNCC.Text = "Thêm";
            this.btnThemNCC.Location = new System.Drawing.Point(xLabel + 400, btnY);
            this.btnThemNCC.BackColor = System.Drawing.Color.SeaGreen;
            this.btnThemNCC.ForeColor = Color.White;
            this.btnThemNCC.FlatStyle = FlatStyle.Flat;
            this.btnThemNCC.FlatAppearance.BorderSize = 0;
            this.btnThemNCC.Height = btnHeight;
            this.btnThemNCC.Width = btnWidth;

            this.btnSuaNCC.Text = "Sửa";
            this.btnSuaNCC.Location = new System.Drawing.Point(xLabel + 530, btnY);
            this.btnSuaNCC.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSuaNCC.ForeColor = Color.White;
            this.btnSuaNCC.FlatStyle = FlatStyle.Flat;
            this.btnSuaNCC.FlatAppearance.BorderSize = 0;
            this.btnSuaNCC.Height = btnHeight;
            this.btnSuaNCC.Width = btnWidth;

            this.btnXoaNCC.Text = "Xóa";
            this.btnXoaNCC.Location = new System.Drawing.Point(xLabel + 660, btnY);
            this.btnXoaNCC.BackColor = System.Drawing.Color.IndianRed;
            this.btnXoaNCC.ForeColor = Color.White;
            this.btnXoaNCC.FlatStyle = FlatStyle.Flat;
            this.btnXoaNCC.FlatAppearance.BorderSize = 0;
            this.btnXoaNCC.Height = btnHeight;
            this.btnXoaNCC.Width = btnWidth;

            // DataGridView
            this.dgvNhaCungCap.Dock = DockStyle.Fill;
            this.dgvNhaCungCap.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dgvNhaCungCap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNhaCungCap.RowTemplate.Height = 30;
            this.dgvNhaCungCap.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Add controls
            this.panelInputs.Controls.Add(this.lblSearch);
            this.panelInputs.Controls.Add(this.txtSearch);
            this.panelInputs.Controls.Add(this.lblTen);
            this.panelInputs.Controls.Add(this.txtTenNCC);
            this.panelInputs.Controls.Add(this.lblNguoiLienLac);
            this.panelInputs.Controls.Add(this.txtNguoiLienLac);
            this.panelInputs.Controls.Add(this.lblEmail);
            this.panelInputs.Controls.Add(this.txtEmailNCC);
            this.panelInputs.Controls.Add(this.lblSDT);
            this.panelInputs.Controls.Add(this.txtSDTNCC);
            this.panelInputs.Controls.Add(this.lblDiaChi);
            this.panelInputs.Controls.Add(this.txtDiaChiNCC);
            this.panelInputs.Controls.Add(this.btnThemNCC);
            this.panelInputs.Controls.Add(this.btnSuaNCC);
            this.panelInputs.Controls.Add(this.btnXoaNCC);

            this.Controls.Add(this.dgvNhaCungCap);
            this.Controls.Add(this.panelInputs);
            this.Controls.Add(this.panelHeader);

            this.panelHeader.ResumeLayout(false);
            this.panelInputs.ResumeLayout(false);
            this.panelInputs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhaCungCap)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
