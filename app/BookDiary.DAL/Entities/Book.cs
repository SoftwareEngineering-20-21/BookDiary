using BookDiary.DAL.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookDiary.DAL.Entities
{
    public enum BookStatus
    {
        InProgress,
        Completed,
        Planned
    }
    public class Book : AbstractEntity
    {
        

        public BookStatus Status { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [MaxLength(225)]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [MaxLength(225)]
        public string Author { get; set; }

        [Required]
        public int TotalPages { get; set; }

        public int UserId { get; set; }
    }
}
