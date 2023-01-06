using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Asp_Dot_Net_Web_Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp_Dot_Net_Web_Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        public readonly ApplicationDbContext _db;
        public static User user = new User();
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration, ApplicationDbContext db)
        {
            _db = db;
            _configuration = configuration;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public User getCurrentUser()
        {
            var userEmail = User.Identity?.Name;
            var user = _db.User.Where(u => u.email == userEmail).First();
            return user;

        }

        // POST api/values
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] RegisterDto request)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all the fields!");
            if (_db.User.Any(u => u.email == request.email)) return BadRequest("Sorry, Email already in use!");

            CreatePasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);

            user.passwordHash = passwordHash;
            user.passwordSalt = passwordSalt;
            user.email = request.email;
            user.firstName = request.firstName;
            user.middleName = request.middleName;
            user.lastName = request.lastName;
            _db.User.Add(user);
            await _db.SaveChangesAsync();
            string token = CreateToken(user);

            var userObj = new
            {
                user.id,
                user.firstName,
                user.lastName,
                user.middleName,
                user.email,
                user.isAdmin,
                user.isCustomer,
                user.isStaff
            };
            return Ok(new { token, user.id, userObj });
        }

        [HttpPut("{id}")]
        [Authorize]
        public object Put(int id, [FromBody] UpdateUserDto user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userExist = _db.User.Find(id);
            if (userExist != null)
            {
                userExist.email = user.email;
                userExist.firstName = user.firstName;
                userExist.middleName = user.middleName;
                userExist.lastName = user.lastName;
                userExist.UpdatedAt = DateTime.Now;
                _db.User.Update(userExist);
                _db.SaveChanges();
                return Ok("User Updated");
            }
            return NotFound("Sorry, Book not found!");

        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto request)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all the fields!");
            var user = _db.User.SingleOrDefault(u => u.email == request.email);
            if (user == null) return BadRequest("Wrong Email or Password!");
            if (!VerifyPasswordHash(request.password, user.passwordHash, user.passwordSalt)) return BadRequest("Wrong Email or Password!");
            string token = CreateToken(user);
            var userObj = new
            {
                user.id,
                user.firstName,
                user.lastName,
                user.email,
                user.middleName,
                user.isAdmin,
                user.isCustomer,
                user.isStaff
            };
            return Ok(new { token, user.id, userObj });
            //return Ok(Json(new { token = token, userId = user.id }));
            //return Ok(new { token = token, userId = user.id });
        }


        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.email),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        [HttpGet("verify-token")]
        [Authorize]
        public async Task<ActionResult> VerifyToken()
        {
            var _jwtHandler = new JwtSecurityTokenHandler();
            try
            {
                // Extract the token from the Authorization header
                var authorizationHeader = Request.Headers["Authorization"];
                var token = authorizationHeader.ToString().Replace("Bearer ", "");
                var userEmail = User.Identity?.Name;
                var user = _db.User.Where(u => u.email == userEmail).First();
                var filteredUser = new
                {
                    id = user.id,
                    firstName = user.firstName,
                    middleName = user.middleName,
                    lastName = user.lastName,
                    email = user.email,
                    isAdmin = user.isAdmin,
                    isStaff = user.isStaff,
                    isCustomer = user.isCustomer
                };
                return Ok(new { user = filteredUser, token });
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);

            }

        }
    }
}

