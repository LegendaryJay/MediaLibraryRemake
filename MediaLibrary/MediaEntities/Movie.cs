namespace ConsoleApp1.MediaEntities;

public class Movie
{
    public long Id { get; set; }
    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }

    public virtual ICollection<MovieGenre> MovieGenres { get; set; }
    public virtual ICollection<UserMovie> UserMovies { get; set; }

    public string ToShortString()
    {
        return $"{Id}: {Title}";
    }
    public string ToPrettyString()
    {
        return $" - Movie {Id}: {Title}" +
               $"\n\tReleased {ReleaseDate:yyyy}" +
               $"\n\tGenres: {string.Join(" - ", MovieGenres.Select(X => X.Genre.Name))}" +
               $"\n\tRated {UserMovies.Select(x => x.Rating).Average():0.00} / 5";
    }
}

//Looks like there are 2 main groups: 
//Movie: Title, release date, List(Genre), List(Ratings, DateRated)
//User: Id, age, gender, zipcode, Occupations

//Users rate
