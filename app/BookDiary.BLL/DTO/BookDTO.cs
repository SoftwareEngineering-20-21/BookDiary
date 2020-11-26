using System;
using System.Collections.Generic;
using System.Linq;

namespace BookDiary.BLL.DTO
{
    public enum BookStatus
    {
        InProgress,
        Completed,
        Planned
    }

    public class BookDTO
    {
        public int Id { get; set; }
        public BookStatus Status { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int TotalPages { get; set; }
        public int ReadPages { get; set; }
        public string Review { get; set; }
        public int Mark { get; set; }
        public int UserId { get; set; }
    }
}
