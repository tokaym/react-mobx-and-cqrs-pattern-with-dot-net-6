using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class MaterialGroupRepository : EfRepositoryBase<MaterialGroup, BaseDbContext>, IMaterialGroupRepository
{
    public MaterialGroupRepository(BaseDbContext context) : base(context)
    {

    }
    public async Task<List<MaterialGroup>> GetAllAsync()
    {
        return await Context.MaterialGroups.ToListAsync();
    }

    public List<MaterialGroup> GetAll()
    {
        return Context.MaterialGroups.ToList();
    }
}