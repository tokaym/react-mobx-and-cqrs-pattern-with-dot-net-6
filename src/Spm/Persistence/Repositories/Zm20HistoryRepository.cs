using Application.Services.IZm20HistoryRepository;
using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class Zm20HistoryRepository : EfRepositoryBase<Zm20History, BaseDbContext>, IZm20HistoryRepository
    {

        public Zm20HistoryRepository(BaseDbContext context) : base(context)
        {

        }
        public async Task<int> DeleteByDate(string date)
        {
            return await Context.ExecuteSqlQueryAsync("delete from SPM.dbo.Zm20Histories where ReportDate = '" + date + "' ");
        }

        public async Task<List<Zm20History>> GetListByMonth(DateTime Date)
        {
            return await Context.Zm20Histories.Where(a => a.ReportDate.Month == Date.Month).ToListAsync();
        }

        public async Task<List<Zm20History>> GetListAsync()
        {
            return await Context.Zm20Histories.ToListAsync();
        }

        public DateTime GetLastUploadDate()
        {
            return Context.Zm20Histories.OrderByDescending(a=>a.ReportDate).FirstOrDefault().ReportDate;
        }
    }
}