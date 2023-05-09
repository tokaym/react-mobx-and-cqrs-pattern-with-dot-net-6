using Application.Services.IRomaniaZm20Repository;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class RomaniaZm20Repository : EfRepositoryBase<RomaniaZm20, BaseDbContext>, IRomaniaZm20Repository
    {
        public RomaniaZm20Repository(BaseDbContext context) : base(context)
        {
            
        }

        public async Task<int> DeleteAll()
        {
            return await Context.ExecuteSqlQueryAsync("delete from SPM.dbo.RomaniaZm20s ");
        }

        public List<RomaniaZm20> GetRomaniaZm20s(string materialSKU)
        {
            return Context.RomaniaZm20s.Where(a=>a.MaterialNo == materialSKU).ToList();
        }

        public List<RomaniaZm20> GetAll()
        {
            return Context.RomaniaZm20s.ToList();
        }
    }
}