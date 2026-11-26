using QuanLyBanHang;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyBanHang
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void DgBill_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
                if (TxtSearchTable.Text != null) 
                {
                    QuanLyController quanLyController = new QuanLyController();
                    LstTables.ItemsSource = quanLyController.TimKiemBan(TxtSearchTable.Text).DefaultView;
                    
                } else
                {
                    HienThiBan();
                }

            } catch
            {
                throw;
            }
        }

        private void TxtSearchMenu_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (TxtSearchMenu.Text != null)
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

        
    }
}
