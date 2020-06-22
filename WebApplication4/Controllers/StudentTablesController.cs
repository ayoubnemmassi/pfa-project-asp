using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
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

        [HttpGet("{id}/{id2}")]
        public async Task<ActionResult<IEnumerable<StudentTable>>> GetStudentTable(int id, string id2)
        {
            return await _context.StudentTable.FromSqlInterpolated($"SELECT * FROM dbo.student_table").Where(res => res.StudentId != id).ToListAsync();

        }
        [HttpGet("{name}")]
        [Route("friend")]
        public async Task<ActionResult<IEnumerable<StudentTable>>> GetStudentTable(string name)
        {
            return await _context.StudentTable.FromSqlInterpolated($"SELECT * FROM dbo.student_table").Where(res => res.LastName == name).ToListAsync();

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
            var student = await _context.StudentTable.FindAsync(id);
         

    


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

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<StudentTable> patchDoc)
        {
            string emailBody = string.Empty;
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var authorFromDB = await _context.StudentTable.FirstOrDefaultAsync(x => x.StudentId == id);
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(authorFromDB.LastName + " " + authorFromDB.FirstName, authorFromDB.Mail));
            message.From.Add(new MailboxAddress("intelligent classes", "ayoub.nemmassi@gmail.com"));
            message.Subject = "Approuvé";
            emailBody = "<h1> hello <a href='http://localhost:4200/' > ici</a></h1>";
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = emailBody
            };
            if (authorFromDB == null)
            {
                return NotFound();
            }
            patchDoc.ApplyTo(authorFromDB);



            var isValid = TryValidateModel(authorFromDB);



            if (!isValid)
            {
                return BadRequest(ModelState);
            } using (var emailClient = new SmtpClient())
            {
                emailClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                emailClient.Authenticate("ayoub.nemmassi@gmail.com", "yugigx1998");
                emailClient.Send(message);
                emailClient.Disconnect(true);
            }
            await _context.SaveChangesAsync();  
           


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
                    user.Password,
                    user.Phonenumber,       
                    user.Address,
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
