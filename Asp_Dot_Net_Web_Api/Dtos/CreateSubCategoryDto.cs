using System;
namespace Asp_Dot_Net_Web_Api.Dtos
{
	public class CreateSubCategoryDto
	{
        [Required]
        public string name { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}

