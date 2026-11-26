using QuanLyBanHang;
using System;
using System.Windows;
using System.Windows.Controls;

namespace QuanLyBanHang
{
    public partial class LoginWindow : Window
    {
        private bool _isSyncing = false;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DangNhap dn = new DangNhap();
                User user = new User();
                user.Username = tbUsername.Text;
                user.Password = tbPassword.Password;
                bool kq = dn.KiemTraDangNhap(user);
                if (kq)
                {
                    MessageBox.Show("Dang nhap thanh cong!");

                    Window1 main = new Window1();
                    main.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Sai mat khau hoac user!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    

        // --- 3 hàm xử lý nút con mắt hiện/ẩn mật khẩu ---
        private void btnTogglePass_Changed(object sender, RoutedEventArgs e)
        {
            if (btnTogglePass.IsChecked == true)
            {
                tbPasswordVisible.Text = tbPassword.Password;
                tbPassword.Visibility = Visibility.Collapsed;
                tbPasswordVisible.Visibility = Visibility.Visible;
                tbPasswordVisible.Focus();
                tbPasswordVisible.CaretIndex = tbPasswordVisible.Text.Length;
            }
            else
            {
                tbPassword.Password = tbPasswordVisible.Text;
                tbPasswordVisible.Visibility = Visibility.Collapsed;
                tbPassword.Visibility = Visibility.Visible;
                tbPassword.Focus();
            }
        }

        private void tbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (_isSyncing) return;
            _isSyncing = true;
            tbPasswordVisible.Text = tbPassword.Password;
            _isSyncing = false;
        }

        private void tbPasswordVisible_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isSyncing) return;
            _isSyncing = true;
            tbPassword.Password = tbPasswordVisible.Text;
            _isSyncing = false;
        }
    }
}