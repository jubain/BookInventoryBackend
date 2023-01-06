using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp_Dot_Net_Web_Api.Dtos;
using Asp_Dot_Net_Web_Api.Models;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp_Dot_Net_Web_Api.Controllers
{

    [Route("api/[controller]")]
    public class SubCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SubCategoryController(ApplicationDbContext db)
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
            var subCats = _db.SubCategory;
            if (subCats.Count() == 0) return NotFound("No Sub-Category found!");
            return Ok(subCats);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            var subCat = _db.SubCategory.Find(id);
            if (subCat == null) return NotFound("Sorry, Sub-Category not found!");
            return Ok(subCat);
        }

        // POST api/values
        [HttpPost]
        [Authorize]
        public object Post([FromBody] CreateSubCategoryDto request)
        {
            if (!ModelState.IsValid) return BadRequest("Sorry, Wrong value!");
            if (getCurrentUser().isAdmin || getCurrentUser().isStaff)
            {
                if (_db.Category.Find(request.CategoryId) == null) return NotFound("Soory, category not found!");
                if (_db.SubCategory.Any(u => u.name == request.name))
                {
                    return BadRequest("Sorry, Sub Category already in use!");
                }

                var newSubCategory = new SubCategory
                {
                    name = request.name,
                    CategoryId = request.CategoryId
                };

                _db.SubCategory.Add(newSubCategory);
                _db.SaveChanges();

                return Ok(newSubCategory);
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

