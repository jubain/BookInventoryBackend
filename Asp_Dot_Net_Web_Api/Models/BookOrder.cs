using System;
namespace Asp_Dot_Net_Web_Api.Models
{
    public class BookOrder : BaseEntity
    {
        [Key]
        public int id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int quantity { get; set; }
        public decimal price { get; set; }
    }
}

