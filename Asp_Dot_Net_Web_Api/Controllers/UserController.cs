
using Asp_Dot_Net_Web_Api.Dtos.UserDtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp_Dot_Net_Web_Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: api/values
        [HttpGet]
        public object Get()
        {
            var users = _db.User;
            return Ok(users);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            var userExist = _db.User.Find(id);
            if (userExist == null)
            {
                return NotFound("Sorry, User not found!");
            }
            return Ok(userExist);
        }

        // POST api/values
        [HttpPost]
        public object Post([FromBody] CreateUserDto obj)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Sorry, Enter correct values!");
            }
            if (_db.User.Any(u => u.email == obj.email))
            {
                return BadRequest("Sorry, Email already in use!");
            }
            //if (userExist is null)
            //{
            var newUser = new User();
            newUser.email = obj.email;
            newUser.password = obj.email;
            newUser.firstName = obj.email;
            newUser.middleName = obj.email;
            newUser.lastName = obj.email;
            _db.User.Add(newUser);
            _db.SaveChangesAsync();

            return Ok(obj);
        }

        // PUT api/values/5
        [HttpPatch("{id}")]
        public object Patch(int id, [FromBody] JsonPatchDocument<User> obj)
        {
            var userExist = _db.User.Find(id);
            if (userExist != null)
            {
                obj.ApplyTo(userExist);
                _db.SaveChangesAsync();
                return Ok(userExist);
            }
            return NotFound("Sorry, User not found!");
        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public object Delete(int id)
        {
            var userExist = _db.User.Find(id);
            if (userExist == null)
            {
                return NotFound("Sorry, User not found!");
            }
            _db.User.Remove(userExist);
            _db.SaveChanges();
            return Ok("User removed!");
        }
    }
}

