using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp_Dot_Net_Web_Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp_Dot_Net_Web_Api.Controllers
{
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        //// GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost]
        public object Post([FromBody] PaymentDto payment)
        {
            if (!ModelState.IsValid) return BadRequest("Please give the amount!");
            var stripeApiKey = "sk_test_51LjPI2LQopOFCRZk9ING2Mt8vxqxkU6KIIfOOEMmkZH29TmnueKyOyWScKnBmZa3DqiIM6DIFro2s3zHfxghK6pT00kOMM5Dvu";
            StripeConfiguration.ApiKey = stripeApiKey;
            var paymentIntent = new PaymentIntentCreateOptions
            {
                Amount = payment.amount,
                Currency = "gbp",
                PaymentMethodTypes = new List<string> { "card", }

            };
            var service = new PaymentIntentService();
            service.Create(paymentIntent);
            // Create the PaymentIntent and return the client secret
            var createdPaymentIntent = service.Create(paymentIntent);
            string clientSecret = createdPaymentIntent.ClientSecret;
            return Ok(new { ClientSecret = clientSecret });
        }
    }
}

