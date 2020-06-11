using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenTokSDK;
using WebApplication4;
using WebApplication4.Models;

/*namespace WebApplication4.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : Controller
    {

        private IConfiguration _Configuration;

        public RoomsController(IConfiguration config)
        {
            _Configuration = config;
        }

        public class RoomForm
        {
            public string RoomName { get; set; }
        }

        [HttpPost]
        public IActionResult GetSession([FromBody]RoomForm roomForm)
        {
            var apiKey = int.Parse(_Configuration["ApiKey"]);
            var apiSecret = _Configuration["ApiSecret"];
            var opentok = new OpenTok(apiKey, apiSecret);
            var roomName = roomForm.RoomName;
            string sessionId;
            string token;
            using (var db = new OpentokContext())
            {
                var room = db.Rooms.Where(r => r.RoomName == roomName).FirstOrDefault();
                if (room != null)
                {
                    sessionId = room.SessionId;
                    token = opentok.GenerateToken(sessionId);
                    room.Token = token;
                    db.SaveChanges();
                }
                else
                {
                    var session = opentok.CreateSession();
                    sessionId = session.Id;
                    token = opentok.GenerateToken(sessionId);
                    var roomInsert = new Room
                    {
                        SessionId = sessionId,
                        Token = token,
                        RoomName = roomName
                    };
                    db.Add(roomInsert);
                    db.SaveChanges();
                }
            }

            return Json(new { sessionId = sessionId, token = token, apiKey = _Configuration["ApiKey"] });

        }
        private readonly StudentContext _context;

        public RoomsController(StudentContext context,string name)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRoom_1()
        {
            return await _context.Room_1.ToListAsync();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = await _context.Room_1.FindAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if (id != room.RoomId)
            {
                return BadRequest();
            }

            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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

        // POST: api/Rooms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            _context.Room_1.Add(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoom", new { id = room.RoomId }, room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Room>> DeleteRoom(int id)
        {
            var room = await _context.Room_1.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Room_1.Remove(room);
            await _context.SaveChangesAsync();

            return room;
        }

        private bool RoomExists(int id)
        {
            return _context.Room_1.Any(e => e.RoomId == id);
        }
    }
}*/
