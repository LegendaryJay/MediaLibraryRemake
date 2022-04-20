using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using ConsoleApp1.FileAccessor.Database.Context;
using ConsoleApp1.MediaEntities;
using Microsoft.EntityFrameworkCore;

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

    public List<Movie> GetAllMovies()
    {
        using (var db = new MovieContext())
        {
            return db.Movies
                .Include(c => c.MovieGenres)
                .ThenInclude(x => x.Genre)
                .Include(x => x.UserMovies)
                .ToList();
        }
    }

    public bool AddMovie(Movie movie)
    {
        using var db = new MovieContext();
        db.Add(movie);
        return db.SaveChanges() > 0;
    }

    public bool UpdateMovie(Movie? movie)
    {
        if (movie is null) return false;
        using var db = new MovieContext();

        var original = db.Movies
            .Include(x => x.MovieGenres)
            .FirstOrDefault(x => movie.Id == x.Id);
        if (original == null) return false;
        original.Title = movie.Title;
        original.ReleaseDate = movie.ReleaseDate;
        
        
        //this is stupid but I've worked for about 10 hours today just trying to solve this problem.
        //Why did this work? Why didnt other methods work???
        original.MovieGenres =
            movie.MovieGenres
                .Where(
                    x => movie.MovieGenres
                        .Select(
                            y => y.Genre.Id
                        )
                        .Contains(x.Genre.Id)).ToList();

        foreach (var genre in movie.MovieGenres) original.MovieGenres.Add(genre);
        return db.SaveChanges() > -1;
    }

    public bool DeleteMovie(long id)
    {
        using var db = new MovieContext();
        var movie = new Movie() {Id = id};
        db.Movies.Remove(movie);
        return db.SaveChanges() > 0;
    }


    public List<Movie> FilterMovieByTitle(string str)
    {
        using var db = new MovieContext();
        return db.Movies
            .Include(c => c.MovieGenres)
            .ThenInclude(x => x.Genre)
            .Include(x => x.UserMovies)
            .Where(x => x.Title.Contains(str))
            .ToList();
    }

    public List<Movie> FilterMovieByGenre(string str)
    {
        using var db = new MovieContext();
        var allowedVariables = db.Genres.Where(x => x.Name.Contains(str));
        if (allowedVariables.Any())
        {
            return db.Movies
                .Include(c => c.MovieGenres)
                .ThenInclude(x => x.Genre)
                .Include(x => x.UserMovies)
                .Where(
                    x => x.MovieGenres.Any(
                        y => allowedVariables.Contains(y.Genre)
                    )
                )
                .ToList();
        }

        return new List<Movie>();
    }

    public List<Movie> FilterMovieByReleaseDate(int year)
    {
        using var db = new MovieContext();
        return db.Movies
            .Include(c => c.MovieGenres)
            .ThenInclude(x => x.Genre)
            .Include(x => x.UserMovies)
            .Where(x => Equals(x.ReleaseDate, year))
            .ToList();
    }

    public List<Movie> FilterMovieByRating(int rating)
    {
        using var db = new MovieContext();
        return db.Movies
            .Include(c => c.MovieGenres)
            .ThenInclude(x => x.Genre)
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
            .ThenInclude(x => x.Genre)
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