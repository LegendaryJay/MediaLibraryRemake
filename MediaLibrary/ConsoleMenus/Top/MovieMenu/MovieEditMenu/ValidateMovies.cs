using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu.MovieEditMenu;

public static class ValidateMovies
{
    public static bool ValidateTitle(string str)
    {
        if (str.Length <= 6) return false;

        var yearString = str.Substring(str.Length - 6);
        return yearString[0] == '(' && yearString[5] == ')' &&
               int.TryParse(yearString.AsSpan(1, 4), out var year);
    }

    public static bool ValidateYear(string str)
    {
        return ValidateYear(str, out var ignore);
    }


    public static bool ValidateYear(string str, out DateTime year)
    {
        if (int.TryParse(str.Trim(), out var tempYear))
        {
            var newYear = new DateTime(tempYear, 1, 1);
            if (newYear <= DateTime.Now && newYear > new DateTime(1887, 1, 1))
            {
                year = newYear;
                return true;
            }
        }

        year = DateTime.MinValue;
        return false;
    }

    public static bool ValidateGenres(IEnumerable<Genre> mg)
    {
        return mg.Any();
    }

    public static bool ValidateMovie(Movie movie)
    {
        return ValidateTitle(movie.Title) && ValidateGenres(movie.Genres) &&
               ValidateYear(movie.ReleaseDate.Year.ToString());
    }
}