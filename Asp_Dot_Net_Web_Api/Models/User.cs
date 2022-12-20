using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Asp_Dot_Net_Web_Api.Models
{
    [Index(nameof(email), IsUnique = true)]
    public class User : BaseEntity
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }

        public string? middleName { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }

        public bool isCustomer { get; set; } = true;

        public bool isStaff { get; set; } = false;

        public bool isAdmin { get; set; } = false;
        
        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}

