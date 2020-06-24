using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenTokSDK;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly StudentContext _context;

        public SessionsController(StudentContext context)
        {
            _context = context;
        }

        // GET: api/Sessions
        [HttpGet]
        public async Task<object> GetSession()
        {
            return await (from s in _context.Session
                          join f in _context.Filiere on s.FiliereId equals f.FiliereId
                          join p in _context.Professor on s.Profid equals p.Profid
                          select new { s.SeanceId, s.Name, p.FirstName, nom = f.Name, s.State,s.Profid,s.FiliereId,
                          }

                          ).ToListAsync();
        }

        // GET: api/Sessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Session>> GetSession(int id)
        {
            var session = await _context.Session.FindAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            return session;
        }

        //POST: api/Utilisateurs/Login
        [HttpGet]
        [Route("select")]
        public async Task<ActionResult> select(string day)
        {
            var user = _context.Session.FromSqlInterpolated($"SELECT * FROM dbo.session").Where(res => res.Day == day).FirstOrDefault();
            
           
                return Ok(new
                {
                    user
                   
                }); ;
            
            
        
        }

        // PUT: api/Sessions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}/{id2}/{id3}")]
        public async Task<IActionResult> PutSession(int id,int id2,int id3, Models.Session session)
        {
            if (id != session.SeanceId)
            {
                return BadRequest();
            }

            _context.Entry(session).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(id))
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

        // POST: api/Sessions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Models.Session>> PostSession(Models.Session session)
        {
            _context.Session.Add(session);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SessionExists(session.SeanceId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSession", new { id = session.SeanceId }, session);
        }

        // DELETE: api/Sessions/5
        [HttpDelete("{id}/{id2}/{id3}")]
        public async Task<ActionResult<Models.Session>> DeleteSession(int id,int id2,int id3)
        {
            var session = await _context.Session.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            _context.Session.Remove(session);
            await _context.SaveChangesAsync();

            return session;
        }

        private bool SessionExists(int id)
        {
            return _context.Session.Any(e => e.SeanceId == id);
        }
    }
}
