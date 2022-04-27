using ConsoleApp1.MediaEntities;
using ConsoleApp1.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Repositories.IDataRepository;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {
    }
}