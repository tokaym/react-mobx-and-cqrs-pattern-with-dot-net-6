using Application.Services.IMb51Repository;
using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class Mb51Repository : EfRepositoryBase<Mb51, BaseDbContext>, IMb51Repository
    {
        public Mb51Repository(BaseDbContext context) : base(context)
        {

        }

        public async Task<int> DeleteAll()
        {
            return await Context.ExecuteSqlQueryAsync("delete from SPM.dbo.Mb51s");
        }


        public async Task<int> DeleteByDate(string date)
        {
            return await Context.ExecuteSqlQueryAsync("delete from SPM.dbo.Mb51Histories where ReportDate = '" + date + "' ");
        }

        public List<Mb51> GetAll(int type)
        {
            return Context.Mb51s.Where(a => a.Dpyr == type || a.ITU == type.ToString()).ToList();
        }

        public async Task<List<Mb51>> GetAllAsync(int type)
        {
            return await Context.Mb51s.Where(a => a.Dpyr == type|| a.ITU == type.ToString()).ToListAsync();
        }

    }
}