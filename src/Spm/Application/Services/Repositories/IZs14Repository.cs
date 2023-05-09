using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.IZs14Repository
{
    public interface IZs14Repository : IAsyncRepository<Zs14>, ISyncRepository<Zs14>
    {
         Task<int> DeleteAll();
         Zs14 GetZs14(string materialSKU);
    }
}