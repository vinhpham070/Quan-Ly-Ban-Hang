using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyBanHang
{
    public class User
    {
        string? _username;
        string? _password;

        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }
    }
}
