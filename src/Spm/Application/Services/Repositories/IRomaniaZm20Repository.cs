using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.IRomaniaZm20Repository
{
    public interface IRomaniaZm20Repository : IAsyncRepository<RomaniaZm20>, ISyncRepository<RomaniaZm20>
    {
        Task<int> DeleteAll();

        List<RomaniaZm20> GetAll();

        List<RomaniaZm20> GetRomaniaZm20s(string materialSKU);
    }
}