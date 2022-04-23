using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.MediaEntities;

public class Movie
{
    [Key] public long Id { get; set; }

    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }

    public virtual ICollection<MovieGenres> MovieGenres { get; set; }
    public virtual ICollection<UserMovie> UserMovies { get; set; }

    private string GetRatingString()
    {
        var ratingSource = UserMovies;
        var averageString = (UserMovies is null || UserMovies.Count == 0)
            ? "?"
            : $"{Math.Round(ratingSource.Average(x => x.Rating), 1), -3}";

        return averageString;
    }

    public string ToShortString()
    {
        return $"{Id, -5} {Title,-50}[{GetRatingString()} / 5]";
    }

    public string ToPrettyString()
    {
        return $" - Movie {Id}: {Title}" +
               $"\n\tReleased {ReleaseDate:yyyy}" +
               $"\n\tGenres: {string.Join(" - ", MovieGenres.Select(g => g.Genre.Name))}" +
               $"\n\tRated {GetRatingString()} / 5";
    }
}

//Looks like there are 2 main groups: 
//Movie: Title, release date, List(Genre), List(Ratings, DateRated)
//User: Id, age, gender, zipcode, Occupations

//Users rate