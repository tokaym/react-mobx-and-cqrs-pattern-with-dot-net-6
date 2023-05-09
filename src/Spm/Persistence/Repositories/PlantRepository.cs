using Application.Services.IPlantRepository;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class PlantRepository : EfRepositoryBase<Plant, BaseDbContext>, IPlantRepository
    {
        public PlantRepository(BaseDbContext context) : base(context)
        {
            
        }
    }
}