using System;
namespace Asp_Dot_Net_Web_Api.Models
{
    public class Author
    {
        [Key]
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}

