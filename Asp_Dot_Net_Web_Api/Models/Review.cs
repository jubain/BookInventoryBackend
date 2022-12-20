using System;
namespace Asp_Dot_Net_Web_Api.Models
{
    public class Review : BaseEntity
    {
        [Key]
        public int id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        [Required]
        public string review { get; set; }
    }
}

