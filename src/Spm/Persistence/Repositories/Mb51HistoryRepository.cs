using Application.Services.IMb51HistoryRepository;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class Mb51HistoryRepository : EfRepositoryBase<Mb51History, BaseDbContext>, IMb51HistoryRepository
    {
        public Mb51HistoryRepository(BaseDbContext context) : base(context)
        {
            
        }
    }
}