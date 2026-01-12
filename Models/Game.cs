using System;
using System.ComponentModel.DataAnnotations;

namespace VideoGameApp.Models;

public class Game
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Range(0, 999)]
    public decimal Price { get; set; }

    public DateTime? ReleaseDate { get; set; }

    [Required]
    public int GenreId { get; set; }
    public Genre? Genre { get; set; }

    [Required]
    public int StudioId { get; set; }
    public Studio? Studio { get; set; }
}