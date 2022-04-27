using ConsoleApp1.FileAccessor.Database.Context;
using ConsoleApp1.MediaEntities;
using ConsoleApp1.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Repositories.IDataRepository;

public class GenreRepository : Repository<Genre>, IGenreRepository
{
    public GenreRepository(DbContext context) : base(context)
    {
    }
}