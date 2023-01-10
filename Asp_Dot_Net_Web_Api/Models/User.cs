using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

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
        public byte[] passwordHash { get; set; }
        [Required]
        public byte[] passwordSalt { get; set; }

        public bool isCustomer { get; set; } = true;

        public bool isStaff { get; set; } = false;

        public bool isAdmin { get; set; } = false;

        public bool deactivated { get; set; } = false;
        public bool deactivateRequest { get; set; } = false;
        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}

