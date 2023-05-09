using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.MainReportRepositories;

public interface IMainReportRepository : IAsyncRepository<MainReport>, ISyncRepository<MainReport>
{
    DateTime GetMaxDate(string plantCode);
    List<string> GetReportDates(string plantCode);
    bool DataCheck(string plantCode);
    List<MainReport> GetByReportDate(DateTime reportDate, string plantCode);
    List<MainReport> GetByReportDate(DateTime startDate, DateTime endDate, string plantCode);
    Task<int> DeleteByReportDate(DateTime reportDate, string plantCode);
    List<DateTime> GetReportDatesDateTime(string plantCode);
}