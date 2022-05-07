using System.ComponentModel;
using System.Linq.Expressions;
using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.FileAccessor;

public interface IFileIo
{
    public PageInfo<Movie> GetPageMovies(PageInfo<Movie> pageIndex, Func<Movie, object> orderBy,
        ListSortDirection direction, Func<Movie, bool> where);
    public bool AddMovie(Movie movie);
    public bool UpdateMovie(Movie movie);
    public bool DeleteMovie(long id);

    public List<Genre> GetAllGenres();

    public Dictionary<string, Movie> BestMovieByOccupation();

    public PageInfo<User> GetPageUsers(PageInfo<User> pageInfo);
    public bool AddRating(long userId, int rating);
    public bool AddUser(User user);
}