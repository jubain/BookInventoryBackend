using System;
using System.Reflection.Metadata;
using System.Text.Json;
using Asp_Dot_Net_Web_Api.Dtos;

namespace Asp_Dot_Net_Web_Api.Models
{

    public class Book : BaseEntity
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public decimal price { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]

        public int pages { get; set; }
        [Required]
        public DateTime publishedDate { get; set; }

        [Required]
        public int SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }

        public string? image { get; set; }
        [Required]
        public string authors { get; set; }

        public ICollection<BookOrder>? BookOrders { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}

