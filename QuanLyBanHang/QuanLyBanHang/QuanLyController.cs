using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.Sqlite;
using QuanLyBanHang;

namespace QuanLyBanHang
{
    class QuanLyController
    {
        public DataTable LayTatCaBan()
        {
            try
            {
                string strSQL = "SELECT * FROM BAN";
                Provider p = new Provider();
                DataTable dt = p.Select(CommandType.Text, strSQL);
                return dt;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public DataTable TimKiemBan(string TenBan)
        {
            try
            {
                string strSQL = "SELECT * FROM Ban WHERE TenBan LIKE CONCAT('%', @TenBan, '%')";
                Provider p = new Provider();
                DataTable dt = p.Select(CommandType.Text, strSQL,
                    new SqliteParameter { ParameterName = "@TenBan", Value = TenBan }
                    );
                return dt;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable LayTatCaMonAn()
        {
            try
            {
                string strSQL = "SELECT * FROM MonAn";
                Provider p = new Provider();
                DataTable dt = p.Select(CommandType.Text, strSQL);
                return dt;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public DataTable TimKiemMonAn(string TenMon)
        {
            try
            {
                string strSQL = "SELECT * FROM MonAn WHERE TenMon LIKE CONCAT('%', @TenMon, '%')";
                Provider p = new Provider();
                DataTable dt = p.Select(CommandType.Text, strSQL,
                    new SqliteParameter { ParameterName = "@TenMon", Value = TenMon }
                    );
                return dt;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
