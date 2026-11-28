using Microsoft.Data.Sqlite;
using QuanLyBanHang.Class;
using System;
using System.Collections.Generic;
using System.Data;

namespace QuanLyBanHang
{
    class QuanLyController
    {
        public DataTable LayTatCaBan()
        {
            try
            {
                string strSQL = "SELECT * FROM Ban";
                Provider p = new Provider();
                return p.Select(CommandType.Text, strSQL);
            }
            catch
            {
                throw;
            }
        }

        public DataTable TimKiemBan(string TenBan)
        {
            try
            {
                string strSQL = "SELECT * FROM Ban WHERE TenBan LIKE '%' || @TenBan || '%'";
                Provider p = new Provider();
                return p.Select(CommandType.Text, strSQL,
                    new SqliteParameter { ParameterName = "@TenBan", Value = TenBan }
                );
            }
            catch
            {
                throw;
            }
        }

        public DataTable LayTatCaMonAn()
        {
            try
            {
                string strSQL = "SELECT * FROM MonAn";
                Provider p = new Provider();
                return p.Select(CommandType.Text, strSQL);
            }
            catch
            {
                throw;
            }
        }

        public DataTable TimKiemMonAn(string TenMon)
        {
            try
            {
                string strSQL = "SELECT * FROM MonAn WHERE TenMon LIKE '%' || @TenMon || '%'";
                Provider p = new Provider();
                return p.Select(CommandType.Text, strSQL,
                    new SqliteParameter { ParameterName = "@TenMon", Value = TenMon }
                );
            }
            catch
            {
                throw;
            }
        }

        public int TimKiemHoaDon(int MaBan)
        {
            try
            {
                string strSQL = "SELECT MaHD FROM HoaDon WHERE MaBan = @MaBan AND TrangThai = 0";
                Provider p = new Provider();
                DataTable dt = p.Select(CommandType.Text, strSQL,
                    new SqliteParameter { ParameterName = "@MaBan", Value = MaBan }
                );

                if (dt != null && dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0]["MaHD"]);
                }
                return -1;
            }
            catch
            {
                throw;
            }
        }

        public ChiTietHoaDon? TimKiemChiTietHoaDon(int MaHD, int MaMon)
        {
            try
            {
                string strSQL = @"
                    SELECT ct.MaHD, ct.MaMon, m.TenMon, ct.SoLuong, ct.DonGia 
                    FROM ChiTietHoaDon ct
                    JOIN MonAn m ON ct.MaMon = m.MaMon
                    WHERE ct.MaHD = @MaHD AND ct.MaMon = @MaMon";

                Provider p = new Provider();
                DataTable dt = p.Select(CommandType.Text, strSQL,
                    new SqliteParameter { ParameterName = "@MaHD", Value = MaHD },
                    new SqliteParameter { ParameterName = "@MaMon", Value = MaMon }
                );

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new ChiTietHoaDon()
                    {
                        MaHD = Convert.ToInt32(row["MaHD"]),
                        MaMon = Convert.ToInt32(row["MaMon"]),
                        TenMon = row["TenMon"].ToString(),
                        SoLuong = Convert.ToInt32(row["SoLuong"]),
                        DonGia = Convert.ToDouble(row["DonGia"])
                    };
                }
                return null;
            }
            catch
            {
                throw;
            }
        }

        public List<ChiTietHoaDon> LayChiTietHoaDon(int MaHD)
        {
            try
            {
                string strSQL = @"
                    SELECT ct.MaHD, ct.MaMon, m.TenMon, ct.SoLuong, ct.DonGia 
                    FROM ChiTietHoaDon ct
                    JOIN MonAn m ON ct.MaMon = m.MaMon
                    WHERE ct.MaHD = @MaHD";

                Provider p = new Provider();
                DataTable dt = p.Select(CommandType.Text, strSQL,
                    new SqliteParameter { ParameterName = "@MaHD", Value = MaHD }
                );

                List<ChiTietHoaDon> dsChiTiet = new List<ChiTietHoaDon>();
                foreach (DataRow row in dt.Rows)
                {
                    dsChiTiet.Add(new ChiTietHoaDon
                    {
                        MaHD = Convert.ToInt32(row["MaHD"]),
                        MaMon = Convert.ToInt32(row["MaMon"]),
                        TenMon = row["TenMon"].ToString(),
                        SoLuong = Convert.ToInt32(row["SoLuong"]),
                        DonGia = Convert.ToDouble(row["DonGia"])
                    });
                }
                return dsChiTiet;
            }
            catch
            {
                throw;
            }
        }

        public int TangSoLuong(int MaHD, int MaMon, int SoLuongTang)
        {
            try
            {
                string strSQL = "UPDATE ChiTietHoaDon SET SoLuong = SoLuong + @SoLuongTang WHERE MaHD = @MaHD AND MaMon = @MaMon";
                Provider p = new Provider();
                return p.ExecuteNonQuery(CommandType.Text, strSQL,
                    new SqliteParameter { ParameterName = "@MaHD", Value = MaHD },
                    new SqliteParameter { ParameterName = "@MaMon", Value = MaMon },
                    new SqliteParameter { ParameterName = "@SoLuongTang", Value = SoLuongTang }
                );
            }
            catch
            {
                throw;
            }
        }

        public int TaoHoaDon(int MaBan)
        {
            try
            {
                string strSQL = @"
                    INSERT INTO HoaDon (MaBan, NgayTao, TrangThai) 
                    VALUES (@MaBan, @NgayTao, 0);
                    SELECT last_insert_rowid();";

                Provider p = new Provider();
                DataTable dt = p.Select(CommandType.Text, strSQL,
                    new SqliteParameter { ParameterName = "@MaBan", Value = MaBan },
                    new SqliteParameter { ParameterName = "@NgayTao", Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }
                );

                if (dt != null && dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0][0]);
                }
                return -1;
            }
            catch
            {
                throw;
            }
        }

        public int ThemChiTietHoaDon(int MaHD, int MaMon, int SoLuong, double DonGia)
        {
            try
            {
                string strSQL = @"
                    INSERT INTO ChiTietHoaDon (MaHD, MaMon, SoLuong, DonGia) 
                    VALUES (@MaHD, @MaMon, @SoLuong, @DonGia)";

                Provider p = new Provider();
                return p.ExecuteNonQuery(CommandType.Text, strSQL,
                    new SqliteParameter { ParameterName = "@MaHD", Value = MaHD },
                    new SqliteParameter { ParameterName = "@MaMon", Value = MaMon },
                    new SqliteParameter { ParameterName = "@SoLuong", Value = SoLuong },
                    new SqliteParameter { ParameterName = "@DonGia", Value = DonGia }
                );
            }
            catch
            {
                throw;
            }
        }
        public int XoaChiTietHoaDon(int MaHD, int MaMon)
        {
            try
            {
                string strSQL = "DELETE FROM ChiTietHoaDon WHERE MaHD = @MaHD AND MaMon = @MaMon";
                Provider p = new Provider();
                return p.ExecuteNonQuery(CommandType.Text, strSQL,
                    new SqliteParameter { ParameterName = "@MaHD", Value = MaHD },
                    new SqliteParameter { ParameterName = "@MaMon", Value = MaMon }
                );
            }
            catch
            {
                throw;
            }
        }

        public int XoaHoaDon(int MaHD)
        {
            try
            {
                string strSQL = "DELETE FROM HoaDon WHERE MaHD = @MaHD";
                Provider p = new Provider();
                return p.ExecuteNonQuery(CommandType.Text, strSQL,
                    new SqliteParameter { ParameterName = "@MaHD", Value = MaHD }
                );
            }
            catch
            {
                throw;
            }
        }
    }
}