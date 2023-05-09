using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.IZm20Repository;
public interface IZm20Repository : IAsyncRepository<Zm20>, ISyncRepository<Zm20>
{
    Task<int> DeleteAll();

    List<Zm20> GetAll();

    List<Zm20> GetZm20s(string materialSKU);
}