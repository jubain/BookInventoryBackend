using System;
namespace Asp_Dot_Net_Web_Api.Dtos
{
	public class PaymentFormData
	{
		public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Token { get; set; }
    }
}

