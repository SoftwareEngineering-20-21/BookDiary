using BookDiary.DAL.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookDiary.DAL.Entities
{
    public class User : AbstractEntity
    {
        [Required]
        [MaxLength(30)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(255)]
        [Index(IsUnique = true)]
        public string Nickname { get; set; }

        [MaxLength(225)]
        public string Fullname { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(255)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
