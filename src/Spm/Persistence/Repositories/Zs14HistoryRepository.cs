using Application.Services.IZs14HistoryRepository;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class Zs14HistoryRepository : EfRepositoryBase<Zs14History, BaseDbContext>, IZs14HistoryRepository
    {

        public Zs14HistoryRepository(BaseDbContext context) : base(context)
        {

        }

    }
}