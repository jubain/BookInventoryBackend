using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp_Dot_Net_Web_Api.Dtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp_Dot_Net_Web_Api.Controllers
{
    [Route("api/[controller]")]
    public class ReviewController : BaseClass
    {
        public ReviewController(ApplicationDbContext db) : base(db)
        {
        }

        // GET: api/values
        [HttpGet]
        public object Get()
        {
            var reviews = _db.Review;
            if (reviews.Count() == 0)
            {
                return NotFound("No Reviews!");
            }
            return Ok(reviews);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            var review = _db.Review.Find(id);
            if (review == null)
            {
                return NotFound("Sorry, Review Not Found!");
            }
            return Ok(review);
        }

        // POST api/values
        [HttpPost]
        public object Post([FromBody]CreateReviewDto review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Sorry, Enter correct values!");
            }
            if(_db.User.Find(review.userId)==null || _db.Book.Find(review.bookId)==null)
            {
                return NotFound("Sorry, User or Book not found!");
            }
            var newReview = new Review
            {
                BookId = review.bookId,
                UserId = review.userId,
                review = review.review
            };
            _db.Review.Add(newReview);
            _db.SaveChangesAsync();

            return Ok(newReview);
        }

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public object Delete(int id)
        {
            var review=_db.Review.Find(id);
            if (review == null)
            {
                return NotFound("Sorry, Review Not Found!");
            }
            _db.Review.Remove(review);
            _db.SaveChangesAsync();
            return Ok("Review Removed");
        }
    }
}

