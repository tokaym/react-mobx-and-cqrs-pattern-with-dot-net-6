using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.IRomaniaZm20HistoryRepository
{
    public interface IRomaniaZm20HistoryRepository : IAsyncRepository<RomaniaZm20History>, ISyncRepository<RomaniaZm20History>
    {
        Task<int> DeleteByDate(string date);
        Task<List<RomaniaZm20History>> GetListByMonth(DateTime Date);
        Task<List<RomaniaZm20History>> GetListAsync();
        DateTime GetLastUploadDate();
    }
}