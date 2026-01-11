namespace VideoGameApp.Models;

public class Game
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public DateTime? ReleaseDate { get; set; }
    public decimal Price { get; set; }

    public int GenreId { get; set; }
    public Genre? Genre { get; set; }

    public int StudioId { get; set; }
    public Studio? Studio { get; set; }
}