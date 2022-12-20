using System;
using Microsoft.Extensions.Hosting;

namespace Asp_Dot_Net_Web_Api.Models
{
    public class Category : BaseEntity
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
    }
}

