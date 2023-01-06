using System;
using System.Text.Json.Serialization;

namespace Asp_Dot_Net_Web_Api.Models
{
    public class Address : BaseEntity
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string address1 { get; set; }

        public string? address2 { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string county { get; set; }
        [Required]
        public string postCode { get; set; }
        [Required]
        public long phone
        {
            get; set;
        }
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}

