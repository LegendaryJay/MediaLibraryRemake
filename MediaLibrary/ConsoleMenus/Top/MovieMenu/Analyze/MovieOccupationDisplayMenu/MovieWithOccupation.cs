using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu.Analyze.MovieOccupationDisplayMenu;

public class MovieWithOccupation
{
    public Occupation Occupation { get; set; }
    public Movie Movie { get; set; }
    public double Rating { get; set; }

    public string ToPrettyString()
    {
        var movieTitle = Movie is not null ? Movie.Title : "[None]";
        return $"{Occupation.Name, -15} | Top Movie: {movieTitle, -60} | {Rating: #.#} / 5";
    }

}