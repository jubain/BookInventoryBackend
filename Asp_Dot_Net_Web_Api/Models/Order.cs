using System;
namespace Asp_Dot_Net_Web_Api.Models
{
    public class Order : BaseEntity
    {
        [Key]
        public int id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<BookOrder> BookOrders { get; set; }
    }
}

