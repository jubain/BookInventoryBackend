using System;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace Asp_Dot_Net_Web_Api.Models
{
    [Index(nameof(name), IsUnique = true)]
    public class SubCategory : BaseEntity
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public ICollection<Book> Books { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

