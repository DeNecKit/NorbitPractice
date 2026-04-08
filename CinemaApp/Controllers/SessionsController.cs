using CinemaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController(ApplicationContext context) : ControllerBase
    {
        private readonly ApplicationContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session>>> GetAll()
        {
            return await _context.Sessions.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Session>> Get(int id)
        {
            Session? session = await _context.Sessions.FindAsync(id);
            if (session is null) return NotFound();
            return session;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Session item)
        {
            try
            {
                await _context.Sessions.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Problem(title: "Internal server error", detail: ex.Message);
            }

            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Session item)
        {
            if (id != item.Id) return BadRequest("Id mismatch");

            try
            {
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _context.Sessions.FindAsync(id) is null) return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Session? session = await _context.Sessions.FindAsync(id);
            if (session is null) return NotFound();

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
