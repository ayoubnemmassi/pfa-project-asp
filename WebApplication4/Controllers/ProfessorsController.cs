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
    public class ProfessorsController : ControllerBase
    {
        private readonly StudentContext _context;

        public ProfessorsController(StudentContext context)
        {
            _context = context;
        }

        // GET: api/Professors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professor>>> GetProfessor()
        {
            return await _context.Professor.ToListAsync();
        }

        // GET: api/Professors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Professor>> GetProfessor(int id)
        {
            var professor = await _context.Professor.FindAsync(id);

            if (professor == null)
            {
                return NotFound();
            }

            return professor;
        }

        // PUT: api/Professors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfessor(int id, Professor professor)
        {
            if (id != professor.Profid)
            {
                return BadRequest();
            }

            _context.Entry(professor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfessorExists(id))
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

        // POST: api/Professors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Professor>> PostProfessor(Professor professor)
        {
            _context.Professor.Add(professor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfessor", new { id = professor.Profid }, professor);
        }
        //POST: api/Utilisateurs/Login
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody]Professor prof)
        {
            var user = _context.Professor.FromSqlInterpolated($"SELECT * FROM dbo.professor").Where(res => res.Mail == prof.Mail).FirstOrDefault();
            //var user = await _context.Utilisateurs.FindAsync(utilisateur.Id);
            if (user != null && user.Password == prof.Password && user.Mail == prof.Mail)
            {
                return Ok(new
                {
                    user.Profid,
                    user.FirstName,
                    user.LastName,
                    user.Mail,
                    user.Type,
                    user.Image
                }); ;
            }
            else
            {
                return BadRequest(new { message = "Email ou Mot de passe est incorrect" });
            }
        }
        // DELETE: api/Professors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Professor>> DeleteProfessor(int id)
        {
            var professor = await _context.Professor.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }

            _context.Professor.Remove(professor);
            await _context.SaveChangesAsync();

            return professor;
        }

        private bool ProfessorExists(int id)
        {
            return _context.Professor.Any(e => e.Profid == id);
        }
    }
}
