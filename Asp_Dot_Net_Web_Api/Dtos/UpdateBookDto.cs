﻿using System;
namespace Asp_Dot_Net_Web_Api.Dtos
{
	public class UpdateBookDto
	{
        [Required]
        public string name { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public decimal price { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]

        public int pages { get; set; }

        [Required]
        public int SubCategoryId { get; set; }

        [Required]
        public string authors { get; set; }
    }
}

