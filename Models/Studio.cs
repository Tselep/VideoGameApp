namespace VideoGameApp.Models;

public class Studio
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Country { get; set; }

    public List<Game> Games { get; set; } = new();
}