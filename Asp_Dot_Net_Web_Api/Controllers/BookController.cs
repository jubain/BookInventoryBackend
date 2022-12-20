
using Asp_Dot_Net_Web_Api.Dtos.BookDto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp_Dot_Net_Web_Api.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
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
            
            var newBook = new Book
            {
                name = book.name,
                price = book.price,
                quantity = book.quantity,
                pages = book.pages,
                publishedDate = book.publishedDate,
                SubCategoryId = book.SubCategoryId,
                authors = book.authors
            };
            _db.Book.Add(newBook);
            _db.SaveChangesAsync();
            return Ok(book);
        }

        // PUT api/values/5
        [HttpPatch("{id}")]
        public object Put(int id, [FromBody] JsonPatchDocument book)
        {
            var bookExist = _db.Book.Find(id);
            if (bookExist != null)
            {
                book.ApplyTo(bookExist);
                _db.SaveChangesAsync();
                return Ok(bookExist);
            }
            return NotFound("Sorry, Book not found!");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public object Delete(int id)
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
    }
}

