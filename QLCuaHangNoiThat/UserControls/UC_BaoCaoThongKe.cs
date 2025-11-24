using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using QLCuaHangNoiThat.Repositories;

namespace QLCuaHangNoiThat.UserControls
{
    public partial class UC_BaoCaoThongKe : UserControl
    {
        private readonly BaoCaoRepository _repo = new BaoCaoRepository();

        public UC_BaoCaoThongKe()
        {
            InitializeComponent();
            this.Load += UC_BaoCaoThongKe_Load;
        }

        private void UC_BaoCaoThongKe_Load(object sender, EventArgs e)
        {
            dtpTuNgay.Value = DateTime.Now.AddMonths(-1);
            dtpDenNgay.Value = DateTime.Now;

            LoadDoanhThu();
            LoadTopSP();
            LoadTonKho();

            btnLoc.Click += BtnLoc_Click;
            btnXuatPDF.Click += BtnXuatPDF_Click;
        }

        private void LoadDoanhThu()
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;

            DataTable dt = _repo.GetDoanhThu(tuNgay, denNgay);
            dgvDoanhThu.DataSource = dt;

            // Chart
            chartDoanhThu.Series.Clear();
            var series = chartDoanhThu.Series.Add("DoanhThu");
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            foreach (DataRow row in dt.Rows)
            {
                series.Points.AddXY(row["Ngay"].ToString(), Convert.ToDecimal(row["TongTien"]));
            }
        }

        private void LoadTopSP()
        {
            try
            {
                dgvTopSP.DataSource = _repo.GetTopSanPham();
            }
            catch
            {
                dgvTopSP.DataSource = null;
            }
        }

        private void LoadTonKho()
        {
            dgvTonKho.DataSource = _repo.GetTonKho();
        }

        private void BtnLoc_Click(object sender, EventArgs e)
        {
            LoadDoanhThu();
        }

        private void BtnXuatPDF_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "PDF File|*.pdf" })
                {
                    if (sfd.ShowDialog() != DialogResult.OK) return;

                    DataTable dt = dgvDoanhThu.DataSource as DataTable;
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu để xuất!");
                        return;
                    }

                    Document doc = new Document(PageSize.A4, 10, 10, 10, 10);
                    PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                    doc.Open();

                    PdfPTable table = new PdfPTable(dt.Columns.Count);

                    // Header
                    foreach (DataColumn col in dt.Columns)
                        table.AddCell(new Phrase(col.ColumnName));

                    // Data
                    foreach (DataRow row in dt.Rows)
                        foreach (var cell in row.ItemArray)
                            table.AddCell(new Phrase(cell.ToString()));

                    doc.Add(table);
                    doc.Close();

                    MessageBox.Show("✅ Xuất PDF thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi khi xuất PDF: {ex.Message}");
            }
        }
    }
}
