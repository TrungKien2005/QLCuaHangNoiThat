namespace QLCuaHangNoiThat.Forms
{
    partial class FormKho
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTenKho;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.Label lblSoDienThoai;
        private System.Windows.Forms.TextBox txtTenKho;
        private System.Windows.Forms.TextBox txtDiaChiKho;
        private System.Windows.Forms.TextBox txtSoDienThoaiLienHe;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;

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
            this.lblTenKho = new System.Windows.Forms.Label();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.lblSoDienThoai = new System.Windows.Forms.Label();
            this.txtTenKho = new System.Windows.Forms.TextBox();
            this.txtDiaChiKho = new System.Windows.Forms.TextBox();
            this.txtSoDienThoaiLienHe = new System.Windows.Forms.TextBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTenKho
            // 
            this.lblTenKho.AutoSize = true;
            this.lblTenKho.Location = new System.Drawing.Point(25, 20);
            this.lblTenKho.Name = "lblTenKho";
            this.lblTenKho.Size = new System.Drawing.Size(57, 17);
            this.lblTenKho.TabIndex = 0;
            this.lblTenKho.Text = "Tên kho";
            // 
            // txtTenKho
            // 
            this.txtTenKho.Location = new System.Drawing.Point(150, 17);
            this.txtTenKho.Name = "txtTenKho";
            this.txtTenKho.Size = new System.Drawing.Size(260, 25);
            this.txtTenKho.TabIndex = 1;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Location = new System.Drawing.Point(25, 60);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(52, 17);
            this.lblDiaChi.TabIndex = 2;
            this.lblDiaChi.Text = "Địa chỉ";
            // 
            // txtDiaChiKho
            // 
            this.txtDiaChiKho.Location = new System.Drawing.Point(150, 57);
            this.txtDiaChiKho.Name = "txtDiaChiKho";
            this.txtDiaChiKho.Size = new System.Drawing.Size(260, 25);
            this.txtDiaChiKho.TabIndex = 3;
            // 
            // lblSoDienThoai
            // 
            this.lblSoDienThoai.AutoSize = true;
            this.lblSoDienThoai.Location = new System.Drawing.Point(25, 100);
            this.lblSoDienThoai.Name = "lblSoDienThoai";
            this.lblSoDienThoai.Size = new System.Drawing.Size(111, 17);
            this.lblSoDienThoai.TabIndex = 4;
            this.lblSoDienThoai.Text = "Số điện thoại liên hệ";
            // 
            // txtSoDienThoaiLienHe
            // 
            this.txtSoDienThoaiLienHe.Location = new System.Drawing.Point(150, 97);
            this.txtSoDienThoaiLienHe.Name = "txtSoDienThoaiLienHe";
            this.txtSoDienThoaiLienHe.Size = new System.Drawing.Size(260, 25);
            this.txtSoDienThoaiLienHe.TabIndex = 5;
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.SeaGreen;
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(150, 140);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(100, 35);
            this.btnLuu.TabIndex = 6;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.Firebrick;
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(310, 140);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 35);
            this.btnHuy.TabIndex = 7;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // FormKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 200);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.txtSoDienThoaiLienHe);
            this.Controls.Add(this.lblSoDienThoai);
            this.Controls.Add(this.txtDiaChiKho);
            this.Controls.Add(this.lblDiaChi);
            this.Controls.Add(this.txtTenKho);
            this.Controls.Add(this.lblTenKho);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thông tin kho";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
