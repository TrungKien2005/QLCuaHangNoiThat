using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.DataAccess;
using QLCuaHangNoiThat.Models;
using QLCuaHangNoiThat.Repositories;

namespace QLCuaHangNoiThat.UserControls
{
    public partial class UC_QuanLySanPham : UserControl
    {
        private readonly SanPhamRepository _repo = new SanPhamRepository();
        //private readonly DanhMucRepository _danhMucRepo = new DanhMucRepository();
        private DataGridViewImageColumn dgvImageColumn;
        private DataGridViewCheckBoxColumn dgvTrangThaiColumn;
        private Label lblKetQuaTimKiem;

        private Image ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        // Add this method to provide a default image
        private Image CreateDefaultImage()
        {
            int width = 70;
            int height = 70;
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.LightGray);
                using (Pen pen = new Pen(Color.Gray, 2))
                {
                    g.DrawRectangle(pen, 1, 1, width - 2, height - 2);
                }
                using (Font font = new Font("Segoe UI", 10, FontStyle.Bold))
                using (Brush brush = new SolidBrush(Color.DarkGray))
                {
                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    g.DrawString("No Image", font, brush, new RectangleF(0, 0, width, height), sf);
                }
            }
            return bmp;
        }

        public UC_QuanLySanPham()
        {
            InitializeComponent();
            SetupUI();
            // DEBUG: Kiểm tra trạng thái nút
            Console.WriteLine($"=== DEBUG BUTTON STATE ===");
            Console.WriteLine($"btnQuanLyDanhMuc Enabled: {btnQuanLyDanhMuc?.Enabled}");
            Console.WriteLine($"btnQuanLyDanhMuc Visible: {btnQuanLyDanhMuc?.Visible}");

            AddEmergencyTestButton();
            // ĐĂNG KÝ NHIỀU SỰ KIỆN
            this.Load += UC_QuanLySanPham_Load;
        }
        private bool _isDataLoaded = false;
        private void UC_QuanLySanPham_Load(object sender, EventArgs e)
        {
            if (!_isDataLoaded)
            {
                LoadData();
                _isDataLoaded = true;
            }
        }
        private void UC_QuanLySanPham_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && !_isDataLoaded)
            {
                LoadData();
                _isDataLoaded = true;
            }
        }
        private void UC_QuanLySanPham_Enter(object sender, EventArgs e)
        {
            // Refresh data mỗi khi focus vào usercontrol
            LoadData();
        }

        private void SetupUI()
        {
            // Cấu hình DataGridView
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(44, 62, 80),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dataGridView1.RowTemplate.Height = 80;

            // TẠO LABEL KẾT QUẢ
            lblKetQuaTimKiem = new Label();
            lblKetQuaTimKiem.Name = "lblKetQuaTimKiem";
            lblKetQuaTimKiem.Location = new Point(txtTimKiem.Location.X, txtTimKiem.Location.Y + txtTimKiem.Height + 5);
            lblKetQuaTimKiem.Size = new Size(200, 20);
            lblKetQuaTimKiem.ForeColor = Color.Blue;
            lblKetQuaTimKiem.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            this.Controls.Add(lblKetQuaTimKiem);

            // Cấu hình các nút
            SetupButtonStyle(btnThem, Color.FromArgb(46, 204, 113));
            SetupButtonStyle(btnSua, Color.FromArgb(52, 152, 219));
            SetupButtonStyle(btnXoa, Color.FromArgb(231, 76, 60));
            SetupButtonStyle(btnRefresh, Color.FromArgb(241, 196, 15));
            //SetupButtonStyle(btnQuanLyDanhMuc, Color.FromArgb(155, 89, 182));

            // CẤU HÌNH PLACEHOLDER VÀ SỰ KIỆN
            SetupSearchPlaceholder();
            // Thiết lập combobox lọc
            //SetupFilterControls();

            Console.WriteLine($"Đã đăng ký sự kiện TextChanged cho txtTimKiem");
        }
        // Replace all occurrences of 'cmbDanhMuc' with 'cboDanhMucFilter' in SetupFilterControls
        //private void SetupFilterControls()
        //{
        //    if (cboDanhMucFilter != null && !DesignMode)
        //    {
        //        try
        //        {
        //            var danhMucList = _danhMucRepo.GetAll();
        //            cboDanhMucFilter.Items.Clear();
        //            cboDanhMucFilter.Items.Add("Tất cả");

        //            if (danhMucList != null && danhMucList.Rows.Count > 0)
        //            {
        //                foreach (DataRow row in danhMucList.Rows)
        //                {
        //                    if (row["TenDanhMuc"] != null)
        //                        cboDanhMucFilter.Items.Add(row["TenDanhMuc"].ToString());
        //                }
        //            }
        //            cboDanhMucFilter.SelectedIndex = 0;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Lỗi SetupFilterControls: {ex.Message}");
        //            // Fallback an toàn
        //            cboDanhMucFilter.Items.Clear();
        //            cboDanhMucFilter.Items.Add("Tất cả");
        //            cboDanhMucFilter.SelectedIndex = 0;
        //        }
        //    }
        //}

        // Add this method to the UC_QuanLySanPham class
        private void SetupSearchPlaceholder()
        {
            if (txtTimKiem != null)
            {
                txtTimKiem.ForeColor = Color.Gray;
                txtTimKiem.Text = "Tìm kiếm sản phẩm...";

                // NÚT XÓA
                Button btnClearSearch = new Button();
                btnClearSearch.Text = "X";
                btnClearSearch.Size = new Size(25, txtTimKiem.Height - 4);
                btnClearSearch.Location = new Point(txtTimKiem.Right - btnClearSearch.Width - 3, txtTimKiem.Top + 2);
                btnClearSearch.BackColor = Color.Transparent;
                btnClearSearch.FlatStyle = FlatStyle.Flat;
                btnClearSearch.Cursor = Cursors.Hand;
                btnClearSearch.Click += (s, e) =>
                {
                    txtTimKiem.Text = "";
                    txtTimKiem.Focus();
                    LoadData();
                    lblKetQuaTimKiem.Text = "";
                };
                this.Controls.Add(btnClearSearch);
                btnClearSearch.BringToFront();

                // SỰ KIỆN FOCUS
                txtTimKiem.GotFocus += (s, e) =>
                {
                    if (txtTimKiem.Text == "Tìm kiếm sản phẩm...")
                    {
                        txtTimKiem.Text = "";
                        txtTimKiem.ForeColor = Color.Black;
                    }
                };

                txtTimKiem.LostFocus += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
                    {
                        txtTimKiem.Text = "Tìm kiếm sản phẩm...";
                        txtTimKiem.ForeColor = Color.Gray;
                        lblKetQuaTimKiem.Text = "";
                    }
                };

                // QUAN TRỌNG: ĐĂNG KÝ SỰ KIỆN TEXT CHANGED
                txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            }
        }

        // Add this method to the UC_QuanLySanPham class
        private void SetupButtonStyle(Button button, Color backColor)
        {
            button.BackColor = backColor;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }

        private void SetupDataGridViewColumns()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            // Cột Hình ảnh - SỬA LẠI
            dgvImageColumn = new DataGridViewImageColumn();
            dgvImageColumn.Name = "HinhAnh";
            dgvImageColumn.HeaderText = "Hình Ảnh";
            dgvImageColumn.DataPropertyName = "HinhAnh"; // Cột chứa Image object
            dgvImageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dgvImageColumn.Width = 80;
            dgvImageColumn.ReadOnly = true;
            dataGridView1.Columns.Add(dgvImageColumn);

            // Cột Mã SP
            DataGridViewTextBoxColumn colMaSP = new DataGridViewTextBoxColumn();
            colMaSP.Name = "MaSP";
            colMaSP.HeaderText = "Mã SP";
            colMaSP.DataPropertyName = "MaSP";
            colMaSP.Width = 70;
            colMaSP.ReadOnly = true;
            dataGridView1.Columns.Add(colMaSP);

            // Cột Tên SP
            DataGridViewTextBoxColumn colTenSP = new DataGridViewTextBoxColumn();
            colTenSP.Name = "TenSP";
            colTenSP.HeaderText = "Tên Sản Phẩm";
            colTenSP.DataPropertyName = "TenSP";
            colTenSP.Width = 150;
            colTenSP.ReadOnly = true;
            dataGridView1.Columns.Add(colTenSP);

            // Cột Danh Mục
            DataGridViewTextBoxColumn colDanhMuc = new DataGridViewTextBoxColumn();
            colDanhMuc.Name = "DanhMuc";
            colDanhMuc.HeaderText = "Danh Mục";
            colDanhMuc.DataPropertyName = "DanhMuc";
            colDanhMuc.Width = 100;
            colDanhMuc.ReadOnly = true;
            dataGridView1.Columns.Add(colDanhMuc);

            // Cột Giá
            DataGridViewTextBoxColumn colGia = new DataGridViewTextBoxColumn();
            colGia.Name = "Gia";
            colGia.HeaderText = "Giá (VNĐ)";
            colGia.DataPropertyName = "Gia";
            colGia.Width = 100;
            colGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colGia.DefaultCellStyle.Format = "N0";
            colGia.ReadOnly = true;
            dataGridView1.Columns.Add(colGia);

            // Cột Số Lượng
            DataGridViewTextBoxColumn colSoLuong = new DataGridViewTextBoxColumn();
            colSoLuong.Name = "SoLuong";
            colSoLuong.HeaderText = "Số Lượng";
            colSoLuong.DataPropertyName = "SoLuong";
            colSoLuong.Width = 80;
            colSoLuong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSoLuong.ReadOnly = true;
            dataGridView1.Columns.Add(colSoLuong);

            // Cột Mô Tả
            DataGridViewTextBoxColumn colMoTa = new DataGridViewTextBoxColumn();
            colMoTa.Name = "MoTa";
            colMoTa.HeaderText = "Mô Tả";
            colMoTa.DataPropertyName = "MoTa";
            colMoTa.Width = 150;
            colMoTa.ReadOnly = true;
            dataGridView1.Columns.Add(colMoTa);

            // Cột SKU
            DataGridViewTextBoxColumn colSKU = new DataGridViewTextBoxColumn();
            colSKU.Name = "SKU";
            colSKU.HeaderText = "SKU";
            colSKU.DataPropertyName = "SKU";
            colSKU.Width = 100;
            colSKU.ReadOnly = true;
            dataGridView1.Columns.Add(colSKU);

            // Cột Trạng Thái (Checkbox Đang Bán)
            dgvTrangThaiColumn = new DataGridViewCheckBoxColumn();
            dgvTrangThaiColumn.Name = "TrangThai";
            dgvTrangThaiColumn.HeaderText = "Đang Bán";
            dgvTrangThaiColumn.DataPropertyName = "DangKinhDoanh";
            dgvTrangThaiColumn.Width = 80;
            dgvTrangThaiColumn.ReadOnly = false; // Cho phép chỉnh sửa
            dataGridView1.Columns.Add(dgvTrangThaiColumn);

            // Cột Tình Trạng (Combobox)
            DataGridViewComboBoxColumn colTinhTrang = new DataGridViewComboBoxColumn();
            colTinhTrang.Name = "TinhTrang";
            colTinhTrang.HeaderText = "Tình Trạng";
            colTinhTrang.DataPropertyName = "TinhTrang";
            colTinhTrang.Width = 120;
            colTinhTrang.ReadOnly = false; // Cho phép chỉnh sửa
            colTinhTrang.Items.AddRange("Còn hàng", "Hết hàng", "Ngừng sản xuất");
            dataGridView1.Columns.Add(colTinhTrang);
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = _repo.GetAllWithDanhMuc();

                if (dt == null || dt.Rows.Count == 0)
                {
                    ShowSampleData();
                    return;
                }

                // THÊM CỘT MỚI CHO HÌNH ẢNH ĐÃ XỬ LÝ
                if (!dt.Columns.Contains("HinhAnh"))
                {
                    dt.Columns.Add("HinhAnh", typeof(Image));
                }

                // XỬ LÝ HÌNH ẢNH CHO MỖI DÒNG
                foreach (DataRow row in dt.Rows)
                {
                    string hinhAnhURL = row["HinhAnhURL"]?.ToString();
                    Image imageToDisplay;

                    if (!string.IsNullOrEmpty(hinhAnhURL) && File.Exists(hinhAnhURL))
                    {
                        try
                        {
                            Image img = Image.FromFile(hinhAnhURL);
                            imageToDisplay = ResizeImage(img, 70, 70);
                        }
                        catch
                        {
                            imageToDisplay = CreateDefaultImage();
                        }
                    }
                    else
                    {
                        imageToDisplay = CreateDefaultImage();
                    }

                    // GÁN HÌNH ẢNH ĐÃ XỬ LÝ VÀO CỘT MỚI
                    row["HinhAnh"] = imageToDisplay;
                }

                SetupDataGridViewColumns();
                dataGridView1.DataSource = dt;

                // Thêm sự kiện
                dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
                dataGridView1.CurrentCellDirtyStateChanged += DataGridView1_CurrentCellDirtyStateChanged;
                dataGridView1.DataError += DataGridView1_DataError;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowSampleData();
            }
        }

        // THÊM PHƯƠNG THỨC XỬ LÝ LỖI DATA GRIDVIEW
        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Bỏ qua lỗi hiển thị hình ảnh
            if (e.Exception is ArgumentException && dataGridView1.Columns[e.ColumnIndex] is DataGridViewImageColumn)
            {
                e.ThrowException = false;
            }
        }

        private void DataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            try
            {
                // Xử lý khi thay đổi cột Trạng Thái (Đang Bán)
                if (dataGridView1.Columns[e.ColumnIndex].Name == "TrangThai")
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    int maSP = Convert.ToInt32(row.Cells["MaSP"].Value);
                    bool dangKinhDoanh = Convert.ToBoolean(row.Cells["TrangThai"].Value);

                    // Cập nhật database
                    _repo.UpdateTrangThaiKinhDoanh(maSP, dangKinhDoanh);

                    // Tự động cập nhật Tình Trạng
                    if (dangKinhDoanh)
                    {
                        row.Cells["TinhTrang"].Value = "Còn hàng";
                    }
                    else
                    {
                        // Nếu tắt Đang Bán, cho phép chọn các tình trạng khác
                        // Giữ nguyên giá trị hiện tại hoặc đặt mặc định
                        if (row.Cells["TinhTrang"].Value?.ToString() == "Còn hàng")
                            row.Cells["TinhTrang"].Value = "Ngừng sản xuất";
                    }

                    MessageBox.Show($"Đã cập nhật trạng thái kinh doanh cho sản phẩm!", "Thành công",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Xử lý khi thay đổi cột Tình Trạng
                if (dataGridView1.Columns[e.ColumnIndex].Name == "TinhTrang")
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    string tinhTrang = row.Cells["TinhTrang"].Value?.ToString();

                    // Nếu chọn "Còn hàng" thì tự động bật Đang Bán
                    if (tinhTrang == "Còn hàng")
                    {
                        row.Cells["TrangThai"].Value = true;
                    }
                    // Nếu chọn "Hết hàng" hoặc "Ngừng sản xuất" thì tắt Đang Bán
                    else if (tinhTrang == "Hết hàng" || tinhTrang == "Ngừng sản xuất")
                    {
                        row.Cells["TrangThai"].Value = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadData(); // Reload data để tránh inconsistent state
            }
        }

        private void ShowSampleData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("MaSP", typeof(int));
                dt.Columns.Add("TenSP", typeof(string));
                dt.Columns.Add("DanhMuc", typeof(string));
                dt.Columns.Add("Gia", typeof(decimal));
                dt.Columns.Add("SoLuong", typeof(int));
                dt.Columns.Add("MoTa", typeof(string));
                dt.Columns.Add("SKU", typeof(string));
                dt.Columns.Add("DangKinhDoanh", typeof(bool));
                dt.Columns.Add("TinhTrang", typeof(string));
                dt.Columns.Add("HinhAnhURL", typeof(Image));

                Image defaultImage = CreateDefaultImage();

                dt.Rows.Add(1, "Ghế Sofa Gỗ Phòng Khách", "Phòng khách", 5000000, 10,
                           "Ghế sofa gỗ cao cấp", "SOFA-001", true, "Còn hàng", defaultImage);
                dt.Rows.Add(2, "Bàn Ăn Gỗ 6 Ghế", "Phòng ăn", 3500000, 0,
                           "Bàn ăn gỗ tự nhiên", "BANAN-001", false, "Hết hàng", defaultImage);
                dt.Rows.Add(3, "Giường Ngủ Queen Size", "Phòng ngủ", 8000000, 3,
                           "Giường ngủ size Queen", "GIUONG-001", true, "Còn hàng", defaultImage);

                SetupDataGridViewColumns();
                dataGridView1.DataSource = dt;

                // Thêm sự kiện
                dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
                dataGridView1.CurrentCellDirtyStateChanged += DataGridView1_CurrentCellDirtyStateChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Các phương thức khác giữ nguyên...
        // (ResizeImage, CreateDefaultImage, SetupFilterControls, LoadDanhMucFilter, 
        // SetupButtonStyle, SetupSearchPlaceholder, và các event handlers)

        private void btnLoc_Click(object sender, EventArgs e)
        {
            // TODO: Implement filter logic here
            // For now, just refresh or reload data as a placeholder
            LoadData();
        }
        //}
        // Add this method to handle the TextChanged event for txtTimKiem
        // PHƯƠNG THỨC XỬ LÝ TEXT CHANGED - CHỈ MỘT
        private Timer _searchTimer;

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine("TextChanged fired: " + txtTimKiem.Text);  // DEBUG
            if (txtTimKiem.Text == "Tìm kiếm sản phẩm..." ||
                string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                LoadData();
                lblKetQuaTimKiem.Text = "";
                return;
            }

            if (_searchTimer == null)
            {
                _searchTimer = new Timer();
                _searchTimer.Interval = 500;
                _searchTimer.Tick += (s, args) =>
                {
                    _searchTimer.Stop();
                    SearchProducts(txtTimKiem.Text);
                };
            }

            // reset timer mỗi lần gõ
            _searchTimer.Stop();
            _searchTimer.Start();
        }

        // PHƯƠNG THỨC SEARCHPRODUCTS - CHỈ MỘT
        private void SearchProducts(string keyword)
        {
            try
            {
                DataTable dt = _repo.SearchSanPham(keyword);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // XỬ LÝ HÌNH ẢNH
                    if (!dt.Columns.Contains("HinhAnh"))
                    {
                        dt.Columns.Add("HinhAnh", typeof(Image));
                    }

                    foreach (DataRow row in dt.Rows)
                    {
                        string hinhAnhURL = row["HinhAnhURL"]?.ToString();
                        Image imageToDisplay;

                        if (!string.IsNullOrEmpty(hinhAnhURL) && File.Exists(hinhAnhURL))
                        {
                            try
                            {
                                Image img = Image.FromFile(hinhAnhURL);
                                imageToDisplay = ResizeImage(img, 70, 70);
                            }
                            catch
                            {
                                imageToDisplay = CreateDefaultImage();
                            }
                        }
                        else
                        {
                            imageToDisplay = CreateDefaultImage();
                        }
                        row["HinhAnh"] = imageToDisplay;
                    }

                    dataGridView1.DataSource = dt;
                    lblKetQuaTimKiem.Text = $"Tìm thấy {dt.Rows.Count} sản phẩm";
                }
                else
                {
                    dataGridView1.DataSource = null;
                    lblKetQuaTimKiem.Text = "Không tìm thấy sản phẩm nào";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AddEmergencyTestButton()
        {
            Button btnEmergencyTest = new Button();
            btnEmergencyTest.Text = "EMERGENCY TEST";
            btnEmergencyTest.Location = new Point(300, 10);
            btnEmergencyTest.BackColor = Color.Red;
            btnEmergencyTest.ForeColor = Color.White;
            btnEmergencyTest.Click += (s, e) =>
            {
                try
                {
                    Console.WriteLine("=== EMERGENCY TEST ===");

                    // Test 1: Kiểm tra repository
                    Console.WriteLine("Testing repository...");
                    DataTable dt = _repo.SearchSanPham("ghế");
                    Console.WriteLine($"Repository result: {dt.Rows.Count} rows");

                    // Test 2: Kiểm tra textbox - SỬA LỖI Ở ĐÂY
                    Console.WriteLine($"TextBox text: '{txtTimKiem.Text}'");
                    Console.WriteLine($"TextBox name: {txtTimKiem.Name}");

                    // Test 3: Thêm event handler test
                    txtTimKiem.TextChanged += (sender, args) =>
                    {
                        Console.WriteLine($"TEST EVENT: Text changed to '{txtTimKiem.Text}'");
                    };
                    Console.WriteLine("Test event handler registered");

                    // Test 4: Gọi trực tiếp search
                    Console.WriteLine("Calling SearchProducts directly...");
                    SearchProducts("ghế");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"!!! Emergency test error: {ex.Message}");
                    MessageBox.Show($"Lỗi test: {ex.Message}", "Lỗi",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            this.Controls.Add(btnEmergencyTest);
        }
        // Add this method to your UC_QuanLySanPham class
        private void btnQuanLyDanhMuc_Click(object sender, EventArgs e)
        {
            try
            {
                FormQuanLyDanhMuc formDanhMuc = new FormQuanLyDanhMuc();
                formDanhMuc.StartPosition = FormStartPosition.CenterParent;

                // Reload danh mục khi form đóng
                formDanhMuc.FormClosed += (s, args) =>
                {
                    // Reload combobox danh mục trong bộ lọc
                    //SetupFilterControls();

                    // Reload lại dữ liệu sản phẩm để cập nhật danh mục mới
                    LoadData();
                };

                formDanhMuc.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở quản lý danh mục: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Add this method to your UC_QuanLySanPham class (e.g., in UC_QuanLySanPham.cs)
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Refresh or reload your data here
            LoadData();
        }
        // Add this method to your UC_QuanLySanPham class
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                try
                {
                    int maSP = Convert.ToInt32(dataGridView1.CurrentRow.Cells["MaSP"].Value);
                    string tenSP = dataGridView1.CurrentRow.Cells["TenSP"].Value?.ToString() ?? "Unknown";

                    // KIỂM TRA SẢN PHẨM CÓ ĐANG ĐƯỢC SỬ DỤNG
                    bool isInUse = _repo.IsSanPhamInUse(maSP);

                    if (isInUse)
                    {
                        MessageBox.Show($"Không thể xóa sản phẩm '{tenSP}'!\n\nSản phẩm này đang được sử dụng trong hệ thống.\nHãy tắt trạng thái 'Đang bán' thay vì xóa.",
                                      "KHÔNG THỂ XÓA",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Warning);
                        return;
                    }

                    // XÁC NHẬN XÓA
                    DialogResult result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa sản phẩm '{tenSP}' (Mã: {maSP})?\n\nHành động này không thể hoàn tác!",
                        "XÁC NHẬN XÓA",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {
                        // THỰC HIỆN XÓA
                        bool deleteResult = _repo.DeleteSanPham(maSP);

                        if (deleteResult)
                        {
                            MessageBox.Show($"Đã xóa sản phẩm '{tenSP}' thành công!",
                                          "THÀNH CÔNG",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Information);

                            // REFRESH DỮ LIỆU
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show($"Không thể xóa sản phẩm '{tenSP}'!",
                                          "LỖI",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa sản phẩm: {ex.Message}",
                                  "LỖI",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);

                    Console.WriteLine($"Chi tiết lỗi xóa: {ex.ToString()}");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!",
                              "THÔNG BÁO",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
            }
        }
        // Add this method to your UC_QuanLySanPham.cs file (code-behind for the UserControl)
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                try
                {
                    int maSP = Convert.ToInt32(dataGridView1.CurrentRow.Cells["MaSP"].Value);
                    string tenSP = dataGridView1.CurrentRow.Cells["TenSP"].Value.ToString();

                    Console.WriteLine($"Đang mở form sửa cho sản phẩm: {maSP} - {tenSP}"); // DEBUG

                    // Lấy thông tin sản phẩm từ repository
                    SanPham sanPham = _repo.GetById(maSP);

                    if (sanPham != null)
                    {
                        Console.WriteLine($"Tìm thấy sản phẩm: {sanPham.TenSanPham}"); // DEBUG
                        FormThemSuaSanPham formSua = new FormThemSuaSanPham(sanPham);
                        if (formSua.ShowDialog() == DialogResult.OK)
                        {
                            LoadData(); // Refresh sau khi sửa
                            MessageBox.Show("Đã cập nhật sản phẩm thành công!", "Thành công",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin sản phẩm!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi mở form sửa: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"Chi tiết lỗi: {ex.ToString()}"); // DEBUG
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // Add this method to your UC_QuanLySanPham.cs file (code-behind)
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                FormThemSuaSanPham formThem = new FormThemSuaSanPham();
                if (formThem.ShowDialog() == DialogResult.OK)
                {
                    LoadData(); // Refresh sau khi thêm
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form thêm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Thêm nút test tạm thời
        private void AddTestButton()
        {
            Button btnTestSearch = new Button();
            btnTestSearch.Text = "Test Search";
            btnTestSearch.Location = new Point(250, 10);
            btnTestSearch.Click += (s, e) =>
            {
                try
                {
                    Console.WriteLine("Testing search directly...");
                    DataTable dt = _repo.SearchSanPham("ghế");
                    Console.WriteLine($"Direct search result: {dt.Rows.Count} rows");

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            Console.WriteLine($"Product: {row["TenSP"]}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Direct search error: {ex.Message}");
                }
            };
            this.Controls.Add(btnTestSearch);
        }



        // QLCuaHangNoiThat\UserControls\UC_QuanLySanPham.Designer.cs
        // Add this method to the UC_QuanLySanPham partial class (preferably in the code-behind .cs file, not the designer)
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // You can implement your logic here, or leave it empty if not needed
        }

        // QLCuaHangNoiThat\UserControls\UC_QuanLySanPham.cs


    }
}