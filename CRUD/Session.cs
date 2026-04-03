namespace CRUD;

public class Session
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public decimal Price { get; set; }
    public DateTimeOffset DateAndTime { get; set; }
    public virtual List<Ticket> Tickets { get; set; } = null!;

    public static IEnumerable<Session> Create(CinemaContext db)
    {
        string? input = null;
        Console.WriteLine("Введите значения полей новой строки:");

        int movieId;
        while (true)
        {
            Console.Write("  MovieId = ");
            input = Console.ReadLine();
            if (input is null) throw new Exception("    Не удалось считать значение MovieId");
            else
            {
                if (int.TryParse(input, out movieId)) break;
                Console.WriteLine("    Некорректное значение MovieId");
            }
        }

        decimal price;
        while (true)
        {
            Console.Write("  Price = ");
            input = Console.ReadLine();
            if (input is null) throw new Exception("    Не удалось считать значение Price");
            else
            {
                if (decimal.TryParse(input, out price)) break;
                Console.WriteLine("    Некорректное значение Price");
            }
        }

        DateTimeOffset dateAndTime;
        while (true)
        {
            Console.Write("  DateAndTime = ");
            input = Console.ReadLine();
            if (input is null) throw new Exception("    Не удалось считать значение DateAndTime");
            else
            {
                if (DateTimeOffset.TryParse(input, out dateAndTime)) break;
                Console.WriteLine("    Некорректное значение DateAndTime");
            }
        }

        var session = new Session
        {
            MovieId = movieId,
            Price = price,
            DateAndTime = dateAndTime,
        };
        db.Sessions.Add(session);
        db.SaveChanges();
        return [session];
    }

    public static IEnumerable<Session> Read(CinemaContext db)
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

        if (id < 0) return db.Sessions;
        Session? session = db.Sessions.Find(id);
        if (session is null) return [];
        return [session];
    }

    public static IEnumerable<Session> Update(CinemaContext db)
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

        int? movieId = null;
        while (true)
        {
            Console.Write("  MovieId = ");
            input = Console.ReadLine();
            if (input is null) throw new Exception("    Не удалось считать значение MovieId");
            else
            {
                if (string.IsNullOrEmpty(input)) break;
                if (int.TryParse(input, out int _movieId))
                {
                    movieId = _movieId;
                    break;
                }
                Console.WriteLine("    Некорректное значение MovieId");
            }
        }

        decimal? price = null;
        while (true)
        {
            Console.Write("  Price = ");
            input = Console.ReadLine();
            if (input is null) throw new Exception("    Не удалось считать значение Price");
            else
            {
                if (string.IsNullOrEmpty(input)) break;
                if (decimal.TryParse(input, out decimal _price))
                {
                    price = _price;
                    break;
                }
                Console.WriteLine("    Некорректное значение Price");
            }
        }

        DateTimeOffset? dateAndTime = null;
        while (true)
        {
            Console.Write("  DateAndTime = ");
            input = Console.ReadLine();
            if (input is null) throw new Exception("    Не удалось считать значение DateAndTime");
            else
            {
                if (string.IsNullOrEmpty(input)) break;
                if (DateTimeOffset.TryParse(input, out DateTimeOffset _dateAndTime))
                {
                    dateAndTime = _dateAndTime;
                    break;
                }
                Console.WriteLine("    Некорректное значение DateAndTime");
            }
        }

        Session? session = db.Sessions.Find(id);
        if (session is null) return [];

        bool changed = false;

        if (movieId is not null) session.MovieId = (int)movieId;
        if (price is not null) session.Price = (decimal)price;
        if (dateAndTime is not null) session.DateAndTime = (DateTimeOffset)dateAndTime;

        if (changed) db.SaveChanges();
        return [session];
    }

    public static IEnumerable<Session> Delete(CinemaContext db)
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

        Session? session = db.Sessions.Find(id);
        if (session is null) return [];

        db.Sessions.Remove(session);
        db.SaveChanges();
        return [session];
    }
}
