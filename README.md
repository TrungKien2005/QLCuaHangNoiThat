UC_QuanLySanPham → đã có.

UC_QuanLyKho → dùng bảng kho + nha_cung_cap.

UC_QuanLyBanHang → nhập hóa đơn + chi tiết hóa đơn.

UC_QuanLyKhachHang → CRUD khach_hang.

UC_QuanLyNhanVien → CRUD nhan_vien.

UC_TaiChinh → thêm thu/chi.

UC_BaoCaoThongKe → chỉ cần vài DataGridView hoặc Chart.
Trang chủ

Quản lý sản phẩm

Quản lý khoA

Quản lý bán hàng

Quản lý khách hàng

Quản lý nhân viên

Quản lý tài chính

Báo cáo & thống kê

Quản lý nhà cung cấp
-------------------------------------------
QLCuaHangNoiThat/
├── 📁 DataAccess/
│   └── 📄 DatabaseHelper.cs
├── 📁 Models/
│   └── 📄 SanPham.cs
├── 📁 Repositories/ -Logic 2/8
│   ├── 📄 SanPhamRepository.cs
│   └── 📄 TaiChinhRepository.cs
├── 📁 UserControls/ - Chỉ sử dụng được khi làm UI thôi 8/8
│   ├── 📄 UC_QuanLySanPham.cs
│   ├── 📄 UC_QuanLySanPham.Designer.cs
│   ├── 📄 UC_TaiChinh.cs
│   └── 📄 UC_TaiChinh.Designer.cs
├── 📁 Forms/
│   └── 📄 FormMain.cs
├── 📄 Program.cs
├── 📄 App.config
└── 📄 QLCuaHangNoiThat.csproj
