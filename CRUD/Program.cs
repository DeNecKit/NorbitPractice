using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

public class Program
{
    public static void Main()
    {
        using var db = new CinemaContext();

        LoadExampleData(db);

        Console.WriteLine("Консольная CRUD-утилита для БД Cinema");
        Console.WriteLine("Введите help для просмотра доступных команд");

        foreach (var ticket in db.Tickets)
        {
            Console.Write($"Билет с рядом {ticket.Row} и местом {ticket.Seat}");
            Console.Write($" на фильм с id = {ticket.Session.MovieId}");
            Console.Write($" в {ticket.Session.DateAndTime}");
            Console.WriteLine();
        }
    }

    static void LoadExampleData(CinemaContext db)
    {
        db.Tickets.RemoveRange(db.Tickets);
        db.Sessions.RemoveRange(db.Sessions);
        db.SaveChanges();

        var session = new Session
        {
            MovieId = 14149634,
            Price = 400,
            DateAndTime = new DateTimeOffset(2026, 05, 15, 12, 15, 00, new TimeSpan(03, 00, 00)),
        };

        db.Sessions.Add(session);

        db.Tickets.Add(new Ticket { SessionId = session.Id, Row = 7, Seat = 15 });
        db.Tickets.Add(new Ticket { SessionId = session.Id, Row = 7, Seat = 16 });

        db.SaveChanges();
    }
}

public class Session
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public decimal Price { get; set; }
    public DateTimeOffset DateAndTime { get; set; }

    public virtual List<Ticket> Tickets { get; set; }
}

public class Ticket
{
    public int Id { get; set; }
    [ForeignKey(nameof(Session))]
    public int SessionId { get; set; }
    public virtual Session Session { get; set; }
    public byte Row { get; set; }
    public byte Seat { get; set; }
}

public class CinemaContext : DbContext
{
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>()
            .HasIndex(ticket => new
            {
                ticket.SessionId,
                ticket.Row,
                ticket.Seat,
            })
            .IsUnique();
    }
}
