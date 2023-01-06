using System;
namespace Asp_Dot_Net_Web_Api.Dtos
{
    public class CreateAddressDto
    {
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
        public long phone { get; set; }
    }
}

