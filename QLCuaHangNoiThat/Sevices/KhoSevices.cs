using System;
using System.Data;
using MySql.Data.MySqlClient;
using QLCuaHangNoiThat.Models;
using QLCuaHangNoiThat.Repositories;

namespace QLCuaHangNoiThat.Services
{
    public class KhoService
    {
        private readonly string _connectionString =
            "Server=localhost;Database=qlcuahangnoithat;Uid=root;Pwd=;";

        private readonly PhieuNhapKhoRepository _phieuNhapRepo = new PhieuNhapKhoRepository();
        private readonly PhieuXuatKhoRepository _phieuXuatRepo = new PhieuXuatKhoRepository();
        private readonly TonKhoRepository _tonKhoRepo = new TonKhoRepository();
        private readonly KhoRepository repository = new KhoRepository();

        // Sự kiện để UC_QuanLyKho subscribe, refresh realtime
        public static event Action TonKhoChanged;

        /// <summary>
        /// Nhập hàng (1 sản phẩm), có transaction & rollback
        /// </summary>
public bool NhapHang(PhieuNhapKho phieu, int maSanPham, int soLuong, decimal donGia)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Lưu phiếu nhập
                        int maPhieu = _phieuNhapRepo.Insert(phieu);

                        // 2. Lưu chi tiết phiếu nhập
                        string qCT = @"
                    INSERT INTO ChiTietPhieuNhap(MaPhieuNhap, MaSanPham, SoLuong, DonGia)
                    VALUES(@MaPhieu, @SP, @SL, @DonGia)";
                        var cmdCT = new MySqlCommand(qCT, conn, tran);
                        cmdCT.Parameters.AddWithValue("@MaPhieu", maPhieu);
                        cmdCT.Parameters.AddWithValue("@SP", maSanPham);
                        cmdCT.Parameters.AddWithValue("@SL", soLuong);
                        cmdCT.Parameters.AddWithValue("@DonGia", donGia);
                        cmdCT.ExecuteNonQuery();

                        // 3. Cập nhật tồn kho
                        int tonHienTai = _tonKhoRepo.GetSoLuongTon(maSanPham, phieu.MaKho, conn, tran);
                        int tonMoi = tonHienTai + soLuong;
                        _tonKhoRepo.SetSoLuongTon(maSanPham, phieu.MaKho, tonMoi, conn, tran);

                        // 4. Cập nhật SoLuongTon trong bảng SanPham (tổng toàn hệ thống)
                        string qSP = @"UPDATE SanPham 
                               SET SoLuongTon = IFNULL(SoLuongTon, 0) + @SL 
                               WHERE MaSanPham = @SP";
                        var cmdSP = new MySqlCommand(qSP, conn, tran);
                        cmdSP.Parameters.AddWithValue("@SL", soLuong);
                        cmdSP.Parameters.AddWithValue("@SP", maSanPham);
                        cmdSP.ExecuteNonQuery();

                        tran.Commit();

                        TonKhoChanged?.Invoke(); // bắn sự kiện realtime
                        return true;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool XuatHang(PhieuXuatKho phieu, int maSanPham, int soLuong, decimal donGia)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // 0. Kiểm tra tồn kho
                        int tonHienTai = _tonKhoRepo.GetSoLuongTon(maSanPham, phieu.MaKho, conn, tran);
                        if (tonHienTai < soLuong)
                            throw new Exception("Không đủ tồn kho để xuất!");

                        // 1. Lưu phiếu xuất
                        int maPhieu = _phieuXuatRepo.Insert(phieu);
                        // 2. Lưu chi tiết phiếu xuất
                        string qCT = @"
                    INSERT INTO ChiTietPhieuXuat(MaPhieuXuat, MaSanPham, SoLuong, DonGia)
                    VALUES(@MaPhieu, @SP, @SL, @DonGia)";
                        var cmdCT = new MySqlCommand(qCT, conn, tran);
                        cmdCT.Parameters.AddWithValue("@MaPhieu", maPhieu);
                        cmdCT.Parameters.AddWithValue("@SP", maSanPham);
                        cmdCT.Parameters.AddWithValue("@SL", soLuong);
                        cmdCT.Parameters.AddWithValue("@DonGia", donGia);
                        cmdCT.ExecuteNonQuery();

                        // 3. Cập nhật tồn kho
                        int tonMoi = tonHienTai - soLuong;
                        _tonKhoRepo.SetSoLuongTon(maSanPham, phieu.MaKho, tonMoi, conn, tran);

                        // 4. Cập nhật SoLuongTon trong bảng SanPham
                        string qSP = @"UPDATE SanPham 
                               SET SoLuongTon = IFNULL(SoLuongTon, 0) - @SL 
                               WHERE MaSanPham = @SP";
                        var cmdSP = new MySqlCommand(qSP, conn, tran);
                        cmdSP.Parameters.AddWithValue("@SL", soLuong);
                        cmdSP.Parameters.AddWithValue("@SP", maSanPham);
                        cmdSP.ExecuteNonQuery();

                        tran.Commit();

                        TonKhoChanged?.Invoke(); // bắn sự kiện realtime
                        return true;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }
        // Lấy danh sách kho
        public DataTable GetDanhSachKho()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Kho";
                var cmd = new MySqlCommand(query, conn);
                var adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // Lấy danh sách nhà cung cấp
        public DataTable GetDanhSachNhaCungCap()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
            SELECT MaNhaCungCap, TenNhaCungCap, NguoiLienLac, Email, SoDienThoai, DiaChi
            FROM NhaCungCap";
                var cmd = new MySqlCommand(query, conn);
                var adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // Thêm nhà cung cấp
        public bool ThemNhaCungCap(NhaCungCap ncc)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
            INSERT INTO NhaCungCap(TenNhaCungCap, NguoiLienLac, Email, SoDienThoai, DiaChi)
            VALUES(@TenNhaCungCap, @NguoiLienLac, @Email, @SoDienThoai, @DiaChi)";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenNhaCungCap", ncc.TenNhaCungCap);
                cmd.Parameters.AddWithValue("@NguoiLienLac", ncc.NguoiLienLac);
                cmd.Parameters.AddWithValue("@Email", ncc.Email);
                cmd.Parameters.AddWithValue("@SoDienThoai", ncc.SoDienThoai);
                cmd.Parameters.AddWithValue("@DiaChi", ncc.DiaChi);
                cmd.Parameters.AddWithValue("@MaNhaCungCap", ncc.MaNhaCungCap);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Sửa nhà cung cấp
        public bool SuaNhaCungCap(NhaCungCap ncc)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
            UPDATE NhaCungCap
            SET TenNhaCungCap=@TenNhaCungCap, NguoiLienLac=@NguoiLienLac, Email=@Email, SoDienThoai=@SoDienThoai, DiaChi=@DiaChi
            WHERE MaNhaCungCap=@MaNhaCungCap";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNhaCungCap", ncc.MaNhaCungCap);
                cmd.Parameters.AddWithValue("@TenNhaCungCap", ncc.TenNhaCungCap);
                cmd.Parameters.AddWithValue("@NguoiLienLac", ncc.NguoiLienLac);
                cmd.Parameters.AddWithValue("@Email", ncc.Email);
                cmd.Parameters.AddWithValue("@SoDienThoai", ncc.SoDienThoai);
                cmd.Parameters.AddWithValue("@DiaChi", ncc.DiaChi);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        // Xóa nhà cung cấp
        public bool XoaNhaCungCap(int maNhaCungCap)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"DELETE FROM NhaCungCap WHERE MaNhaCungCap=@MaNhaCungCap";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNhaCungCap", maNhaCungCap);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public bool ThemKho(Kho kho)
        {
            return repository.ThemKho(kho);
        }

        public bool SuaKho(Kho kho)
        {
            return repository.SuaKho(kho);
        }

        public bool XoaKho(int maKho)
        {
            return repository.XoaKho(maKho);
        }

    }
}
