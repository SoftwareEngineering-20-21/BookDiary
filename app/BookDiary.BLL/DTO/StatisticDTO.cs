using System;
using System.Collections.Generic;
using System.Linq;

namespace BookDiary.BLL.DTO
{
    public class StatisticDTO
    {
        public int Id { get; set; }
        public DateTimeOffset Day { get; set; }
        public int OldPages { get; set; }
        public int NewPages { get; set; }
        public int BookId { get; set; }
    }
}
