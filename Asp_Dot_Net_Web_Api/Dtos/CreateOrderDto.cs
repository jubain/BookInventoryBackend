using System;
namespace Asp_Dot_Net_Web_Api.Dtos
{
    public class CreateOrderDto
    {
        [Required]
        public int userId { get; set; }
        [Required]
        public BookObject[] Books { get; set; }
    }
}

