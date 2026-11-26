using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyBanHang.Class
{
    internal class MonAn
    {
        int _maMon;
        string? _tenMon;
        double _donGia;

        public int MaMon { get => _maMon; set => _maMon = value; }
        public string? TenMon { get => _tenMon; set => _tenMon = value; }
        public double DonGia
        {
            get => _donGia;
            set
            {
                if (value >= 0) _donGia = value; // Chặn đơn giá âm
            }
        }

        public string DonGiaFormatted => $"{_donGia:N0} VNĐ";
    }
}
