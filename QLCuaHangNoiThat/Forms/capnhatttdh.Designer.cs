namespace QLCuaHangNoiThat.Forms
{
    partial class capnhatttdh
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tenkh = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.madh = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.trangthai = new System.Windows.Forms.ComboBox();
            this.btnluu = new System.Windows.Forms.Button();
            this.btnhuy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(244, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(307, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "CẬP NHẬT TRẠNG THÁI ĐƠN HÀNG";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã đơn hàng:";
            // 
            // tenkh
            // 
            this.tenkh.AutoSize = true;
            this.tenkh.Location = new System.Drawing.Point(265, 132);
            this.tenkh.Name = "tenkh";
            this.tenkh.Size = new System.Drawing.Size(51, 20);
            this.tenkh.TabIndex = 2;
            this.tenkh.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(132, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tên khách hàng:";
            // 
            // madh
            // 
            this.madh.AutoSize = true;
            this.madh.Location = new System.Drawing.Point(244, 88);
            this.madh.Name = "madh";
            this.madh.Size = new System.Drawing.Size(51, 20);
            this.madh.TabIndex = 4;
            this.madh.Text = "label5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(435, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Trạng thái:";
            // 
            // trangthai
            // 
            this.trangthai.FormattingEnabled = true;
            this.trangthai.Location = new System.Drawing.Point(515, 80);
            this.trangthai.Name = "trangthai";
            this.trangthai.Size = new System.Drawing.Size(121, 28);
            this.trangthai.TabIndex = 6;
            // 
            // btnluu
            // 
            this.btnluu.BackColor = System.Drawing.Color.Green;
            this.btnluu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnluu.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnluu.Location = new System.Drawing.Point(295, 237);
            this.btnluu.Name = "btnluu";
            this.btnluu.Size = new System.Drawing.Size(75, 39);
            this.btnluu.TabIndex = 7;
            this.btnluu.Text = "Lưu";
            this.btnluu.UseVisualStyleBackColor = false;
            this.btnluu.Click += new System.EventHandler(this.btnluu_Click_1);
            // 
            // btnhuy
            // 
            this.btnhuy.BackColor = System.Drawing.Color.Red;
            this.btnhuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnhuy.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnhuy.Location = new System.Drawing.Point(439, 237);
            this.btnhuy.Name = "btnhuy";
            this.btnhuy.Size = new System.Drawing.Size(75, 39);
            this.btnhuy.TabIndex = 8;
            this.btnhuy.Text = "Hủy";
            this.btnhuy.UseVisualStyleBackColor = false;
            this.btnhuy.Click += new System.EventHandler(this.btnhuy_Click_1);
            // 
            // capnhatttdh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 414);
            this.Controls.Add(this.btnhuy);
            this.Controls.Add(this.btnluu);
            this.Controls.Add(this.trangthai);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.madh);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tenkh);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "capnhatttdh";
            this.Text = "capnhatttdh";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label tenkh;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label madh;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox trangthai;
        private System.Windows.Forms.Button btnluu;
        private System.Windows.Forms.Button btnhuy;
    }
}