using System;
namespace Asp_Dot_Net_Web_Api.Dtos
{
	public class RegisterDto
	{
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
    }
}

