namespace CinemaApp.Models;

public class Ticket
{
    public int Id { get; set; }
    public int SessionId { get; set; }
    public virtual Session Session { get; set; } = null!;
    public byte Row { get; set; }
    public byte Seat { get; set; }
}
