using System;
namespace Asp_Dot_Net_Web_Api.Dtos
{
    public class UpdateAddressDto
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public string postCode { get; set; }
        public long phone { get; set; }
    }
}

