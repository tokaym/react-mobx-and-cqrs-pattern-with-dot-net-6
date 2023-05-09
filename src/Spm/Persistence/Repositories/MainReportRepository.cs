using Application.Services.MainReportRepositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class MainReportRepository : EfRepositoryBase<MainReport, BaseDbContext>, IMainReportRepository
{
    public MainReportRepository(BaseDbContext context) : base(context)
    {
    }

    public bool DataCheck(string plantCode)
    {
        return Context.MainReports.Include(a => a.Plant).Where(a => a.Plant.Code == plantCode).Any();
    }

    public List<MainReport> GetByReportDate(DateTime reportDate, string plantCode)
    {
        return Context.MainReports.Where(a => a.ReportDate == reportDate).Include(a => a.Plant).Where(a => a.Plant.Code == plantCode).ToList();
    }

    public List<string> GetReportDates(string plantCode)
    {
        return Context.MainReports.Include(a => a.Plant).Where(a => a.Plant.Code == plantCode).GroupBy(a => new { a.ReportDate }).OrderByDescending(o => o.First().ReportDate)
                        .Select(r => r.First().ReportDate.ToString()).ToList();
    }
    public List<DateTime> GetReportDatesDateTime(string plantCode)
    {
        return Context.MainReports.Include(a => a.Plant).Where(a => a.Plant.Code == plantCode).GroupBy(a => new { a.ReportDate }).OrderByDescending(o => o.First().ReportDate)
                        .Select(r => r.First().ReportDate).ToList();
    }

    public DateTime GetMaxDate(string plantCode)
    {
        return Context.MainReports.Include(a => a.Plant).Where(a => a.Plant.Code == plantCode).OrderByDescending(i => i.ReportDate).First().ReportDate;
    }


    public List<MainReport> GetByReportDate(DateTime startDate, DateTime endDate, string plantCode)
    {
        return Context.MainReports.Where(a => a.ReportDate <= endDate && a.ReportDate >= startDate).Include(a => a.Plant).Where(a => a.Plant.Code == plantCode).ToList();
    }

    public async Task<int> DeleteByReportDate(DateTime reportDate, string plantCode)
    {
        string plantId = Context.Plants.FirstOrDefault(a => a.Code == plantCode).Id.ToString();
        string query = "delete from SPM.dbo.MainReports where ReportDate = CONVERT(datetime, '" + reportDate.ToString("yyyy-MM-dd") + "') and PlantId = '" + plantId + "'";
        return await Context.ExecuteSqlQueryAsync(query);
    }
}