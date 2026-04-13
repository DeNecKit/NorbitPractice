using CinemaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController(ApplicationContext context) : ControllerBase
{
    private readonly ApplicationContext _context = context;

    [HttpGet("{publicId}")]
    public async Task<ActionResult<GetTicketDTO>> Get(Guid publicId)
    {
        Ticket? ticket = await _context.Tickets
            .Include(ticket => ticket.Session)
            .ThenInclude(session => session.Movie)
            .FirstOrDefaultAsync(
                ticket => ticket.PublicId == publicId);
        if (ticket is null) return NotFound();

        return new GetTicketDTO
        {
            PublicId = ticket.PublicId,
            MovieTitle = ticket.Session.Movie.Title,
            DateAndTime = ticket.Session.DateAndTime,
            Row = ticket.Row,
            Seat = ticket.Seat,
            Price = ticket.Session.Price,
        };
    }

    [HttpPost]
    public async Task<ActionResult> Post(CreateTicketDTO dto)
    {
        Ticket ticket = new()
        {
            PublicId = dto.PublicId,
            SessionId = dto.SessionId,
            Row = dto.Row,
            Seat = dto.Seat,
        };

        try
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return BadRequest(ex.Message);
        }

        Session? session = await _context.Sessions
            .FindAsync(ticket.SessionId);
        ticket.Session = session!;

        Movie? movie = await _context.Movies
            .FindAsync(ticket.Session.MovieId);
        ticket.Session.Movie = movie!;

        return CreatedAtAction
        (
            nameof(Get),
            new { id = ticket.Id },
            new GetTicketDTO
            {
                PublicId = ticket.PublicId,
                MovieTitle = ticket.Session.Movie.Title,
                DateAndTime = ticket.Session.DateAndTime,
                Row = ticket.Row,
                Seat = ticket.Seat,
                Price = ticket.Session.Price,
            }
        );
    }
}
