using System;
using System.Collections.Generic;
using System.Linq;

namespace BookDiary.BLL.DTO
{
    public class NotificationDTO
    {
        public int Id { get; set; }
        public DateTimeOffset Day { get; set; }
        public string Message { get; set; }
        public bool IsSeen { get; set; }
        public int BookId { get; set; }
    }
}
