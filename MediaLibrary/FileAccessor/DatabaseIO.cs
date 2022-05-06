using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using ConsoleApp1.FileAccessor.Database.Context;
using ConsoleApp1.MediaEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace ConsoleApp1.FileAccessor;

public class DatabaseIo : IFileIo
{
    // public bool AddItem<T>(T item)
    // {

    // }
    //
    // public bool UpdateItem<T>(long id, T item)
    // {
    //     using var db = new MovieContext();
    //     var dbMovie = db.Movies.Find(id);
    //     if (dbMovie is not null)
    //     {
    //         db.Attach(item);
    //         db.Entry(item).State = EntityState.Modified;
    //     }
    // }
    //
    // public List<T> GetAllMovies<T>()
    // {
    //     switch
    //

    //
    // public bool SetItem<T>(T item)
    // {
    //     using var db = new MovieContext();
    //     var dbMovie = db.Movies.Find(item.Id);
    //     if (dbMovie is not null)
    //     {
    //         db.Attach(movie);
    //         db.Entry(movie).State = EntityState.Modified;
    //     }
    //     else
    //     {
    //     }
    //
    //     return db.SaveChanges() > 0;
    // }
    //

    //


    public PageInfo<Movie> GetPageMovies(PageInfo<Movie> pageInfo, Func<Movie, object> orderBy,
        ListSortDirection direction, Func<Movie, bool> where)
    {
        using var db = new MovieContext();

        pageInfo.TotalItemCount = db.Movies.Where(where).Count();
        pageInfo.Items = db.Movies
            .Include(x => x.MovieGenres)
            .ThenInclude(x => x.Genre)
            .Include(x => x.UserMovies)
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
            .Include(x => x.MovieGenres).FirstOrDefault(x => x.Id == movie.Id);
        if (original is null) return false;

        original.Id = movie.Id;
        original.Title = movie.Title;
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


    public List<Movie> FilterMovieByTitle(string str)
    {
        using var db = new MovieContext();
        return db.Movies
            .Include(x => x.MovieGenres)
            .ThenInclude(x => x.Genre)
            .Include(x => x.UserMovies)
            .Where(x => x.Title.Contains(str))
            .ToList();
    }

    public List<Movie> FilterMovieByGenre(List<long> indexList)
    {
        using var db = new MovieContext();
        return db.Movies
            .Include(x => x.MovieGenres)
            .ThenInclude(x => x.Genre)
            .Include(x => x.UserMovies)
            .Where(
                m => m.MovieGenres.Select(mg => mg.GenreId).Any(indexList.Contains)
            )
            .ToList();

        return new List<Movie>();
    }

    public List<Movie> FilterMovieByReleaseDate(int year)
    {
        using var db = new MovieContext();
        return db.Movies
            .Include(c => c.MovieGenres)
            .ThenInclude(c => c.Genre)
            .Include(x => x.UserMovies)
            .Where(x => Equals(x.ReleaseDate, year))
            .ToList();
    }

    public List<Movie> FilterMovieByRating(int rating)
    {
        using var db = new MovieContext();
        return db.Movies
            .Include(c => c.MovieGenres)
            .ThenInclude(c => c.Genre)
            .Include(x => x.UserMovies)
            .Where(x => x.UserMovies.Average(y => y.Rating) > rating)
            .OrderByDescending(x => x.UserMovies.Average(y => y.Rating))
            .ThenBy(x => x.Title)
            .ToList();
    }


    public List<Movie> BestMovieByOccupation()
    {
        using var db = new MovieContext();
        // return db.Users
        //     .Include(x => x.UserMovies)
        //     .ThenInclude(x => x.Movie)
        //     .ThenInclude(x => x.MovieGenres)
        //     .ThenInclude(x => x.Genre)
        //     .GroupBy(x => x.Occupation)
        //     .SelectMany( x => x.))
        //     .O(x => x.UserMovies.Average(y => y.Rating))
        //     .SelectMany(x => x.Take(1))
        return db.Movies
            .Include(c => c.MovieGenres)
            .ThenInclude(c => c.Genre)
            .Include(x => x.UserMovies)
            .ThenInclude(x => x.User)
            .GroupBy(x => x.UserMovies.Select(y => y.User.Occupation))
            .SelectMany(x => x.Take(1))
            .OrderByDescending(x => x.UserMovies.Average(y => y.Rating))
            .ThenBy(x => x.Title)
            .ToList();
    }

    public List<Genre> GetAllGenres()
    {
        using var db = new MovieContext();
        return db.Genres.ToList();
    }

    public List<User> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public bool AddRating(long userId, int rating)
    {
        throw new NotImplementedException();
    }

    public bool AddUser(User user)
    {
        throw new NotImplementedException();
    }
}