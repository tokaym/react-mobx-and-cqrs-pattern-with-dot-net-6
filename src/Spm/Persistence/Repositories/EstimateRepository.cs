using Application.Services.IEstimateRepository;
using Application.Services.IZs14HistoryRepository;
using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class EstimateRepository : EfRepositoryBase<Estimate, BaseDbContext>, IEstimateRepository
    {

        public EstimateRepository(BaseDbContext context) : base(context)
        {

        }

        public async Task<List<Estimate>> GetEstimatesByDate(int year, int month)
        {
            return await Context.Estimates.Where(a=>a.Year == year && a.Month == month).ToListAsync();
        }
    }
}