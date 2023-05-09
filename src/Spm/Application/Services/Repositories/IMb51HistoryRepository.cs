using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.IMb51HistoryRepository
{
    public interface IMb51HistoryRepository : IAsyncRepository<Mb51History>, ISyncRepository<Mb51History>
    {
        
    }
}