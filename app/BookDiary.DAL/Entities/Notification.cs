using BookDiary.DAL.Abstractions;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookDiary.DAL.Entities
{
    public class Notification : AbstractEntity
    {
        [Required]
        public DateTimeOffset Day { get; set; }

        [MaxLength(225)]
        public string Message { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsSeen { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}
