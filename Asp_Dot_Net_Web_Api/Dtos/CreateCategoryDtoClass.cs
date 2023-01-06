using System;
namespace Asp_Dot_Net_Web_Api.Dtos
{
	public class CreateCategoryDto
	{
        [Required]
        public string name { get; set; }
    }
}

