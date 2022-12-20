using System;
namespace Asp_Dot_Net_Web_Api.Dtos
{
    public class CreateReviewDto
    {
        [Required]
        public int userId { get; set; }
        [Required]
        public int bookId { get; set; }
        [Required]
        public string review { get; set; }
    }
}

