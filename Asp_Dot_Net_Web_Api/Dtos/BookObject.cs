using System;
namespace Asp_Dot_Net_Web_Api.Dtos
{
	public class BookObject
	{
        [Required]
        public int BookId { get; set; }
        [Required]
        public int quantity { get; set; }

    }
}

