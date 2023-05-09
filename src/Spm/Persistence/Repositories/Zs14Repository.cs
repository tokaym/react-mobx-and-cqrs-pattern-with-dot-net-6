using Application.Services.IZs14Repository;
using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class Zs14Repository : EfRepositoryBase<Zs14, BaseDbContext>, IZs14Repository
    {
        public Zs14Repository(BaseDbContext context) : base(context)
        {

        }

        public async Task<int> DeleteAll()
        {
            return await Context.ExecuteSqlQueryAsync("delete from SPM.dbo.Zs14s ");
        }

        public Zs14 GetZs14(string materialSKU)
        {
            return Context.Zs14s.FirstOrDefault(a=>a.MaterialSKU == materialSKU);
        }
    }
}