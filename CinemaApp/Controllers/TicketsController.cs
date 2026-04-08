using CinemaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController(ApplicationContext context) : ControllerBase
{
    private readonly ApplicationContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ticket>>> GetAll()
    {
        return await _context.Tickets.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Ticket>> Get(int id)
    {
        Ticket? ticket = await _context.Tickets.FindAsync(id);
        if (ticket is null) return NotFound();
        return ticket;
    }

    [HttpPost]
    public async Task<ActionResult> Post(Ticket item)
    {
        try
        {
            await _context.Tickets.AddAsync(item);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Problem(title: "Internal server error", detail: ex.Message);
        }

        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, Ticket item)
    {
        if (id != item.Id) return BadRequest("Id mismatch");

        try
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await _context.Tickets.FindAsync(id) is null) return NotFound();
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Ticket? ticket = await _context.Tickets.FindAsync(id);
        if (ticket is null) return NotFound();

        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
