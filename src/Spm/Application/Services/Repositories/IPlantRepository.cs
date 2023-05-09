using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.IPlantRepository
{
    public interface IPlantRepository : IAsyncRepository<Plant>, ISyncRepository<Plant>
    {
         
    }
}