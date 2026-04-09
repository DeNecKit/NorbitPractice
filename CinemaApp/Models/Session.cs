using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Models;

public class Session
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public virtual Movie Movie { get; set; } = null!;
    [Precision(8, 2)]
    public decimal Price { get; set; }
    public DateTimeOffset DateAndTime { get; set; }
    public virtual List<Ticket> Tickets { get; set; } = null!;
}
