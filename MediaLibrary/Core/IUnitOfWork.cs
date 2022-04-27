using ConsoleApp1.Repositories.IDataRepository;
using MediaLibrary.Core.Repositories;

namespace ConsoleApp1.Repositories.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    public IGenreRepository Genres { get; }
    public IMovieRepository Movies { get; }
    public IOccupationRepository Occupations { get; }
    public IUserMovieRepository UserMovies { get; }
    public IUserRepository Users { get; }

    public int Complete();
}