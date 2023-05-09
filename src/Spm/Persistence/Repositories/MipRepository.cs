using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class MipRepository : EfRepositoryBase<Mip, BaseDbContext>, IMipRepository
    {

        public MipRepository(BaseDbContext context) : base(context)
        {

        }

        public async Task<List<Mip>> GetAllAsync()
        {
            return Context.Mips.ToList();
        }
    }
}