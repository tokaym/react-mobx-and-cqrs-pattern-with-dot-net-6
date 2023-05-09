using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IMipRepository : IAsyncRepository<Mip>, ISyncRepository<Mip>
    {
        Task<List<Mip>> GetAllAsync();
    }
}