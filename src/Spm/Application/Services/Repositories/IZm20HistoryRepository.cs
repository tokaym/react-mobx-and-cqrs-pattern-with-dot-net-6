using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.IZm20HistoryRepository
{

    public interface IZm20HistoryRepository : IAsyncRepository<Zm20History>, ISyncRepository<Zm20History>
    {
        Task<int> DeleteByDate(string date);
        Task<List<Zm20History>> GetListByMonth(DateTime Date);
        Task<List<Zm20History>> GetListAsync();
        DateTime GetLastUploadDate();
    }
}