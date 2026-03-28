USE Cinema;

DROP TABLE IF EXISTS Tickets;
DROP TABLE IF EXISTS [Sessions];
DROP TABLE IF EXISTS Halls;
DROP TABLE IF EXISTS MovieGenres;
DROP TABLE IF EXISTS Genres;
DROP TABLE IF EXISTS Movies;
DROP TABLE IF EXISTS Theaters;
DROP TABLE IF EXISTS AgeRatings;


CREATE TABLE AgeRatings (
    Id INT PRIMARY KEY IDENTITY,
    [Name] NVARCHAR(16) NOT NULL,
);

CREATE TABLE Theaters (
    Id INT PRIMARY KEY IDENTITY,
    [Address] NVARCHAR(64) NOT NULL,
);

CREATE TABLE Movies (
    Id INT PRIMARY KEY IDENTITY,
    [Name] NVARCHAR(64) NOT NULL,
    [Description] NVARCHAR(128) NOT NULL,
    AgeRatingId INT FOREIGN KEY REFERENCES AgeRatings(Id) NOT NULL,
    ReleaseDate DATE NOT NULL,
);

CREATE TABLE Genres (
    Id INT PRIMARY KEY IDENTITY,
    [Name] NVARCHAR(32) NOT NULL,
);

CREATE TABLE MovieGenres (
    MovieId INT FOREIGN KEY REFERENCES Movies(Id),
    GenreId INT FOREIGN KEY REFERENCES Genres(Id),
    PRIMARY KEY (MovieId, GenreId),
);

CREATE TABLE Halls (
    Id INT PRIMARY KEY IDENTITY,
    TheaterId INT FOREIGN KEY REFERENCES Theaters(Id) NOT NULL,
    [Number] SMALLINT NOT NULL,
);

CREATE TABLE [Sessions] (
    Id INT PRIMARY KEY IDENTITY,
    MovieId INT FOREIGN KEY REFERENCES Movies(Id) NOT NULL,
    Price DECIMAL(6, 2) NOT NULL,
    HallId INT FOREIGN KEY REFERENCES Halls(Id) NOT NULL,
    DateAndTime DATETIMEOFFSET NOT NULL,
);

CREATE TABLE Tickets (
    Id UNIQUEIDENTIFIER DEFAULT NEWID() UNIQUE,
    SessionId INT FOREIGN KEY REFERENCES [Sessions](Id) NOT NULL,
    [Row] SMALLINT,
    Seat SMALLINT,
    IsRefunded BIT NOT NULL,
    PRIMARY KEY (Id, [Row], Seat),
);

CREATE UNIQUE INDEX IX_Ticket_Id ON Tickets(Id);


INSERT INTO AgeRatings ([Name])
VALUES ('0+'), ('6+'), ('12+'), ('16+'), ('18+')

INSERT INTO Theaters ([Address])
VALUES (N'Ленина 1')

INSERT INTO Movies ([Name], [Description], AgeRatingId, ReleaseDate)
VALUES (N'Фильм1', '123', 1, '2012-05-06'),
       (N'Фильм2', '123', 3, '2014-12-15'),
       (N'Фильм3', '123', 4, '2018-08-16')

INSERT INTO Genres ([Name])
VALUES (N'Драма'), (N'Комедия'), (N'Триллер'), (N'Боевик')

INSERT INTO MovieGenres (MovieId, GenreId)
VALUES (1, 2),
       (2, 1), (2, 3),
       (3, 3), (3, 4)

INSERT INTO Halls (TheaterId, [Number])
VALUES (1, 1), (1, 2), (1, 3)

INSERT INTO [Sessions] (MovieId, Price, HallId, DateAndTime)
VALUES (1, 435, 1, '2026-04-26 13:15:00 +03:00'),
       (2, 475, 1, '2026-04-27 13:30:00 +03:00'),
       (1, 435, 1, '2026-04-26 16:00:00 +03:00'),
       (2, 475, 1, '2026-04-27 16:15:00 +03:00'),
       (1, 435, 1, '2026-04-26 19:45:00 +03:00'),
       (2, 475, 1, '2026-04-27 20:00:00 +03:00')

INSERT INTO Tickets (SessionId, [Row], Seat, IsRefunded)
VALUES (1, 8, 15, 0),
       (1, 8, 16, 0)
