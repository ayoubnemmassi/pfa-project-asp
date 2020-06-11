using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class PresencesController : ControllerBase
    {
        private readonly StudentContext _context;

        public PresencesController(StudentContext context)
        {
            _context = context;
        }

        // GET: api/Presences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Presence>>> GetPresence()
        {
            return await _context.Presence.ToListAsync();
        }

        // GET: api/Presences/5
        [HttpGet("{id}/{id2}")]
      
        public async Task<ActionResult<IEnumerable<Presence>>> Getpresence(int id,DateTime id2)
        {
            return await _context.Presence.FromSqlInterpolated($"SELECT * FROM dbo.presence").Where(res => res.SeanceId == id ).Where(res => res.Date.Date == id2.Date ).ToListAsync();
   
        }

        // PUT: api/Presences/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPresence(int id, Presence presence)
        {
            if (id != presence.SeanceId)
            {
                return BadRequest();
            }

            _context.Entry(presence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PresenceExists(id))
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

        // POST: api/Presences
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Presence>> PostPresence(Presence presence)
        {
            _context.Presence.Add(presence);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PresenceExists(presence.SeanceId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPresence", new { id = presence.SeanceId }, presence);
        }

        // DELETE: api/Presences/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Presence>> DeletePresence(int id)
        {
            var presence = await _context.Presence.FindAsync(id);
            if (presence == null)
            {
                return NotFound();
            }

            _context.Presence.Remove(presence);
            await _context.SaveChangesAsync();

            return presence;
        }

        private bool PresenceExists(int id)
        {
            return _context.Presence.Any(e => e.SeanceId == id);
        }
    }
}
