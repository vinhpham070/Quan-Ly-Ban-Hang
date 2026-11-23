using System.Data;

namespace QuanLyBanHang
{
    internal class DatabaseInitializer
    {
        Provider p = new Provider();

        public void Initialize()
        {
            string sql = @"
                -- 1. Bảng Tài khoản
                CREATE TABLE IF NOT EXISTS User (
                    Username TEXT PRIMARY KEY,
                    Password TEXT NOT NULL
                );

               
            ";

            p.ExecuteNonQuery(CommandType.Text, sql);

            
        }
    }
}