using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyBanHang.Class
{
    internal class HoaDon
    {
        int _maHD;
        int _maBan;
        string? _ngayLap;
        double _tongTien;
        int _trangThai;

        public int MaHD { get => _maHD; set => _maHD = value; }
        public int MaBan { get => _maBan; set => _maBan = value; }
        public string? NgayLap { get => _ngayLap; set => _ngayLap = value; }
        public double TongTien
        {
            get => _tongTien;
            set
            {
                if (value >= 0) _tongTien = value; // Chặn tổng tiền âm
            }
        }
        public int TrangThai
        {
            get => _trangThai;
            set
            {
                if (value == 0 || value == 1) _trangThai = value; // Chỉ nhận 0 hoặc 1
            }
        }
    }
}
