using System.Configuration;
using System.Data;
using System.Windows;

namespace QuanLyBanHang
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                DatabaseInitializer dbInit = new DatabaseInitializer();
                dbInit.Initialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Database Initialization Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }

        
    }

}
