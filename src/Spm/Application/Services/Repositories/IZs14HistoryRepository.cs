using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.IZs14HistoryRepository
{
    public interface IZs14HistoryRepository : IAsyncRepository<Zs14History>, ISyncRepository<Zs14History>
    {
         
    }
}