using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD;

public class Ticket
{
    public int Id { get; set; }
    [ForeignKey(nameof(Session))]
    public int SessionId { get; set; }
    public virtual Session Session { get; set; } = null!;
    public byte Row { get; set; }
    public byte Seat { get; set; }

    public static IEnumerable<Ticket> Create(CinemaContext db)
    {
        string? input = null;
        Console.WriteLine("Введите значения полей новой строки:");

        int sessionId;
        while (true)
        {
            Console.Write("  SessionId = ");
            input = Console.ReadLine();
            if (input is null) throw new Exception("    Не удалось считать значение SessionId");
            else
            {
                if (int.TryParse(input, out sessionId)) break;
                Console.WriteLine("    Некорректное значение SessionId");
            }
        }

        byte row;
        while (true)
        {
            Console.Write("  Row = ");
            input = Console.ReadLine();
            if (input is null) throw new Exception("    Не удалось считать значение Row");
            else
            {
                if (byte.TryParse(input, out row)) break;
                Console.WriteLine("    Некорректное значение Row");
            }
        }

        byte seat;
        while (true)
        {
            Console.Write("  Seat = ");
            input = Console.ReadLine();
            if (input is null) throw new Exception("    Не удалось считать значение Seat");
            else
            {
                if (byte.TryParse(input, out seat)) break;
                Console.WriteLine("    Некорректное значение Seat");
            }
        }

        var ticket = new Ticket
        {
            SessionId = sessionId,
            Row = row,
            Seat = seat,
        };
        db.Tickets.Add(ticket);
        db.SaveChanges();
        return [ticket];
    }

    public static IEnumerable<Ticket> Read(CinemaContext db)
    {
        Console.WriteLine("Введите Id строки (-1, чтобы показать все строки)");
        string? input;
        int id;
        while (true)
        {
            Console.Write("  Id = ");
            input = Console.ReadLine();
            if (input is null) throw new Exception("    Не удалось считать значение Id");
            else
            {
                if (int.TryParse(input, out id)) break;
                Console.WriteLine("    Некорректное значение Id");
            }
        }

        if (id < 0) return db.Tickets;
        Ticket? ticket = db.Tickets.Find(id);
        if (ticket is null) return [];
        return [ticket];
    }

    public static IEnumerable<Ticket> Update(CinemaContext db)
    {
        string? input = null;

        Console.WriteLine("Введите Id строки для изменения");
        int id;
        while (true)
        {
            Console.Write("  Id = ");
            input = Console.ReadLine();
            if (input is null) throw new Exception("    Не удалось считать значение Id");
            else
            {
                if (int.TryParse(input, out id)) break;
                Console.WriteLine("    Некорректное значение Id");
            }
        }

        Console.WriteLine("Введите новые значения полей строки (оставьте поле пустым, чтобы не менять):");

        int? sessionId = null;
        while (true)
        {
            Console.Write("  SessionId = ");
            input = Console.ReadLine();
            if (input is null) throw new Exception("    Не удалось считать значение SessionId");
            else
            {
                if (string.IsNullOrEmpty(input)) break;
                if (int.TryParse(input, out int _sessionId))
                {
                    sessionId = _sessionId;
                    break;
                }
                Console.WriteLine("    Некорректное значение SessionId");
            }
        }

        byte? row = null;
        while (true)
        {
            Console.Write("  Row = ");
            input = Console.ReadLine();
            if (input is null) throw new Exception("    Не удалось считать значение Row");
            else
            {
                if (string.IsNullOrEmpty(input)) break;
                if (byte.TryParse(input, out byte _row))
                {
                    row = _row;
                    break;
                }
                Console.WriteLine("    Некорректное значение Row");
            }
        }

        byte? seat = null;
        while (true)
        {
            Console.Write("  Seat = ");
            input = Console.ReadLine();
            if (input is null) throw new Exception("    Не удалось считать значение Seat");
            else
            {
                if (string.IsNullOrEmpty(input)) break;
                if (byte.TryParse(input, out byte _seat))
                {
                    seat = _seat;
                    break;
                }
                Console.WriteLine("    Некорректное значение Seat");
            }
        }

        Ticket? ticket = db.Tickets.Find(id);
        if (ticket is null) return [];

        bool changed = false;

        if (sessionId is not null) ticket.SessionId = (int)sessionId;
        if (row is not null) ticket.Row = (byte)row;
        if (seat is not null) ticket.Seat = (byte)seat;

        if (changed) db.SaveChanges();
        return [ticket];
    }

    public static IEnumerable<Ticket> Delete(CinemaContext db)
    {
        Console.WriteLine("Введите Id строки для удаления");
        string? input;
        int id;
        while (true)
        {
            Console.Write("  Id = ");
            input = Console.ReadLine();
            if (input is null) throw new Exception("    Не удалось считать значение Id");
            else
            {
                if (int.TryParse(input, out id)) break;
                Console.WriteLine("    Некорректное значение Id");
            }
        }

        Ticket? ticket = db.Tickets.Find(id);
        if (ticket is null) return [];

        db.Tickets.Remove(ticket);
        db.SaveChanges();
        return [ticket];
    }
}