using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ControlTemplate.Operations
{
    public static class EncryptionAndDecryption
    {
        private static string encryptKey = @"rtFd";

        public static string Encrypt(string str)
        {
            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();
            byte[] key = Encoding.Unicode.GetBytes(encryptKey);
            byte[] data = Encoding.Unicode.GetBytes(str);
            MemoryStream MStream = new MemoryStream();
            CryptoStream CStream = new CryptoStream(MStream, descsp.CreateEncryptor(key, key), CryptoStreamMode.Write);
            CStream.Write(data, 0, data.Length);
            CStream.FlushFinalBlock();
            return Convert.ToBase64String(MStream.ToArray());
        }

        public static string Decrypt(string str)
        {
            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();
            byte[] key = Encoding.Unicode.GetBytes(encryptKey);
            byte[] data = Convert.FromBase64String(str);
            MemoryStream MStream = new MemoryStream();
            CryptoStream CStram = new CryptoStream(MStream, descsp.CreateDecryptor(key, key), CryptoStreamMode.Write);
            CStram.Write(data, 0, data.Length);
            CStram.FlushFinalBlock();
            return Encoding.Unicode.GetString(MStream.ToArray());
        }
    }
}
