using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyBanHang.Class
{
    internal class ChiTietHoaDon
    {
        int _maHD;
        int _maMon;
        string? _tenMon;
        int _soLuong = 1;
        double _donGia;

        public int MaHD { get => _maHD; set => _maHD = value; }
        public int MaMon { get => _maMon; set => _maMon = value; }
        public string? TenMon { get => _tenMon; set => _tenMon = value; }
        public int SoLuong
        {
            get => _soLuong;
            set
            {
                if (value > 0) _soLuong = value; // Bắt buộc số lượng >= 1
            }
        }
        public double DonGia
        {
            get => _donGia;
            set
            {
                if (value >= 0) _donGia = value; // Chặn đơn giá âm
            }
        }

        public double ThanhTien => _soLuong * _donGia;
        public string ThanhTienFormatted => $"{ThanhTien:N0} VNĐ";
    }
}
