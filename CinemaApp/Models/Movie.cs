using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Models;

public class Movie
{
    public int Id { get; set; }
    [MaxLength(128)]
    public string Title { get; set; } = null!;
    public DateOnly ReleaseDate { get; set; }
    [MaxLength(32)]
    public string AgeRating { get; set; } = null!;
    public byte Duration { get; set; }
    [MaxLength(1024)]
    public string Description { get; set; } = null!;
    [Unicode(false)]
    [MaxLength(2048)]
    public string PosterURL { get; set; } = null!;
    [MaxLength(128)]
    public string Genres { get; set; } = null!;
}
