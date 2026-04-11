using CinemaApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var conString = builder.Configuration.GetConnectionString("CinemaAPI") ??
     throw new InvalidOperationException("Connection string 'CinemaAPI' not found.");
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(conString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

    if (!context.Movies.Any())
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
            }
        ];
        context.Movies.AddRange(movies);
        context.SaveChanges();

        Session[] sessions = [
            new Session
            {
                MovieId = movies[0].Id,
                Price = 400,
                DateAndTime = new DateTimeOffset(2026,04,25, 12,30,00, new TimeSpan(03,00,00)),
            },
            new Session
            {
                MovieId = movies[1].Id,
                Price = 500,
                DateAndTime = new DateTimeOffset(2026,04,25, 18,30,00, new TimeSpan(03,00,00)),
            },
            new Session
            {
                MovieId = movies[2].Id,
                Price = 400,
                DateAndTime = new DateTimeOffset(2026,04,26, 12,30,00, new TimeSpan(03,00,00)),
            },
            new Session
            {
                MovieId = movies[3].Id,
                Price = 500,
                DateAndTime = new DateTimeOffset(2026,04,26, 18,30,00, new TimeSpan(03, 00, 00)),
            }
        ];
        context.Sessions.AddRange(sessions);
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
