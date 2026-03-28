USE Cinema;

SELECT DateAndTime FROM [Sessions]
WHERE MovieId = 1
ORDER BY DateAndTime ASC;

DELETE FROM [Sessions]
WHERE Id = 2;

UPDATE [Sessions]
SET Price = 450
WHERE Id = 1;

SELECT MovieId, COUNT(*) AS MovieCount
FROM [Sessions]
GROUP BY MovieId;

SELECT Movies.[Name] AS MovieName,
       AgeRatings.[Name] AS AgeRating
FROM Movies
LEFT JOIN AgeRatings ON Movies.AgeRatingId = [AgeRatings].Id;

SELECT Movies.[Name] AS MovieName,
       Genres.[Name] AS Genre
FROM MovieGenres
RIGHT JOIN Movies ON MovieGenres.MovieId = Movies.Id
LEFT JOIN Genres ON MovieGenres.GenreId = Genres.Id;

SELECT Theaters.[Address] AS Theater,
       Halls.Number AS HallNumber
FROM Halls
INNER JOIN Theaters ON Halls.TheaterId = Theaters.Id;
