using System.Linq.Expressions;
using ConsoleApp1.FileAccessor.RepositoryContext;
using ConsoleApp1.MediaEntities;

namespace MediaLibrary.Core.Repositories;

public interface IMovieRepository : IRepository<Movie>
{
    int GetMovieCount();
    IEnumerable<Movie> GetDetailedMovies(int pageIndex, int pageSize);

    IEnumerable<Movie> GetSortedDetailedMovies(int pageIndex, int pageSize, Expression<Func<Movie, object>> orderBy,
        bool isAsc);
}