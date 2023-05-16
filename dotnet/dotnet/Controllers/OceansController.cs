using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet.Data;
using proiectelul.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;


namespace dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OceansController : ControllerBase
    {
        private readonly DataContext _context;

        public OceansController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Oceans  in ordinea nr de rechini folosing include
        // 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ocean>>> GetOceans()
        {
            var oceans = await _context.Oceans
                .Include(o => o.Sharks)
                .ToListAsync();

            var sortedOceans = oceans.OrderByDescending(o => o.Sharks.Count).ToList();

            if (sortedOceans == null || !sortedOceans.Any())
            {
                return NotFound();
            }

            return sortedOceans;
        }


        // GET: api/Oceans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ocean>> GetOcean(int id)
        {
          if (_context.Oceans == null)
          {
              return NotFound();
          }
            var ocean = await _context.Oceans.FindAsync(id);

            if (ocean == null)
            {
                return NotFound();
            }

            return ocean;
        }

        [HttpGet("~/api/oceans/{oceanId}/sharks")]
        public async Task<ActionResult<IEnumerable<Shark>>> GetSharksbyOcean(int oceanId)
        {
            var sharks = await _context.Sharks.Where(s => s.OceanId == oceanId).ToListAsync();
            if (sharks == null)
            {
                return NotFound();
            }
            return sharks;
        }


        // PUT: api/Oceans/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOcean(int id, Ocean ocean)
        {
            if (id != ocean.Id)
            {
                return BadRequest();
            }

            _context.Entry(ocean).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OceanExists(id))
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

        // POST: api/Oceans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ocean>> PostOcean(Ocean ocean)
        {
          if (_context.Oceans == null)
          {
              return Problem("Entity set 'DataContext.Oceans'  is null.");
          }
            _context.Oceans.Add(ocean);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOcean", new { id = ocean.Id }, ocean);
        }

        // DELETE: api/Oceans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOcean(int id)
        {
            if (_context.Oceans == null)
            {
                return NotFound();
            }
            var ocean = await _context.Oceans.FindAsync(id);
            if (ocean == null)
            {
                return NotFound();
            }

            _context.Oceans.Remove(ocean);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OceanExists(int id)
        {
            return (_context.Oceans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
