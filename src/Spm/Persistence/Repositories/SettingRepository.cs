using Application.Services.IMb51HistoryRepository;
using Application.Services.ISettingRepository;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class SettingRepository : EfRepositoryBase<Setting, BaseDbContext>, ISettingRepository
    {
        public SettingRepository(BaseDbContext context) : base(context)
        {
            
        }
    }
}