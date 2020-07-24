using ControlTemplate.Interfaces;
using ControlTemplate.Operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ControlTemplate.Models
{
    public class LoginData : ILoginData
    {
        public LoginData(string path)
        {
            _users = GetUsers(path);
            _path = path;
        }

        private Dictionary<string, string> _users;

        private string _path;

        private Dictionary<string,string> GetUsers(string path)
        {
            Dictionary<string, string> temp = new Dictionary<string, string>();
            if (!File.Exists(path))
                return new Dictionary<string, string>();
            var contents = File.ReadAllLines(path);
            foreach (var content in contents)
            {
                var items = content.Split(",");
                if (items.Count() != 2)
                    throw new Exception($"读取格式错误");
                var decodestr = EncryptionAndDecryption.Decrypt(items[1]);
                temp.Add(items[0],decodestr);
            }
            return temp;
        }

        bool ILoginData.IsExistUser(string name)
        {      
            if (_users.ContainsKey(name))
                return true;
            return false;
        }

        void ILoginData.AddUser(string name, string password)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var encodePassword = EncryptionAndDecryption.Encrypt(password);
            stringBuilder.Append(name);
            stringBuilder.Append(",");
            stringBuilder.Append(encodePassword);
            List<string> t = new List<string>() { stringBuilder.ToString() };
            if(File.Exists(_path))
            {
                if (_users.ContainsKey(name))
                    return;    
            }
            File.AppendAllLines(_path, t);
        }

        bool ILoginData.ValidateUser(string name, string password)
        {
            if (!_users.ContainsKey(name))
                return false;
            if (!string.Equals(_users[name], password))
                return false;
            return true;

        }

        bool ILoginData.DeleteUser(string name)
        {
            if (!File.Exists(_path))
                return false;
            if (!_users.ContainsKey(name))
                return false;
            _users.Remove(name);
            StringBuilder stringBuilder ;
            using(StreamWriter sw=new StreamWriter(_path,false,Encoding.Default))
            {
                foreach(var user in _users)
                {
                    stringBuilder = new StringBuilder();
                    stringBuilder.Append(user.Key);
                    stringBuilder.Append(",");
                    var encodePassword = EncryptionAndDecryption.Encrypt(user.Value);
                    stringBuilder.Append(encodePassword);
                    sw.WriteLine(stringBuilder);
                }
            }
            return true;
        }
    }
}
