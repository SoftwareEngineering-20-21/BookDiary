using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookDiary.BLL.Interfaces
{
    interface IHashService
    {
        string GetHash(string password);
    }
}
