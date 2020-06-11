using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilieresController : ControllerBase
    {
        private readonly StudentContext _context;

        public FilieresController(StudentContext context)
        {
            _context = context;
        }

        // GET: api/Filieres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Filiere>>> GetFiliere()
        {
            return await _context.Filiere.ToListAsync();
        }

        // GET: api/Filieres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Filiere>> GetFiliere(int id)
        {
            var filiere = await _context.Filiere.FindAsync(id);

            if (filiere == null)
            {
                return NotFound();
            }

            return filiere;
        }

        // PUT: api/Filieres/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFiliere(int id, Filiere filiere)
        {
            if (id != filiere.FiliereId)
            {
                return BadRequest();
            }

            _context.Entry(filiere).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FiliereExists(id))
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

        // POST: api/Filieres
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Filiere>> PostFiliere(Filiere filiere)
        {
            _context.Filiere.Add(filiere);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFiliere", new { id = filiere.FiliereId }, filiere);
        }

        // DELETE: api/Filieres/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Filiere>> DeleteFiliere(int id)
        {
            var filiere = await _context.Filiere.FindAsync(id);
            if (filiere == null)
            {
                return NotFound();
            }

            _context.Filiere.Remove(filiere);
            await _context.SaveChangesAsync();

            return filiere;
        }

        private bool FiliereExists(int id)
        {
            return _context.Filiere.Any(e => e.FiliereId == id);
        }
    }
}
