using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.ISettingRepository
{
    public interface ISettingRepository : IAsyncRepository<Setting>, ISyncRepository<Setting>
    {
         
    }
}