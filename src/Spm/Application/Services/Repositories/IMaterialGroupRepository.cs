using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IMaterialGroupRepository : IAsyncRepository<MaterialGroup>, ISyncRepository<MaterialGroup>
{
    Task<List<MaterialGroup>> GetAllAsync();

    List<MaterialGroup> GetAll();
}
