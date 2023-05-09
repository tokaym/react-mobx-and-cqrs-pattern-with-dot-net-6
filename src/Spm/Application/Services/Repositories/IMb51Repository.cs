using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.IMb51Repository
{
    public interface IMb51Repository : IAsyncRepository<Mb51>, ISyncRepository<Mb51>
    {
        Task<int> DeleteAll();
        List<Mb51> GetAll(int type);
        Task<List<Mb51>> GetAllAsync(int type);
        Task<int> DeleteByDate(string date);
    }
}