using QuanLyBanHang.Class;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyBanHang
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
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
    }
}