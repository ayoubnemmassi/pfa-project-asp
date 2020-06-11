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
    public class StudentTablesController : ControllerBase
    {
        private readonly StudentContext _context;

        public StudentTablesController(StudentContext context)
        {
            _context = context;
        }

        // GET: api/StudentTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentTable>>> GetStudentTable()
        {
            return await _context.StudentTable.ToListAsync();
        }

        // GET: api/StudentTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentTable>> GetStudentTable(int id)
        {
            var studentTable = await _context.StudentTable.FindAsync(id);

            if (studentTable == null)
            {
                return NotFound();
            }

            return studentTable;
        }

        // PUT: api/StudentTables/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentTable(int id, StudentTable studentTable)
        {
            if (id != studentTable.StudentId)
            {
                return BadRequest();
            }

            _context.Entry(studentTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentTableExists(id))
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

        // POST: api/StudentTables
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<StudentTable>> PostStudentTable(StudentTable studentTable)
        {
            _context.StudentTable.Add(studentTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentTable", new { id = studentTable.StudentId }, studentTable);
        }

        //POST: api/Utilisateurs/Login
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody]StudentTable student)
        {
            var user = _context.StudentTable.FromSqlInterpolated($"SELECT * FROM dbo.student_table").Where(res => res.Mail == student.Mail ).FirstOrDefault();
            //var user = await _context.Utilisateurs.FindAsync(utilisateur.Id);
            if (user != null && user.Password == student.Password && user.Mail == student.Mail)
            {
                return Ok(new
                {
                    user.StudentId,
                    user.FirstName,
                    user.LastName,
                    user.Mail,
                    user.Approved,
                    user.Type,
                    user.Image
                }); ;
            }
            else
            {
                return BadRequest(new { message = "Email ou Mot de passe est incorrect" });
            }
        }
        // DELETE: api/StudentTables/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentTable>> DeleteStudentTable(int id)
        {
            var studentTable = await _context.StudentTable.FindAsync(id);
            if (studentTable == null)
            {
                return NotFound();
            }

            _context.StudentTable.Remove(studentTable);
            await _context.SaveChangesAsync();

            return studentTable;
        }

        private bool StudentTableExists(int id)
        {
            return _context.StudentTable.Any(e => e.StudentId == id);
        }
    }
}
