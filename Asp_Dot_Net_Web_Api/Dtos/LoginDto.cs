using System;
namespace Asp_Dot_Net_Web_Api.Dtos
{
    public class LoginDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}

