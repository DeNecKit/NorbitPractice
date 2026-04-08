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

    [HttpPost]
    public async Task<ActionResult> Post(Movie item)
    {
        try
        {
            await _context.Movies.AddAsync(item);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Problem(title: "Internal server error", detail: ex.Message);
        }

        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, Movie item)
    {
        if (id != item.Id) return BadRequest("Id mismatch");

        try
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await _context.Movies.FindAsync(id) is null) return NotFound();
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Movie? movie = await _context.Movies.FindAsync(id);
        if (movie is null) return NotFound();

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
