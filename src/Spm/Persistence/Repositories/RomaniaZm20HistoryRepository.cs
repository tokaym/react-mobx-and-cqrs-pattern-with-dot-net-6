using Application.Services.IRomaniaZm20HistoryRepository;
using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class RomaniaZm20HistoryRepository : EfRepositoryBase<RomaniaZm20History, BaseDbContext>, IRomaniaZm20HistoryRepository
    {
        public RomaniaZm20HistoryRepository(BaseDbContext context) : base(context)
        {
            
        }

         public async Task<int> DeleteByDate(string date)
        {
            return await Context.ExecuteSqlQueryAsync("delete from SPM.dbo.RomaniaZm20Histories where ReportDate = '" + date + "' ");
        }

        public async Task<List<RomaniaZm20History>> GetListByMonth(DateTime Date)
        {
            return await Context.RomaniaZm20Histories.Where(a => a.ReportDate.Month == Date.Month).ToListAsync();
        }

        public async Task<List<RomaniaZm20History>> GetListAsync()
        {
            return await Context.RomaniaZm20Histories.ToListAsync();
        }

        public DateTime GetLastUploadDate()
        {
            return Context.RomaniaZm20Histories.OrderByDescending(a=>a.ReportDate).FirstOrDefault().ReportDate;
        }
    }
}