using CinemaApp.Models;

namespace CinemaApp;

public class DbInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = serviceProvider.GetRequiredService<ApplicationContext>();

        if (context.Movies.Any()) return;

        InitializeSessions(context, InitializeMovies(context));
    }

    private static Movie[] InitializeMovies(ApplicationContext context)
    {
        Movie[] movies = [
            new Movie
            {
                Title = "Интерстеллар",
                ReleaseDate = new DateOnly(2014, 11, 07),
                AgeRating = "12+",
                Duration = 169,
                Description = "Будущее Земли омрачено катастрофами, голодом и засухами. Единственный способ обеспечить выживание человечества — межзвёздные перелёты. Недавно обнаруженная кротовая нора на дальних окраинах Солнечной системы позволяет команде астронавтов отправиться туда, где ещё не ступала нога человека, — к планете, условия на которой, возможно, пригодны для поддержания жизни.",
                PosterURL = "https://m.media-amazon.com/images/M/MV5BYzdjMDAxZGItMjI2My00ODA1LTlkNzItOWFjMDU5ZDJlYWY3XkEyXkFqcGc@._V1_SX300.jpg",
                Genres = "Приключение, Драма, Научная фантастика",
            },
            new Movie
            {
                Title = "Титаник",
                ReleaseDate = new DateOnly(1997, 12, 19),
                AgeRating = "12+",
                Duration = 194,
                Description = "84 года спустя столетняя женщина по имени Роза Дьюитт Бьюкейтер рассказывает историю своей жизни своей внучке Лиззи Калверт, а также Броку Ловетту, Льюису Бодайну, Бобби Бьюэллу и Анатолию Михайловичу на борту судна «Келдыш». Действие начинается 10 апреля 1912 года на корабле под названием «Титаник», куда юная Роза поднимается вместе с пассажирами первого класса, своей матерью Рут Дьюитт Бьюкейтер и женихом Каледоном Хокли. Тем временем бродячий художник Джек Доусон и его лучший друг Фабрицио Де Росси выигрывают в карты билеты в третий класс на этот же лайнер. Роза рассказывает всю историю от самого отплытия и до гибели «Титаника» в его первом и последнем рейсе 15 апреля 1912 года в 2 часа 20 минут ночи.",
                PosterURL = "https://m.media-amazon.com/images/M/MV5BYzYyN2FiZmUtYWYzMy00MzViLWJkZTMtOGY1ZjgzNWMwN2YxXkEyXkFqcGc@._V1_SX300.jpg",
                Genres = "Драма, Мелодрама",
            },
            new Movie
            {
                Title = "Шрек",
                ReleaseDate = new DateOnly(2001, 05, 18),
                AgeRating = "6+",
                Duration = 90,
                Description = "Когда зелёный огр по имени Шрек обнаруживает, что его болото «затопило» всевозможными сказочными существами по вине коварного лорда Фаркуада, Шрек отправляется в путь вместе с весьма говорливым ослом, чтобы «убедить» Фаркуада вернуть ему родное болото. Вместо этого заключается сделка. Фаркуад, мечтающий стать королём, посылает Шрека вызволить принцессу Фиону, которая дожидается своего истинного возлюбленного в башне, охраняемой огнедышащим драконом. Однако на обратном пути с Фионой становится очевидно, что не только безобразный огр Шрек начинает влюбляться в прекрасную принцессу, но и сама Фиона скрывает огромную тайну.",
                PosterURL = "https://m.media-amazon.com/images/M/MV5BN2FkMTRkNTUtYTI0NC00ZjI4LWI5MzUtMDFmOGY0NmU2OGY1XkEyXkFqcGc@._V1_SX300.jpg",
                Genres = "Анимационный, Приключение, Комедия",
            },
            new Movie
            {
                Title = "Мегамозг",
                ReleaseDate = new DateOnly(2010, 11, 05),
                AgeRating = "6+",
                Duration = 95,
                Description = "После того как суперзлодей Мегамозг (Феррелл) уничтожает своего заклятого врага-героя Метро-Мэна (Питт), ему становится скучно — ведь больше не с кем сражаться. Тогда он создаёт нового противника, Титана (Хилл), который, вместо того чтобы использовать свои силы во благо, решает уничтожить мир. И теперь Мегамозгу впервые в жизни предстоит стать спасителем положения.",
                PosterURL = "https://m.media-amazon.com/images/M/MV5BMTAzMzI0NTMzNDBeQTJeQWpwZ15BbWU3MDM3NTAyOTM@._V1_SX300.jpg",
                Genres = "Анимационный, Экшн, Приключение",
            },
            new Movie
            {
                Title = "Чебурашка",
                ReleaseDate = new DateOnly(2023, 01, 01),
                AgeRating = "6+",
                Duration = 113,
                Description = "На небольшой приморский городок обрушивается дождь из апельсинов, а вместе с фруктами с неба падает неизвестный науке мохнатый непоседливый зверёк. Одержимое апельсинами животное оказывается в домике нелюдимого старика-садовника Геннадия, который из вредности решает оставить его жить у себя, так как местная богачка жаждет заполучить необычного зверя для своей избалованной внучки. Также эта коварная женщина, владелица кондитерской фабрики, пытается выведать секрет шоколада у хозяйки маленького магазинчика — дочери Геннадия, много лет обиженной на отца.",
                PosterURL = "https://avatars.mds.yandex.net/get-kinopoisk-image/4303601/a9f04abf-a08f-4633-84c3-a818d75a4daa/1920x",
                Genres = "Семейный, Комедия, Фэнтези",
            },
            new Movie
            {
                Title = "Последний богатырь",
                ReleaseDate = new DateOnly(2017, 10, 26),
                AgeRating = "12+",
                Duration = 114,
                Description = "Победитель 5-го сезона «Битвы магов» белый маг Святозар, а на самом деле — обычный парень Иван, привычно пудрит мозги телезрителям и доверчивым клиентам, готовым поверить во что угодно. Однажды, спасаясь бегством от крепких охранников мужа очередной одураченной им домохозяйки, парень внезапно переносится из Москвы в сказочную страну Белогорье. А встреченный там старец объявляет Ивану, что тот — сын Ильи Муромца, и только он может их спасти. Так наш «белый маг» оказывается в эпицентре противостояния волшебных сил добра и зла, вот только сам парень уверен, что он тут совершенно ни при чём, и всё это — какая-то нелепая ошибка.",
                PosterURL = "https://avatars.mds.yandex.net/get-kinopoisk-image/1900788/b31c80d6-865d-41b4-83fe-2984811dabff/1920x",
                Genres = "Фэнтези, Комедия, Приключения, Семейный",
            },
        ];
        context.Movies.AddRange(movies);
        context.SaveChanges();
        return movies;
    }

    private static void InitializeSessions(ApplicationContext context, Movie[] movies)
    {
        Session[] sessions = [
            new Session
            {
                MovieId = movies[0].Id,
                Movie = movies[0],
                Price = 400,
                DateAndTime = new DateTimeOffset(2026,04,25, 12,30,00, new TimeSpan(03,00,00)),
            },
            new Session
            {
                MovieId = movies[1].Id,
                Movie = movies[1],
                Price = 500,
                DateAndTime = new DateTimeOffset(2026,04,25, 18,30,00, new TimeSpan(03,00,00)),
            },
            new Session
            {
                MovieId = movies[2].Id,
                Movie = movies[2],
                Price = 400,
                DateAndTime = new DateTimeOffset(2026,04,26, 12,30,00, new TimeSpan(03,00,00)),
            },
            new Session
            {
                MovieId = movies[3].Id,
                Movie = movies[3],
                Price = 500,
                DateAndTime = new DateTimeOffset(2026,04,26, 18,30,00, new TimeSpan(03,00,00)),
            },
            new Session
            {
                MovieId = movies[4].Id,
                Movie = movies[4],
                Price = 400,
                DateAndTime = new DateTimeOffset(2026,04,27, 12,30,00, new TimeSpan(03,00,00)),
            },
            new Session
            {
                MovieId = movies[4].Id,
                Movie = movies[4],
                Price = 500,
                DateAndTime = new DateTimeOffset(2026,04,27, 18,30,00, new TimeSpan(03,00,00)),
            },
            new Session
            {
                MovieId = movies[5].Id,
                Movie = movies[5],
                Price = 400,
                DateAndTime = new DateTimeOffset(2026,04,28, 12,30,00, new TimeSpan(03,00,00)),
            },
            new Session
            {
                MovieId = movies[5].Id,
                Movie = movies[5],
                Price = 500,
                DateAndTime = new DateTimeOffset(2026,04,28, 18,30,00, new TimeSpan(03,00,00)),
            },
        ];
        context.Sessions.AddRange(sessions);
        context.SaveChanges();
    }
}
