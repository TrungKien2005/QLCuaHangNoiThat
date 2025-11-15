using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace QLCuaHangNoiThat.UserControls
{
    public partial class UC_TrangChu : UserControl
    {
        public UC_TrangChu()
        {
            InitializeComponent();
        }

        private void UC_TrangChu_Load(object sender, EventArgs e)
        {
            LoadStatisticsCards();
            LoadRecentActivity();
            SetupChart();
        }

        private void LoadStatisticsCards()
        {
            // Xóa controls cũ
            flowLayoutStats.Controls.Clear();

            // Tạo các card thống kê
            var stats = new[]
            {
                new { Title = "TỔNG DOANH THU", Value = "125.5M", Icon = "💰", Color = Color.FromArgb(0, 150, 136), Trend = "+12.5%" },
                new { Title = "SẢN PHẨM", Value = "1,248", Icon = "📦", Color = Color.FromArgb(33, 150, 243), Trend = "+5.2%" },
                new { Title = "ĐƠN HÀNG", Value = "324", Icon = "🛒", Color = Color.FromArgb(156, 39, 176), Trend = "+8.7%" },
                new { Title = "KHÁCH HÀNG", Value = "2,156", Icon = "👥", Color = Color.FromArgb(255, 87, 34), Trend = "+3.4%" }
            };

            foreach (var stat in stats)
            {
                var card = CreateStatCard(stat.Title, stat.Value, stat.Icon, stat.Color, stat.Trend);
                flowLayoutStats.Controls.Add(card);
            }
        }

        private Panel CreateStatCard(string title, string value, string icon, Color color, string trend)
        {
            var card = new Panel
            {
                BackColor = Color.White,
                Size = new Size(260, 100),
                Margin = new Padding(10),
                Padding = new Padding(15)
            };

            // Tạo hiệu ứng bóng
            card.Paint += (s, e) =>
            {
                using (var shadowPath = new GraphicsPath())
                {
                    shadowPath.AddRectangle(new Rectangle(0, 0, card.Width, card.Height));
                    using (var shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
                    {
                        e.Graphics.FillPath(shadowBrush, shadowPath);
                    }
                }
            };

            // Icon
            var lblIcon = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI", 20),
                Location = new Point(15, 15),
                Size = new Size(40, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(lblIcon);

            // Value
            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(65, 15),
                Size = new Size(120, 30),
                ForeColor = color
            };
            card.Controls.Add(lblValue);

            // Title
            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9),
                Location = new Point(65, 50),
                Size = new Size(120, 20),
                ForeColor = Color.Gray
            };
            card.Controls.Add(lblTitle);

            // Trend
            var lblTrend = new Label
            {
                Text = trend,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                Location = new Point(190, 20),
                Size = new Size(50, 20),
                ForeColor = trend.StartsWith("+") ? Color.FromArgb(76, 175, 80) : Color.FromArgb(244, 67, 54),
                TextAlign = ContentAlignment.MiddleRight
            };
            card.Controls.Add(lblTrend);

            return card;
        }

        private void LoadRecentActivity()
        {
            // Tạo DataTable mẫu cho hoạt động gần đây
            DataTable dt = new DataTable();
            dt.Columns.Add("ThoiGian", typeof(string));
            dt.Columns.Add("NoiDung", typeof(string));
            dt.Columns.Add("TrangThai", typeof(string));
            dt.Columns.Add("GiaTri", typeof(string));

            // Thêm dữ liệu mẫu
            dt.Rows.Add("10:30 AM", "Đơn hàng #DH0012", "Thành công", "5,250,000 đ");
            dt.Rows.Add("09:15 AM", "Nhập kho sản phẩm mới", "Hoàn tất", "150 sản phẩm");
            dt.Rows.Add("Hôm qua", "Khách hàng mới đăng ký", "Thành công", "15 khách hàng");
            dt.Rows.Add("Hôm qua", "Bảo trì hệ thống", "Đã hoàn thành", "Thành công");
            dt.Rows.Add("2 ngày trước", "Đơn hàng #DH0011", "Đang xử lý", "3,800,000 đ");

            dataGridViewRecent.DataSource = dt;

            // Định dạng DataGridView
            dataGridViewRecent.Columns["ThoiGian"].Width = 120;
            dataGridViewRecent.Columns["TrangThai"].Width = 100;
            dataGridViewRecent.Columns["GiaTri"].Width = 120;

            // Định dạng màu sắc cho các dòng
            dataGridViewRecent.RowPrePaint += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.RowIndex < dataGridViewRecent.Rows.Count)
                {
                    var row = dataGridViewRecent.Rows[e.RowIndex];
                    string trangThai = row.Cells["TrangThai"].Value?.ToString() ?? "";

                    if (trangThai == "Thành công" || trangThai == "Hoàn tất")
                        row.DefaultCellStyle.BackColor = Color.FromArgb(232, 245, 233);
                    else if (trangThai == "Đang xử lý")
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 224);
                }
            };
        }

        private void SetupChart()
        {
            // Tạo panel chart với dữ liệu mẫu
            panelChart.Paint += (s, e) =>
            {
                DrawSampleChart(e.Graphics);
            };
        }

        private void DrawSampleChart(Graphics g)
        {
            // Clear background
            g.Clear(Color.White);

            // Draw title
            using (var titleFont = new Font("Segoe UI", 12, FontStyle.Bold))
            using (var titleBrush = new SolidBrush(Color.FromArgb(51, 51, 76)))
            {
                g.DrawString("DOANH THU 7 NGÀY GẦN ĐÂY", titleFont, titleBrush, new PointF(20, 20));
            }

            // Sample data
            string[] days = { "T2", "T3", "T4", "T5", "T6", "T7", "CN" };
            decimal[] revenue = { 15.2M, 18.5M, 12.8M, 22.3M, 19.7M, 25.1M, 28.4M };

            // Chart area
            int chartX = 50;
            int chartY = 70;
            int chartWidth = panelChart.Width - 100;
            int chartHeight = panelChart.Height - 100;

            // Find max value for scaling
            decimal maxRevenue = 0;
            foreach (var rev in revenue)
            {
                if (rev > maxRevenue) maxRevenue = rev;
            }

            // Draw grid lines
            using (var gridPen = new Pen(Color.FromArgb(240, 240, 240)))
            {
                for (int i = 0; i <= 4; i++)
                {
                    int y = chartY + (int)(chartHeight * i / 4);
                    g.DrawLine(gridPen, chartX, y, chartX + chartWidth, y);
                }
            }

            // Draw bars
            int barWidth = (chartWidth - 20) / days.Length - 10;
            for (int i = 0; i < days.Length; i++)
            {
                int barHeight = (int)((revenue[i] / maxRevenue) * chartHeight);
                int x = chartX + 10 + i * (barWidth + 10);
                int y = chartY + chartHeight - barHeight;

                // Gradient fill
                using (var brush = new LinearGradientBrush(
                    new Rectangle(x, y, barWidth, barHeight),
                    Color.FromArgb(0, 150, 136),
                    Color.FromArgb(0, 120, 116),
                    LinearGradientMode.Vertical))
                {
                    g.FillRectangle(brush, x, y, barWidth, barHeight);
                }

                // Draw value on top
                using (var valueFont = new Font("Segoe UI", 8))
                using (var valueBrush = new SolidBrush(Color.FromArgb(51, 51, 76)))
                {
                    string valueText = revenue[i].ToString("0.0") + "M";
                    var valueSize = g.MeasureString(valueText, valueFont);
                    g.DrawString(valueText, valueFont, valueBrush,
                        x + (barWidth - valueSize.Width) / 2, y - 20);
                }

                // Draw day label
                using (var dayFont = new Font("Segoe UI", 9))
                using (var dayBrush = new SolidBrush(Color.Gray))
                {
                    var daySize = g.MeasureString(days[i], dayFont);
                    g.DrawString(days[i], dayFont, dayBrush,
                        x + (barWidth - daySize.Width) / 2, chartY + chartHeight + 5);
                }
            }

            // Draw Y-axis labels
            using (var axisFont = new Font("Segoe UI", 8))
            using (var axisBrush = new SolidBrush(Color.Gray))
            {
                for (int i = 0; i <= 4; i++)
                {
                    decimal value = maxRevenue * (4 - i) / 4;
                    string label = value.ToString("0") + "M";
                    int y = chartY + (int)(chartHeight * i / 4);
                    g.DrawString(label, axisFont, axisBrush, 10, y - 8);
                }
            }
        }

        // Cập nhật dữ liệu real-time (có thể gọi từ nơi khác)
        public void RefreshData()
        {
            LoadStatisticsCards();
            LoadRecentActivity();
            panelChart.Invalidate(); // Vẽ lại chart
        }
    }
}