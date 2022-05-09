using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.ConsoleMenus.Top.MovieMenu.Analyze.MovieOccupationDisplayMenu;
using ConsoleApp1.ConsoleMenus.Top.MovieMenu.Analyze.MovieYearErrorDisplayMenu;
using ConsoleApp1.FileAccessor.Database.Context;
using ConsoleApp1.MediaEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace ConsoleApp1.FileAccessor;

public class DatabaseIo : IFileIo
{
    public PageInfo<Movie> GetPageMovies(PageInfo<Movie> pageInfo, Func<Movie, object> orderBy,
        ListSortDirection direction, Func<Movie, bool> where)
    {
        using var db = new MovieContext();

        pageInfo.TotalItemCount = db.Movies.Where(where).Count();
        pageInfo.Items = db.Movies
            .Include(x => x.UserMovies)
            .Include(x => x.MovieGenres)
            .ThenInclude(x => x.Genre)
            .Where(where)
            .OrderByDynamic(orderBy, direction)
            .Select(p => p)
            .Skip(pageInfo.PageIndex * pageInfo.PageLength)
            .Take(pageInfo.PageLength)
            .ToList();
        return pageInfo;
    }

    public bool AddMovie(Movie movie)
    {
        if (movie is null) return false;
        using var db = new MovieContext();

        var original = new Movie();

        original.Id = movie.Id;
        original.Title = movie.Title;
        original.ReleaseDate = movie.ReleaseDate;

        db.Add(original);

        var isSuccessful = db.SaveChanges() > -1;

        movie.Id = original.Id;
        original.MovieGenres = new List<MovieGenres>();
        foreach (var genre in movie.MovieGenres)
        {
            original.MovieGenres.Add(
                new MovieGenres
                {
                    MovieId = original.Id,
                    GenreId = genre.GenreId,
                }
            );
        }

        return db.SaveChanges() > -1 && isSuccessful;
    }

    public bool UpdateMovie(Movie? movie)
    {
        if (movie is null) return false;
        using var db = new MovieContext();

        var original = db.Movies
            .Include(x => x.MovieGenres)
            .Include(x => x.UserMovies)
            .FirstOrDefault(x => x.Id == movie.Id);
        if (original is null) return false;

        original.Id = movie.Id;
        original.Title = movie.Title;
        original.ReleaseDate = movie.ReleaseDate;
        original.MovieGenres.Clear();

        foreach (var genre in movie.MovieGenres)
        {
            original.MovieGenres.Add(
                new MovieGenres
                {
                    MovieId = movie.Id,
                    GenreId = genre.GenreId,
                }
            );
        }

        return db.SaveChanges() > -1;
    }

    public bool DeleteMovie(long id)
    {
        using var db = new MovieContext();
        var movie = new Movie {Id = id};
        db.Movies.Remove(movie);
        return db.SaveChanges() > 0;
    }

    public PageInfo<User> GetPageUsers(PageInfo<User> pageInfo)
    {
        using var db = new MovieContext();

        pageInfo.TotalItemCount = db.Users.Count();
        pageInfo.Items = db.Users
            .Include(x => x.UserMovies)
            .Include(x => x.Occupation)
            .Skip(pageInfo.PageIndex * pageInfo.PageLength)
            .Take(pageInfo.PageLength)
            .ToList();
        return pageInfo;
    }

    public List<MovieWithOccupation> BestMovieByOccupation()
    {
        using var db = new MovieContext();

        var results = new List<MovieWithOccupation>();

        var userMovies = db.UserMovies
            .Include(x => x.User)
            .Include(x => x.Movie)
            .Select(x => new
            {
                Rating = x.Rating,
                Movie = x.Movie,
                Occupation = x.User.Occupation
            }).ToList()
            .GroupBy(x => x.Occupation.Id);

        foreach (var ums in userMovies)
        {
            var thing = ums.Select(x => new
                {
                    x.Movie,
                    x.Rating
                })
                .GroupBy(x => x.Movie)
                .Select(x => new
                {
                    Movie = x.Key,
                    Rating = x.Average(y => y.Rating)
                })
                .OrderByDescending(x => x.Rating)
                .First();


            var result = new MovieWithOccupation
            {
                Movie = thing.Movie,
                Rating = thing.Rating,
                Occupation = ums.Select(x => x.Occupation).First()
            };
            results.Add(result);
        }

        results.Sort((x, y) =>
            string.Compare(x.Occupation.Name, y.Occupation.Name, StringComparison.Ordinal));

        return results;
    }

    public List<Genre> GetAllGenres()
    {
        using var db = new MovieContext();
        return db.Genres.ToList();
    }

    public bool Rate(long userId, long movieId, int rating)
    {
        using var db = new MovieContext();
        var userMovie = db.UserMovies.FirstOrDefault(x => x.MovieId == movieId && x.UserId == userId);
        if (rating == 0)
        {
            if (userMovie is not null)
            {
                db.Remove(userMovie);
            }
            else
            {
                return false;
            }
        }
        else if (userMovie is null)
        {
            db.Add(new UserMovie
                {
                    UserId = userId,
                    MovieId = movieId,
                    Rating = rating,
                    RatedAt = DateTime.Today
                }
            );
        }
        else
        {
            userMovie.Rating = rating;
            userMovie.RatedAt = DateTime.Now;
            db.Update(userMovie);
        }

        return db.SaveChanges() > -1;
    }

    public bool AddUser(User user)
    {
        using var db = new MovieContext();
        var occ = db.Occupations.Find(user.Occupation.Id);
        user.Occupation = occ;
        db.Users
            .Add(user);
        return db.SaveChanges() > -1;
    }

    public List<Occupation> GetAllOccupations()
    {
        using var db = new MovieContext();
        return db.Occupations.ToList();
    }

    public List<MovieWithYearError> GetMovieDiff()
    {
        using var db = new MovieContext();
        return db.Movies
            .ToList()
            .Select(x => new MovieWithYearError(x))
            .Where(x => x.isDiff)
            .OrderByDescending(x => x.YearDIff)
            .ToList();
    }
}