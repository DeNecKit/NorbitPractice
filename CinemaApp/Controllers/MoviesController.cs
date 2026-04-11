using CinemaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController(ApplicationContext context) : ControllerBase
{
    private readonly ApplicationContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetAll()
    {
        return await _context.Movies.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> Get(int id)
    {
        Movie? movie = await _context.Movies.FindAsync(id);
        if (movie is null) return NotFound();
        return movie;
    }
}
