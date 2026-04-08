using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Models;

public class ApplicationContext : DbContext
{
    public DbSet<Movie> Movies { get; set; } = null!;
    public DbSet<Session> Sessions { get; set; } = null!;
    public DbSet<Ticket> Tickets { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }
}
