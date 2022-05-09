using System.ComponentModel;
using System.Linq.Expressions;
using ConsoleApp1.ConsoleMenus.Top.MovieMenu.Analyze.MovieOccupationDisplayMenu;
using ConsoleApp1.ConsoleMenus.Top.MovieMenu.Analyze.MovieYearErrorDisplayMenu;
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

    public List<MovieWithOccupation> BestMovieByOccupation();

    public PageInfo<User> GetPageUsers(PageInfo<User> pageInfo);
    public bool Rate(long userId, long movieId, int rating);
    public bool AddUser(User user);
    public List<Occupation> GetAllOccupations();
    List<MovieWithYearError> GetMovieDiff();
}