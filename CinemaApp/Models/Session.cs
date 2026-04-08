namespace CinemaApp.Models;

public class Session
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public virtual Movie Movie { get; set; } = null!;
    public decimal Price { get; set; }
    public DateTimeOffset DateAndTime { get; set; }
    public virtual List<Ticket> Tickets { get; set; } = null!;
}
