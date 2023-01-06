using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp_Dot_Net_Web_Api.Dtos;
using Asp_Dot_Net_Web_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp_Dot_Net_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AddressController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AddressController(ApplicationDbContext db)
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
        public object Get()
        {
            var addresses = _db.Address.Where(a => a.UserId == getCurrentUser().id).ToList();
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string json = JsonConvert.SerializeObject(addresses, settings);
            return Ok(json);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            var address = _db.Address.Find(id);
            if (address?.UserId == getCurrentUser().id)
            {
                if (address == null) return NotFound("Sorry, user not found!");
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                string json = JsonConvert.SerializeObject(address, settings);
                return Ok(json);
            }
            return Unauthorized("Sorry, You are not authorized!");
        }

        // POST api/values
        [HttpPost]
        public object Post([FromBody] CreateAddressDto formBody)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var address = new Address
            {
                address1 = formBody.address1,
                address2 = formBody.address2,
                city = formBody.city,
                county = formBody.county,
                postCode = formBody.postCode,
                phone = formBody.phone,
                UserId = getCurrentUser().id,
                User = getCurrentUser()
            };
            _db.Address.Add(address);
            _db.SaveChanges();
            return Ok(address);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public object Put(int id, [FromBody] UpdateAddressDto formBody)
        {
            if (!ModelState.IsValid) return BadRequest("Please fill all the required field");
            var address = _db.Address.Find(id);
            if (address == null) return NotFound("Sorry, address not found!");
            if (getCurrentUser().id == address.UserId)
            {
                address.address1 = formBody.address1;
                address.address2 = formBody.address2;
                address.city = formBody.city;
                address.county = formBody.county;
                address.postCode = formBody.postCode;
                address.phone = formBody.phone;
                address.UpdatedAt = DateTime.Now;

                _db.Address.Update(address);
                _db.SaveChanges();
                return Ok("Address Updated");
            }
            return Unauthorized("Sorry, You are not authorized!");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public object Delete(int id)
        {
            if (!ModelState.IsValid) return BadRequest("Please enter id!");
            var address = _db.Address.Find(id);
            if (address == null) return NotFound("Sorry, address not found!");
            if (getCurrentUser().id == address.UserId)
            {
                _db.Address.Remove(address);
                _db.SaveChanges();
                return Ok("Address removed!");
            }
            return Unauthorized("Sorry, You are not authorized!");
        }
    }
}

