using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookDiary.BLL.Interfaces
{
    public interface IHashService
    {
        string GetHash(string password);
    }
}
