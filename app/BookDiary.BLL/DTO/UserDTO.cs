using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookDiary.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
