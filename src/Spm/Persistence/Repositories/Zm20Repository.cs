using Application.Services.IZm20Repository;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class Zm20Repository : EfRepositoryBase<Zm20, BaseDbContext>, IZm20Repository
    {
        public Zm20Repository(BaseDbContext context) : base(context)
        {
        }


        public async Task<int> DeleteAll()
        {
            return await Context.ExecuteSqlQueryAsync("delete from SPM.dbo.Zm20s ");
        }

        public List<Zm20> GetZm20s(string materialSKU)
        {
            return Context.Zm20s.Where(a=>a.MaterialSKU == materialSKU).ToList();
        }

        public List<Zm20> GetAll()
        {
            return Context.Zm20s.ToList();
        }
    }
}