namespace CinemaApp.Models;

public class Ticket
{
    public int Id { get; set; }
    public Guid PublicId { get; set; }
    public int SessionId { get; set; }
    public virtual Session Session { get; set; } = null!;
    public byte Row { get; set; }
    public byte Seat { get; set; }
}

public class GetTicketDTO
{
    public Guid PublicId { get; set; }
    public string MovieTitle { get; set; } = null!;
    public DateTimeOffset DateAndTime { get; set; }
    public byte Row { get; set; }
    public byte Seat { get; set; }
    public decimal Price { get; set; }
}

public class CreateTicketDTO
{
    public Guid PublicId { get; set; }
    public int SessionId { get; set; }
    public byte Row { get; set; }
    public byte Seat { get; set; }
}