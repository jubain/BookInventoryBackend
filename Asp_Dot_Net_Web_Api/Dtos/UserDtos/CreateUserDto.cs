using System;
using System.ComponentModel;

namespace Asp_Dot_Net_Web_Api.Dtos.UserDtos
{
    public class CreateUserDto
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        public string? middleName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [DefaultValue(true)]
        public bool? isCustomer { get; set; } = true;
        [DefaultValue(false)]
        public bool? isAdmin { get; set; } = false;
        [DefaultValue(false)]
        public bool? isStaff { get; set; } = false;
    }
}

