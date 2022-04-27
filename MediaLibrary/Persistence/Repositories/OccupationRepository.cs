using ConsoleApp1.MediaEntities;
using ConsoleApp1.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Repositories.IDataRepository;

public class OccupationRepository : Repository<Occupation>, IOccupationRepository
{
    public OccupationRepository(DbContext context) : base(context)
    {
    }
}