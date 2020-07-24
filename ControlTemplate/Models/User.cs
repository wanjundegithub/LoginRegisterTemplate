using System;
using System.Collections.Generic;
using System.Text;

namespace ControlTemplate.Models
{
    public class User
    {
        public User(string account,string password)
        {
            Account = account;
            Password = password;
        }

        public string Account { get; }

        public string Password { get; }
    }
}
