using ConsoleApp1.MediaEntities;
using ConsoleApp1.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Repositories.IDataRepository;

public class UserMovieRepository : Repository<UserMovie>, IUserMovieRepository
{
    public UserMovieRepository(DbContext context) : base(context)
    {
    }
}