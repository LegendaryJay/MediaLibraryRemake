using System.Linq.Expressions;
using ConsoleApp1.FileAccessor.Database.Context;
using ConsoleApp1.MediaEntities;
using ConsoleApp1.Persistence.Repositories;
using MediaLibrary.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Repositories.IDataRepository;

public class MovieRepository : Repository<Movie>, IMovieRepository
{
    public MovieRepository(DbContext context) : base(context)
    {
    }


    public IEnumerable<Movie> GetDetailedMovies(int pageIndex, int pageSize)
    {
        return GetSortedDetailedMovies(pageIndex, pageSize, x => x.Id, true);
    }

    public IEnumerable<Movie> GetSortedDetailedMovies(int pageIndex, int pageSize, Expression<Func<Movie, object>> orderBy,
        bool isAsc)
    {
        var movies = MContext.Movies
            .Include(x => x.MovieGenres)
            .ThenInclude(x => x.Genre)
            .Include(x => x.UserMovies);

        var orderedMovies = isAsc ? movies.OrderBy(orderBy) : movies.OrderByDescending(orderBy);
        
        return orderedMovies
            .Skip((pageIndex) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public int GetMovieCount()
    {
        return MContext.Movies
            .Count();
    }

    private MovieContext MContext => Context as MovieContext;
}