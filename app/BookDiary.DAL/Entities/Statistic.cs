using BookDiary.DAL.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookDiary.DAL.Entities
{
    public class Statistic : AbstractEntity
    {
        [Required]
        public DateTimeOffset Day { get; set; }

        [Required]
        public int OldPages { get; set; }

        [Required]
        public int NewPages { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}