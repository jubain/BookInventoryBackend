using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp_Dot_Net_Web_Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp_Dot_Net_Web_Api.Controllers
{
    [Route("api/[controller]")]
    public class BookOrderController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookOrderController(ApplicationDbContext db)
        {
            _db = db;
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        public User getCurrentUser()
        {
            var userEmail = User.Identity?.Name;
            var user = _db.User.Where(u => u.email == userEmail).First();
            return user;
        }


        // GET: api/values
        [HttpGet]
        [Authorize]
        public object Get()
        {
            if (getCurrentUser() != null)
            {
                var bookOrders = _db.BookOrders;
                if (bookOrders.Count() <= 0)
                {
                    return NotFound("Sorry, No order found!");
                }
                return Ok(bookOrders);
            }
            return Unauthorized("Sorry, You are not Authorized!");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public void Get(int id)
        {

        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] BookOrderDto value)
        {

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

