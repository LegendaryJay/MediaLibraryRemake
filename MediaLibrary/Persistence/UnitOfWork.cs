using ConsoleApp1.FileAccessor.Database.Context;
using ConsoleApp1.Repositories.IDataRepository;
using ConsoleApp1.Repositories.UnitOfWork;
using MediaLibrary.Core.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MovieContext _context;

    public UnitOfWork(MovieContext context)
    {
        _context = context;
        Genres = new GenreRepository(_context);
        Movies = new MovieRepository(_context);
        Occupations = new OccupationRepository(_context);
        UserMovies = new UserMovieRepository(_context);
        Users = new UserRepository(_context);
    }

    public IGenreRepository Genres { get; private set; }
    public IMovieRepository Movies { get; private set; }
    public IOccupationRepository Occupations { get; private set; }
    public IUserMovieRepository UserMovies { get; private set; }
    public IUserRepository Users { get; private set; }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}