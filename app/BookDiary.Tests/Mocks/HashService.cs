using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookDiary.BLL.Interfaces;

namespace BookDiary.Tests.Mocks
{
    public class HashService : IHashService
    {
        public string GetHash(string password)
        {
            return password;
        }
    }
}
