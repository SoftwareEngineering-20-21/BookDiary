using System;
using System.Text;
using BookDiary.BLL.Interfaces;
using System.Security.Cryptography;

namespace BookDiary.BLL.Services
{
    public class HashService : IHashService
    {
        public string GetHash(string password)
        {
            SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider();
            sh.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
            byte[] re = sh.Hash;
            StringBuilder sb = new StringBuilder();
            foreach (byte b in re)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
