using System;
using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;

namespace QuanLyBanHang
{
    internal class Provider
    {
        public static string ConnectionString = $@"Data Source={Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\QuanLy.db"))};";
        SqliteConnection? Connection { get; set; }

        public void Connect()
        {
            try
            {
                if (Connection == null)
                    Connection = new SqliteConnection(ConnectionString);
                if (Connection.State != ConnectionState.Closed)
                    Connection.Close();
                Connection.Open();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DisConnect()
        {
            if (Connection != null && Connection.State == ConnectionState.Open)
                Connection.Close();
        }

        public DataTable Select(CommandType commandType, string strSQL, params SqliteParameter[] parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                Connect();
                using SqliteCommand cmd = Connection!.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;

                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                using SqliteDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DisConnect();
            }
            return dt;
        }

        public int ExecuteNonQuery(CommandType commandType, string strSQL, params SqliteParameter[] parameters)
        {
            int nRow = 0;
            try
            {
                Connect();
                using SqliteCommand cmd = Connection!.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;

                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                nRow = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DisConnect();
            }
            return nRow;
        }
    }
}