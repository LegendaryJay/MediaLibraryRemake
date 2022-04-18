using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.FileAccessor;

public interface IFileIo
{
    public List<Movie> GetAllMovies();
    public bool AddMovie(Movie movie);
    public bool UpdateMovie(Movie movie);
    public bool DeleteMovie(long id);
    public List<Movie> FilterMovieByTitle(string str);
    public List<Movie> FilterMovieByGenre(string str);
    public List<Movie> FilterMovieByReleaseDate(int year);
    public List<Movie> FilterMovieByRating(int rating);
    public List<Movie> BestMovieByOccupation();

    public List<Genre> GetAllGenres();

    public List<User> GetAllUsers();
    public bool AddRating(long userId, int rating);
    public bool AddUser(User user);
}