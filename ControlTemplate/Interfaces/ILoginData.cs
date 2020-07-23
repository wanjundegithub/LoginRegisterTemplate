using System;
using System.Collections.Generic;
using System.Text;

namespace ControlTemplate.Interfaces
{
    public interface ILoginData
    {
        void AddUser(string name,string password);

        bool IsExistUser(string name);

        bool ValidateUser(string name, string password);
    }
}
