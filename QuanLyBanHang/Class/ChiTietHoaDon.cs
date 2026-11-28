using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuanLyBanHang.Class
{
    internal class ChiTietHoaDon : INotifyPropertyChanged
    {
        private int _maHD;
        private int _maMon;
        private string? _tenMon;
        private int _soLuong = 1;
        private double _donGia;

        public int MaHD
        {
            get => _maHD;
            set
            {
                if (_maHD != value)
                {
                    _maHD = value;
                    OnPropertyChanged();
                }
            }
        }

        public int MaMon
        {
            get => _maMon;
            set
            {
                if (_maMon != value)
                {
                    _maMon = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? TenMon
        {
            get => _tenMon;
            set
            {
                if (_tenMon != value)
                {
                    _tenMon = value;
                    OnPropertyChanged();
                }
            }
        }

        public int SoLuong
        {
            get => _soLuong;
            set
            {
                if (value > 0 && _soLuong != value)
                {
                    _soLuong = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ThanhTien));
                    OnPropertyChanged(nameof(ThanhTienFormatted));
                }
            }
        }

        public double DonGia
        {
            get => _donGia;
            set
            {
                if (value >= 0 && _donGia != value)
                {
                    _donGia = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ThanhTien));
                    OnPropertyChanged(nameof(ThanhTienFormatted));
                }
            }
        }

        public double ThanhTien => _soLuong * _donGia;
        public string ThanhTienFormatted => $"{ThanhTien:N0} VNĐ";

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}