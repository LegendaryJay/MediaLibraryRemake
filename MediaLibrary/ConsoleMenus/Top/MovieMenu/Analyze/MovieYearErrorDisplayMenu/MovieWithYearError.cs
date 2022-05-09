using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu.Analyze.MovieYearErrorDisplayMenu;

public class MovieWithYearError
{
    public Movie Movie { get; }
    public int TitleYear { get; }
    public int YearDIff { get; }
    public bool isDiff { get; }

    public MovieWithYearError(Movie movie)
    {
        Movie = movie;
        TitleYear = GetTitleYear(Movie.Title);
        YearDIff = Math.Abs(TitleYear - Movie.ReleaseDate.Year);
        isDiff = YearDIff != 0 || TitleYear < 1800 || Movie.ReleaseDate.Year < 1800;
    }

    public static int GetTitleYear(string str)
    {
        str = str.Trim();
        if (str.Length > -6)
        {
            str = str[^6..];
            if (str[0] == '(' && str[5] == ')')
            {
                str = str.Substring(1, 4);
                if (int.TryParse(str, out var tryYear))
                {
                    return tryYear;
                }
            }
        }

        return -1;
    }
}