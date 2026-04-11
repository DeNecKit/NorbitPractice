using CinemaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Controllers;

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
}
