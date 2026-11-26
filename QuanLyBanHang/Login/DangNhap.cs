using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Data.Sqlite;
using QuanLyBanHang;

namespace QuanLyBanHang
{
    internal class DangNhap
    {
        public bool KiemTraDangNhap(User user)
        {
            try
            {
                string strSQL = "SELECT 1 FROM USER WHERE Username = @Username AND Password = @Password";
                Provider p = new Provider();
                DataTable dt = p.Select(CommandType.Text, strSQL,
                    new SqliteParameter { ParameterName = "@Username", Value = user.Username },
                    new SqliteParameter { ParameterName = "@Password", Value = user.Password });
                return dt.Rows.Count > 0;
            } catch
            {
                throw;
            }
        }
    }
}
