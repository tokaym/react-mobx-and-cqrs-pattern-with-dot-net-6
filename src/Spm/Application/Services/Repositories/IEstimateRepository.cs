using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.IEstimateRepository
{
    public interface IEstimateRepository : IAsyncRepository<Estimate>, ISyncRepository<Estimate>
    {
        Task<List<Estimate>> GetEstimatesByDate(int year, int month);
    }
}