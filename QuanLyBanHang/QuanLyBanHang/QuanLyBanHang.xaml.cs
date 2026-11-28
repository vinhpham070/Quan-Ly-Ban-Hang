using QuanLyBanHang;
using QuanLyBanHang.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace QuanLyBanHang
{
    public partial class Window1 : Window
    {
        DispatcherTimer _clockTimer;
        int _selectedMaBan = -1;
        string _selectedTenBan = null;

        int _selectedMaMon = -1;
        string _selectedTenMon = null;
        double _selectedDonGia = -1;
        List<ChiTietHoaDon> _lstChiTietHoaDon = new List<ChiTietHoaDon>();
        public Window1()
        {
            InitializeComponent();
        }

        private void DgBill_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _lstChiTietHoaDon.Clear();
            foreach (var item in DgBill.SelectedItems)
            {
                if (item is ChiTietHoaDon chiTiet)
                {
                    _lstChiTietHoaDon.Add(chiTiet);
                }
            }
        }

        private void HienThiMenu()
        {
            QuanLyController quanLyController = new QuanLyController();
            LstMenu.ItemsSource = quanLyController.LayTatCaMonAn().DefaultView;
        }

        private void HienThiBan()
        {
            QuanLyController quanLyController = new QuanLyController();
            LstTables.ItemsSource = quanLyController.LayTatCaBan().DefaultView;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                HienThiBan();
                HienThiMenu();
                StartClock();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HienThiTieuDeHoaDonBan(int _selectedMaBan, string _selectedTenBan)
        {
            try
            {
                TxtSelectedTableHeader.Text = $"HÓA ĐƠN - {_selectedTenBan.ToUpper()}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TxtSearchTable_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(TxtSearchTable.Text))
                {
                    QuanLyController quanLyController = new QuanLyController();
                    LstTables.ItemsSource = quanLyController.TimKiemBan(TxtSearchTable.Text).DefaultView;
                }
                else
                {
                    HienThiBan();
                }
            }
            catch
            {
                throw;
            }
        }

        private void TxtSearchMenu_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(TxtSearchMenu.Text))
                {
                    QuanLyController quanLyController = new QuanLyController();
                    LstMenu.ItemsSource = quanLyController.TimKiemMonAn(TxtSearchMenu.Text).DefaultView;
                }
                else
                {
                    HienThiMenu();
                }
            }
            catch
            {
                throw;
            }
        }

        private void LstTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                QuanLyController quanLyController = new QuanLyController();
                if (LstTables.SelectedItem is DataRowView row)
                {
                    // 1. Cập nhật giá trị vào biến toàn cục mỗi khi click bàn mới
                    _selectedMaBan = Convert.ToInt32(row["MaBan"]);
                    _selectedTenBan = row["TenBan"].ToString();

                    // 2. Đổi chữ trên tiêu đề hóa đơn
                    HienThiTieuDeHoaDonBan(_selectedMaBan, _selectedTenBan);

                    // 3. Load hóa đơn + tính tổng tiền (hoặc reset về 0 nếu bàn trống)
                    int MaHD = quanLyController.TimKiemHoaDon(_selectedMaBan);
                    if (MaHD != -1)
                    {
                        LoadHoaDonLenGrid(MaHD);
                    }
                    else
                    {
                        DgBill.ItemsSource = null;
                        TxtTotalMoney.Text = "0 VNĐ";
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private void LstMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (LstMenu.SelectedItem is DataRowView row)
                {
                    _selectedMaMon = Convert.ToInt32(row["MaMon"]);
                    _selectedTenMon = row["TenMon"].ToString();
                    _selectedDonGia = Convert.ToDouble(row["DonGia"]);
                }
            }
            catch
            {
                throw;
            }
        }

        private void StartClock()
        {
            UpdateDateTimeDisplay();

            _clockTimer = new DispatcherTimer();
            _clockTimer.Interval = TimeSpan.FromSeconds(1);
            _clockTimer.Tick += (s, args) =>
            {
                UpdateDateTimeDisplay();
            };
            _clockTimer.Start();
        }

        private void UpdateDateTimeDisplay()
        {
            DateTime now = DateTime.Now;
            TxtCurrentDate.Text = now.ToString("dd/MM/yyyy");
            TxtCurrentTime.Text = now.ToString("HH:mm:ss");
        }

        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedMaBan == -1)
                {
                    MessageBox.Show("Vui lòng chọn bàn trước khi thêm món!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_selectedMaMon == -1)
                {
                    MessageBox.Show("Vui lòng chọn món trong thực đơn!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                QuanLyController quanLyController = new QuanLyController();
                int MaHD = quanLyController.TimKiemHoaDon(_selectedMaBan);

                if (MaHD == -1)
                {
                    MaHD = quanLyController.TaoHoaDon(_selectedMaBan);
                }

                ChiTietHoaDon? chiTiet = quanLyController.TimKiemChiTietHoaDon(MaHD, _selectedMaMon);
                if (chiTiet != null)
                {
                    quanLyController.TangSoLuong(MaHD, _selectedMaMon, 1);
                }
                else
                {
                    quanLyController.ThemChiTietHoaDon(MaHD, _selectedMaMon, 1, _selectedDonGia);
                }

                LoadHoaDonLenGrid(MaHD);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm món: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadHoaDonLenGrid(int maHD)
        {
            QuanLyController quanLyController = new QuanLyController();
            List<ChiTietHoaDon> dsChiTiet = quanLyController.LayChiTietHoaDon(maHD);

            DgBill.ItemsSource = dsChiTiet;

            double tongTien = dsChiTiet.Sum(item => item.ThanhTien);
            TxtTotalMoney.Text = $"{tongTien:N0} VNĐ";
        }

        private void BtnDecreaseQty_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((sender as FrameworkElement)?.DataContext is ChiTietHoaDon item && item.SoLuong > 1)
                {
                    QuanLyController quanLyController = new QuanLyController();
                    quanLyController.TangSoLuong(item.MaHD, item.MaMon, -1);
                    LoadHoaDonLenGrid(item.MaHD);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi giảm số lượng: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnIncreaseQty_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((sender as FrameworkElement)?.DataContext is ChiTietHoaDon item)
                {
                    QuanLyController quanLyController = new QuanLyController();
                    quanLyController.TangSoLuong(item.MaHD, item.MaMon, 1);
                    LoadHoaDonLenGrid(item.MaHD);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tăng số lượng: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TxtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Nhấn Enter để bỏ focus khỏi ô nhập, tự động kích hoạt TxtQty_LostFocus bên dưới
                Keyboard.ClearFocus();
            }
        }

        private void TxtQty_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is TextBox tb && tb.DataContext is ChiTietHoaDon item)
                {
                    // Kiểm tra giá trị nhập vào phải là số nguyên dương (> 0)
                    if (int.TryParse(tb.Text, out int soLuongMoi) && soLuongMoi > 0)
                    {
                        int chenhLech = soLuongMoi - item.SoLuong;
                        if (chenhLech != 0)
                        {
                            QuanLyController quanLyController = new QuanLyController();
                            quanLyController.TangSoLuong(item.MaHD, item.MaMon, chenhLech);
                            LoadHoaDonLenGrid(item.MaHD);
                        }
                    }
                    else
                    {
                        // Nếu gõ chữ hoặc số <= 0 thì tự động khôi phục lại số lượng cũ
                        tb.Text = item.SoLuong.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật số lượng: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_lstChiTietHoaDon.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất 1 món trên hóa đơn để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var confirm = MessageBox.Show($"Bạn có chắc muốn xóa {_lstChiTietHoaDon.Count} món đã chọn khỏi hóa đơn?",
                                              "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirm != MessageBoxResult.Yes) return;

                QuanLyController quanLyController = new QuanLyController();
                int MaHD = _lstChiTietHoaDon[0].MaHD;

                // Xóa từng món đã chọn trong database
                foreach (ChiTietHoaDon item in _lstChiTietHoaDon.ToList())
                {
                    quanLyController.XoaChiTietHoaDon(item.MaHD, item.MaMon);
                }

                _lstChiTietHoaDon.Clear();
                LoadHoaDonLenGrid(MaHD);
                if (DgBill.Items.Count == 0)
                    quanLyController.XoaHoaDon(MaHD);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa món: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}