 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet.Data;
using dotnet.Models;
using System.Security.Claims;
using dotnet.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        [ActivatorUtilitiesConstructor]
        public UsersController(DataContext context)
        {
            _context = context;
        }

        private string GenerateJwtToken(string username)
        {

            var key = Encoding.ASCII.GetBytes("elizabalintelizabalint17mai");
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username)
    };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] User user)
        {
            
            var users = _context.Users.ToDictionary(u => u.username, u => u.password);
            
            if (users.Any(u => u.Key == user.username && u.Value == user.password))
            {
                var token = GenerateJwtToken(user.username);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }


        // GET: api/Users folosind orderBy
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var users = await _context.Users
                                       .OrderBy(u => u.username)
                                       .ToListAsync();
            if (users.Count == 0)
            {
                return NotFound();
            }
            return users;
        }



        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }



        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;
            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        //[Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'DataContext.User'  is null.");
          }

            //var cl = new  Claim("isAdmin", "true");

            //TO REVIEW: build failed la claims
            /*if (user.admin)
            {
                user.ClaimUsers.Add(new ClaimUser("isAdmin", "true", user, cl));
            }*/

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }



        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
