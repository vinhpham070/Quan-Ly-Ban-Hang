using System.Data;

namespace QuanLyBanHang
{
    internal class DatabaseInitializer
    {
        Provider p = new Provider();

        public void Initialize()
        {
            // 1. Bảng Tài khoản
            string sql = @"
                CREATE TABLE IF NOT EXISTS User (
                    Username TEXT PRIMARY KEY,
                    Password TEXT NOT NULL
                );
            ";
            p.ExecuteNonQuery(CommandType.Text, sql);

            // 2. Bảng Bàn + Dữ liệu mồi
            sql = @"
                CREATE TABLE IF NOT EXISTS Ban (
                    MaBan INTEGER PRIMARY KEY AUTOINCREMENT,
                    TenBan TEXT NOT NULL
                );

                INSERT INTO Ban (TenBan)
                SELECT TenBan FROM (
                    SELECT 'Bàn 01' AS TenBan
                    UNION ALL SELECT 'Bàn 02'
                    UNION ALL SELECT 'Bàn 03'
                    UNION ALL SELECT 'Bàn 04'
                    UNION ALL SELECT 'Bàn 05'
                    UNION ALL SELECT 'Bàn VIP 1'
                    UNION ALL SELECT 'Mang về'
                )
                WHERE NOT EXISTS (SELECT 1 FROM Ban);
            ";
            p.ExecuteNonQuery(CommandType.Text, sql);

            // 3. Bảng Món ăn / Nước uống + Dữ liệu mồi
            sql = @"
                CREATE TABLE IF NOT EXISTS MonAn (
                    MaMon INTEGER PRIMARY KEY AUTOINCREMENT,
                    TenMon TEXT NOT NULL,
                    DonGia REAL NOT NULL CHECK (DonGia >= 0)
                );

                INSERT INTO MonAn (TenMon, DonGia)
                SELECT TenMon, DonGia FROM (
                    SELECT 'Sinh tố bơ' AS TenMon, 35000 AS DonGia
                    UNION ALL SELECT 'Cà phê sữa đá', 25000
                    UNION ALL SELECT 'Trà đào cam sả', 32000
                    UNION ALL SELECT 'Bạc xỉu', 29000
                    UNION ALL SELECT 'Mì xào bò', 45000
                    UNION ALL SELECT 'Cơm chiên dương châu', 40000
                )
                WHERE NOT EXISTS (SELECT 1 FROM MonAn);
            ";
            p.ExecuteNonQuery(CommandType.Text, sql);

            // 4. Bảng Hóa đơn (TrangThai: 0 = Chưa thanh toán, 1 = Đã thanh toán)
            sql = @"
                CREATE TABLE IF NOT EXISTS HoaDon (
                    MaHD INTEGER PRIMARY KEY AUTOINCREMENT,
                    MaBan INTEGER NOT NULL,
                    NgayLap TEXT NOT NULL,
                    TongTien REAL NOT NULL DEFAULT 0 CHECK (TongTien >= 0),
                    TrangThai INTEGER NOT NULL DEFAULT 0 CHECK (TrangThai IN (0, 1)),
                    FOREIGN KEY (MaBan) REFERENCES Ban(MaBan)
                );
            ";
            p.ExecuteNonQuery(CommandType.Text, sql);

            // 5. Bảng Chi tiết hóa đơn
            sql = @"
                CREATE TABLE IF NOT EXISTS ChiTietHoaDon (
                    MaHD INTEGER NOT NULL,
                    MaMon INTEGER NOT NULL,
                    SoLuong INTEGER NOT NULL CHECK (SoLuong > 0),
                    DonGia REAL NOT NULL CHECK (DonGia >= 0),
                    PRIMARY KEY (MaHD, MaMon),
                    FOREIGN KEY (MaHD) REFERENCES HoaDon(MaHD) ON DELETE CASCADE,
                    FOREIGN KEY (MaMon) REFERENCES MonAn(MaMon)
                );
            ";
            p.ExecuteNonQuery(CommandType.Text, sql);
        }
    }
}