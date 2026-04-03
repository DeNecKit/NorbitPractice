using Microsoft.Extensions.Configuration;
using System.Data.Entity;

namespace CRUD;

public class Program
{
    public static void Main()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        string configurationString = configuration.GetConnectionString("CinemaDB")
            ?? "Server=localhost;Database=CinemaContext;Trusted_Connection=true;";

        using var db = new CinemaContext(configurationString);

        // LoadExampleData(db);

        Console.WriteLine("Консольная CRUD-утилита для БД Cinema");
        Console.WriteLine("Введите help для просмотра доступных команд");
        Console.WriteLine();

        bool loop = true;
        while (loop)
        {
            Console.Write("> ");
            string? line = Console.ReadLine();
            if (line is null) break;

            string[] words = line.Trim().Split();
            if (words.Length == 0) break;

            string command = words[0];
            switch (command)
            {
                case "help":
                    Console.WriteLine();
                    Console.WriteLine("Таблицы:");
                    Console.WriteLine("  Sessions (MovieId, Price, DateTimeOffset)");
                    Console.WriteLine("    (MovieId - Id фильма в IMDb)");
                    Console.WriteLine("  Tickets (SessionId, Row, Seat)");
                    Console.WriteLine();

                    Console.WriteLine("Доступные команды:");
                    Console.WriteLine("  help - показать данное сообщение");
                    Console.WriteLine("  create <НазваниеТаблицы> - добавить новую строку в таблицу");
                    Console.WriteLine("  read <НазваниеТаблицы> - получить строку (строки) таблицы");
                    Console.WriteLine("  update <НазваниеТаблицы> - обновить данные строки");
                    Console.WriteLine("  delete <НазваниеТаблицы> - удалить строку по Id");
                    Console.WriteLine("  quit - закрыть программу");
                    Console.WriteLine();
                    break;

                case "create":
                    TableAction(words, db, Session.Create, Ticket.Create);
                    break;

                case "read":
                    TableAction(words, db, Session.Read, Ticket.Read);
                    break;

                case "update":
                    TableAction(words, db, Session.Update, Ticket.Update);
                    break;

                case "delete":
                    TableAction(words, db, Session.Delete, Ticket.Delete);
                    break;

                case "quit":
                    loop = false;
                    break;

                default:
                    Console.WriteLine($"Неизвестная команда: '{command}'");
                    break;
            }
        }
    }

    static void TableAction(string[] words, CinemaContext db,
        Func<CinemaContext, IEnumerable<object>> sessionAction,
        Func<CinemaContext, IEnumerable<object>> ticketAction)
    {
        if (words.Length == 1)
        {
            Console.WriteLine("Ожидается название таблицы");
            return;
        }

        try 
        {
            IEnumerable<object> table;
            Type type;
            string tableName = words[1];
            switch (tableName)
            {
                case "Sessions":
                    table = sessionAction(db);
                    type = typeof(Session);
                    break;
                case "Tickets":
                    table = ticketAction(db);
                    type = typeof(Ticket);
                    break;
                default:
                    throw new Exception($"Неизвестная таблица: '{tableName}'");
            };
            PrintTable(type, table);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Не удалось выполнить операцию:");
            Console.WriteLine(ex.ToString());
        }
    }

    static void PrintTable(Type type, IEnumerable<object> table)
    {
        Console.WriteLine();
        if (type == typeof(Session)) Console.WriteLine("Id\tMovieId\t\tPrice\t\tDateAndTime");
        else if (type == typeof(Ticket)) Console.WriteLine("Id\tSessionId\tRow\tSeat");
        else throw new Exception("Unreachable");
        foreach (var row in table)
        {
            if (row is Session session)
            {
                Console.Write($"{session.Id}\t");
                Console.Write($"{session.MovieId}\t");
                Console.Write($"{session.Price}\t\t");
                Console.Write($"{session.DateAndTime}\n");
            }
            else if (row is Ticket ticket)
            {
                Console.Write($"{ticket.Id}\t");
                Console.Write($"{ticket.SessionId}\t\t");
                Console.Write($"{ticket.Row}\t");
                Console.Write($"{ticket.Seat}\n");
            }
            else throw new Exception("Unreachable");
        }
        Console.WriteLine();
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

public class CinemaContext : DbContext
{
    public DbSet<Session> Sessions { get; set; } = null!;
    public DbSet<Ticket> Tickets { get; set; } = null!;

    public CinemaContext(string? connectionString) : base(connectionString) { }

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
