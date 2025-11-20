namespace QLCuaHangNoiThat.Forms
{
    partial class dskh
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColMakh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHokh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenkh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colsdt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coldiachi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colngaytao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Stencil", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label1.Location = new System.Drawing.Point(254, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(449, 52);
            this.label1.TabIndex = 0;
            this.label1.Text = "CHỌN KHÁCH HÀNG";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(45, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(993, 100);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Location = new System.Drawing.Point(45, 118);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(993, 389);
            this.panel2.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColMakh,
            this.colHokh,
            this.colTenkh,
            this.colEmail,
            this.colsdt,
            this.coldiachi,
            this.colngaytao});
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(990, 383);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            // 
            // ColMakh
            // 
            this.ColMakh.HeaderText = "Mã khách hàng";
            this.ColMakh.MinimumWidth = 8;
            this.ColMakh.Name = "ColMakh";
            // 
            // colHokh
            // 
            this.colHokh.HeaderText = "Họ";
            this.colHokh.MinimumWidth = 8;
            this.colHokh.Name = "colHokh";
            // 
            // colTenkh
            // 
            this.colTenkh.HeaderText = "Tên";
            this.colTenkh.MinimumWidth = 8;
            this.colTenkh.Name = "colTenkh";
            // 
            // colEmail
            // 
            this.colEmail.HeaderText = "Email";
            this.colEmail.MinimumWidth = 8;
            this.colEmail.Name = "colEmail";
            // 
            // colsdt
            // 
            this.colsdt.HeaderText = "SĐT";
            this.colsdt.MinimumWidth = 8;
            this.colsdt.Name = "colsdt";
            // 
            // coldiachi
            // 
            this.coldiachi.HeaderText = "địa chỉ";
            this.coldiachi.MinimumWidth = 8;
            this.coldiachi.Name = "coldiachi";
            // 
            // colngaytao
            // 
            this.colngaytao.HeaderText = "ngày tạo";
            this.colngaytao.MinimumWidth = 8;
            this.colngaytao.Name = "colngaytao";
            // 
            // dskh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 555);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "dskh";
            this.Text = "dskh";
            this.Load += new System.EventHandler(this.dskh_Load_1);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMakh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHokh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenkh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colsdt;
        private System.Windows.Forms.DataGridViewTextBoxColumn coldiachi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colngaytao;
    }
}