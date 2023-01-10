
using Asp_Dot_Net_Web_Api.Dtos.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Asp_Dot_Net_Web_Api.Authorization;
using Asp_Dot_Net_Web_Api.Models;
using Asp_Dot_Net_Web_Api.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp_Dot_Net_Web_Api.Controllers
{
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
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
            var loggedInUser = getCurrentUser();
            if (loggedInUser.isAdmin)
            {
                var users = _db.User;
                return Ok(users);
            }
            return Unauthorized("Sorry, You are not authorized!");

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
            //if (userExist?.email == User.Identity?.Name)
            //{
            //    return Ok(userExist);
            //}
            return Ok(userExist);
            //return Unauthorized("Sorry, You are not authorized!");
        }

        [HttpPut("{id}")]
        [Authorize]
        public object Put(int id, [FromBody] UpdateUserDto user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (getCurrentUser().isAdmin || getCurrentUser().id == id)
            {
                var userExist = _db.User.Find(id);
                if (userExist != null)
                {

                    userExist.email = user.email != null ? user.email : userExist.email;
                    userExist.firstName = user.firstName != null ? user.firstName : userExist.firstName;
                    userExist.middleName = user.middleName != null ? user.middleName : userExist.middleName;
                    userExist.lastName = user.lastName != null ? user.lastName : userExist.lastName;
                    userExist.isCustomer = user.isCustomer.Value;
                    userExist.isAdmin = user.isAdmin.Value;
                    userExist.isStaff = user.isStaff.Value;
                    if (user.deactivated != null)
                    {
                        userExist.deactivated = user.deactivated.Value;
                    }

                    userExist.UpdatedAt = DateTime.Now;
                    _db.User.Update(userExist);
                    _db.SaveChanges();
                    return Ok("User Updated");
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
            if (getCurrentUser().isAdmin)
            {
                var userExist = _db.User.Find(id);
                if (userExist == null)
                {
                    return NotFound("Sorry, User not found!");
                }
                //if (userExist?.email == User.Identity?.Name)
                if (!userExist.deactivated) return Unauthorized("Sorry, no account deactivation request found!");
                _db.User.Remove(userExist);
                _db.SaveChanges();
                return Ok("User removed!");

            }
            return Unauthorized("Sorry, You are not authorized!");
        }
    }
}
