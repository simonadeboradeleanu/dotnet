using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet.Data;
using proiectelul.Models;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using dotnet.Models;

namespace dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharksController : ControllerBase
    {
        private readonly DataContext _context;

        public SharksController(DataContext context)
        {
            _context = context;
        }


        // GET: api/Sharks  in ordinea anului in care s au nascut folosind join
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Shark>>> GetSharks()
        {
            var sharks = await _context.Sharks
                .GroupJoin(_context.Details, // left outer join
                    s => s.Id,
                    d => d.SharkId,
                    (s, d) => new { Shark = s, Detail = d.FirstOrDefault() }
                )
                .OrderBy(sd => sd.Detail != null ? sd.Detail.BirthYear : int.MaxValue) 
                .Select(sd => sd.Shark)
                .ToListAsync();

            if (sharks == null || !sharks.Any())
            {
                return NotFound();
            }

            return sharks;
        }



        // GET: api/Sharks/5
        [HttpGet("{id}")]

        public async Task<ActionResult<Shark>> GetShark(int id)     //, [FromBody] User usr)
        {

            /*if (!usr.admin)
            { return Unauthorized(); }*/

          if (_context.Sharks == null)
          {
              return NotFound();
          }
            var shark = await _context.Sharks.FindAsync(id);

            if (shark == null)
            {
                return NotFound();
            }

            return shark;
        }

        // PUT: api/Sharks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShark(int id, Shark shark)
        {
            /*if (!IsAdmin(usr.username))
            {
                return Unauthorized();
            }*/
            if (id != shark.Id)
            {
                return BadRequest();
            }

            _context.Entry(shark).State = EntityState.Modified;
            if (shark.Detail != null)
            {
                _context.Entry(shark.Detail).State = shark.Detail.SharkId == 0 ? EntityState.Added : EntityState.Modified;
                shark.Detail.SharkId = shark.Id; 
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SharkExists(id))
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
        
        // POST: api/Sharks
        //[Authorize(Roles = "Admin")]
        [HttpPost]

        public async Task<ActionResult<Shark>> PostShark(Shark shark)
        {
            /*if (!usr.admin)
            { return Unauthorized(); }*/

            if (_context.Sharks == null)
          {
              return Problem("Entity set 'DataContext.Sharks'  is null.");
          }
            if (shark.Detail != null)
            {
                _context.Details.Add(shark.Detail);
            }

            _context.Sharks.Add(shark);
            await _context.SaveChangesAsync();
            shark.Detail.SharkId = shark.Id; 
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShark", new { id = shark.Id }, shark);
        }

        // DELETE: api/Sharks/5
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteShark(int id)
        {
            if (_context.Sharks == null)
            {
                return NotFound();
            }
            var shark = await _context.Sharks.FindAsync(id);
            if (shark == null)
            {
                return NotFound();
            }
            if (shark.Detail != null)
            {
                _context.Details.Remove(shark.Detail);
            }

            _context.Sharks.Remove(shark);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SharkExists(int id)
        {
            return (_context.Sharks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
