using System;
namespace Asp_Dot_Net_Web_Api.Models
{
    public class BaseEntity
    {
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}

