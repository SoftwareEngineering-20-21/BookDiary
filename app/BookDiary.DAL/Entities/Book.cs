using BookDiary.DAL.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookDiary.DAL.CustomValidators;
using System.ComponentModel;

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

        [Required]
        [DefaultValue(0)]
        public int ReadPages { get; set; }

        [Column(TypeName = "VARCHAR")]
        [MaxLength(1000)]
        public string Review { get; set; }

        [MinValue(1)]
        [MaxValue(5)]
        public int Mark { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
