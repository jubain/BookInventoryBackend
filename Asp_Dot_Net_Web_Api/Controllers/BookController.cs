
using Asp_Dot_Net_Web_Api.Dtos;
using Asp_Dot_Net_Web_Api.Dtos.BookDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp_Dot_Net_Web_Api.Controllers
{
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
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

        [HttpGet]
        public object Get()
        {
            var books = _db.Book;
            if (books.Count() == 0)
            {
                return NotFound("There is no books!");
            }
            return Ok(books);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            var book = _db.Book.Find(id);
            if (book == null)
            {
                return NotFound("There is no books!");
            }
            return Ok(book);
        }

        // POST api/values
        [HttpPost]
        [Authorize]
        public object Post([FromBody] CreateBookDto book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_db.SubCategory.Find(book.SubCategoryId) == null)
            {
                return NotFound("Sorry, Sub Category not found");
            }
            if (getCurrentUser().isAdmin || getCurrentUser().isStaff)
            {
                var newBook = new Book
                {
                    name = book.name,
                    price = book.price,
                    description = book.description,
                    quantity = book.quantity,
                    pages = book.pages,
                    publishedDate = book.publishedDate,
                    SubCategoryId = book.SubCategoryId,
                    authors = book.authors,
                    image=book.image
                };
                _db.Book.Add(newBook);
                _db.SaveChanges();
                return Ok(book);
            }
            return Unauthorized("Sorry, You are not authorized!");
        }

        //// PUT api/values/5
        //[HttpPatch("{id}")]
        //[Authorize]
        //public object Patch(int id, [FromBody] JsonPatchDocument book)
        //{
        //    if (getCurrentUser().isAdmin || getCurrentUser().isStaff)
        //    {
        //        var bookExist = _db.Book.Find(id);
        //        if (bookExist != null)
        //        {
        //            book.ApplyTo(bookExist);
        //            _db.SaveChanges();
        //            return Ok(bookExist);
        //        }
        //        return NotFound("Sorry, Book not found!");
        //    }
        //    return Unauthorized("Sorry, You are not authorized!");
        //}

        [HttpPut("{id}")]
        [Authorize]
        public object Put(int id, [FromBody] UpdateBookDto book)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (getCurrentUser().isAdmin || getCurrentUser().isStaff)
            {
                var bookExist = _db.Book.Find(id);
                if (bookExist != null)
                {
                    bookExist.name = book.name;
                    bookExist.description = book.description;
                    bookExist.price = book.price;
                    bookExist.quantity = book.quantity;
                    bookExist.pages = book.pages;
                    bookExist.SubCategoryId = book.SubCategoryId;
                    bookExist.authors = book.authors;
                    bookExist.UpdatedAt = DateTime.Now;
                    _db.Book.Update(bookExist);
                    _db.SaveChanges();
                    return Ok(bookExist);
                }
                return NotFound("Sorry, Book not found!");
            }
            return Unauthorized("Sorry, You are not authorized!");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize]
        public object Delete(int id)
        {
            if (getCurrentUser().isAdmin || getCurrentUser().isStaff)
            {
                var book = _db.Book.Find(id);
                if (book == null)
                {
                    return NotFound("Sorry, Book not found!");
                }
                _db.Book.Remove(book);
                _db.SaveChanges();
                return Ok("Book removed!");
            }
            return Unauthorized("Sorry, You are not authorized!");
        }
    }
}

