using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp_Dot_Net_Web_Api.Dtos;
using Asp_Dot_Net_Web_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp_Dot_Net_Web_Api.Controllers

{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
       
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
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
            var categories = _db.Category;
            if (categories.Count() == 0) return NotFound("No Sub-Category found!");
            return Ok(categories);

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            var category = _db.Category.Find(id);
            if (category == null) return NotFound("Sorry, Category not found!");
            return Ok(category);
        }

        // POST api/values
        [HttpPost]
        [Authorize]
        public object Post([FromBody] CreateCategoryDto request)
        {
            if (!ModelState.IsValid) return BadRequest("Sorry, Wrong value!");
            if(getCurrentUser().isAdmin || getCurrentUser().isStaff)
            {
                var newCategory = new Category
                {
                    name = request.name
                };
                _db.Category.Add(newCategory);
                _db.SaveChanges();

                return Ok(newCategory);
            }
            return Unauthorized("Sorry, you are not authorized!");
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

