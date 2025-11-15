using System.Windows.Forms;

namespace QLCuaHangNoiThat.UserControls
{
    partial class UC_QuanLySanPham
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridView1;
        private TextBox txtMaSP;
        private TextBox txtTenSP;
        private TextBox txtGia;
        private TextBox txtSoLuong;
        private TextBox txtDanhMuc;
        private TextBox txtMoTa;
        private TextBox txtTimKiem;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnRefresh;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;

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
            this.dataGridView1 = new DataGridView();
            this.txtMaSP = new TextBox();
            this.txtTenSP = new TextBox();
            this.txtGia = new TextBox();
            this.txtSoLuong = new TextBox();
            this.txtDanhMuc = new TextBox();
            this.txtMoTa = new TextBox();
            this.txtTimKiem = new TextBox();
            this.btnThem = new Button();
            this.btnSua = new Button();
            this.btnXoa = new Button();
            this.btnRefresh = new Button();

            // Thiết lập properties và layout cho các controls
            // ... (code thiết lập UI)

            // Thêm controls vào UserControl
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtMaSP);
            this.Controls.Add(this.txtTenSP);
            this.Controls.Add(this.txtGia);
            this.Controls.Add(this.txtSoLuong);
            this.Controls.Add(this.txtDanhMuc);
            this.Controls.Add(this.txtMoTa);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnRefresh);

            // Gán sự kiện
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            this.Load += new System.EventHandler(this.UC_QuanLySanPham_Load);
        }
    }
}