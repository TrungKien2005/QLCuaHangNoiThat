namespace QLCuaHangNoiThat.UserControls
{
    partial class UC_TrangChu
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.flowLayoutStats = new System.Windows.Forms.FlowLayoutPanel();
            this.panelChart = new System.Windows.Forms.Panel();
            this.panelRecentActivity = new System.Windows.Forms.Panel();
            this.dataGridViewRecent = new System.Windows.Forms.DataGridView();
            this.lblRecentActivity = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelRecentActivity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecent)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(20);
            this.panelHeader.Size = new System.Drawing.Size(1150, 80);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.lblTitle.Location = new System.Drawing.Point(15, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(324, 38);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TỔNG QUAN HỆ THỐNG";
            // 
            // flowLayoutStats
            // 
            this.flowLayoutStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutStats.Location = new System.Drawing.Point(20, 100);
            this.flowLayoutStats.Name = "flowLayoutStats";
            this.flowLayoutStats.Size = new System.Drawing.Size(1110, 120);
            this.flowLayoutStats.TabIndex = 1;
            // 
            // panelChart
            // 
            this.panelChart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelChart.BackColor = System.Drawing.Color.White;
            this.panelChart.Location = new System.Drawing.Point(20, 240);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(1110, 250);
            this.panelChart.TabIndex = 2;
            // 
            // panelRecentActivity
            // 
            this.panelRecentActivity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRecentActivity.BackColor = System.Drawing.Color.White;
            this.panelRecentActivity.Controls.Add(this.dataGridViewRecent);
            this.panelRecentActivity.Controls.Add(this.lblRecentActivity);
            this.panelRecentActivity.Location = new System.Drawing.Point(20, 510);
            this.panelRecentActivity.Name = "panelRecentActivity";
            this.panelRecentActivity.Size = new System.Drawing.Size(1110, 161);
            this.panelRecentActivity.TabIndex = 3;
            // 
            // dataGridViewRecent
            // 
            this.dataGridViewRecent.AllowUserToAddRows = false;
            this.dataGridViewRecent.AllowUserToDeleteRows = false;
            this.dataGridViewRecent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewRecent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewRecent.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewRecent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewRecent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRecent.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dataGridViewRecent.Location = new System.Drawing.Point(20, 50);
            this.dataGridViewRecent.Name = "dataGridViewRecent";
            this.dataGridViewRecent.ReadOnly = true;
            this.dataGridViewRecent.RowHeadersWidth = 51;
            this.dataGridViewRecent.RowTemplate.Height = 24;
            this.dataGridViewRecent.Size = new System.Drawing.Size(1070, 90);
            this.dataGridViewRecent.TabIndex = 1;
            // 
            // lblRecentActivity
            // 
            this.lblRecentActivity.AutoSize = true;
            this.lblRecentActivity.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecentActivity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.lblRecentActivity.Location = new System.Drawing.Point(15, 15);
            this.lblRecentActivity.Name = "lblRecentActivity";
            this.lblRecentActivity.Size = new System.Drawing.Size(179, 28);
            this.lblRecentActivity.TabIndex = 0;
            this.lblRecentActivity.Text = "HOẠT ĐỘNG GẦN ĐÂY";
            // 
            // UC_TrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Controls.Add(this.panelRecentActivity);
            this.Controls.Add(this.panelChart);
            this.Controls.Add(this.flowLayoutStats);
            this.Controls.Add(this.panelHeader);
            this.Name = "UC_TrangChu";
            this.Size = new System.Drawing.Size(1150, 691);
            this.Load += new System.EventHandler(this.UC_TrangChu_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelRecentActivity.ResumeLayout(false);
            this.panelRecentActivity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutStats;
        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.Panel panelRecentActivity;
        private System.Windows.Forms.DataGridView dataGridViewRecent;
        private System.Windows.Forms.Label lblRecentActivity;
    }
}