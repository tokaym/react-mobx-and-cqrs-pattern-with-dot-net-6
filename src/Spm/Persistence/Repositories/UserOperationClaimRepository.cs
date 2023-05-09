using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class UserOperationClaimRepository : EfRepositoryBase<UserOperationClaim, BaseDbContext>, IUserOperationClaimRepository
{
    public UserOperationClaimRepository(BaseDbContext context) : base(context)
    {
    }
    public async Task<List<UserOperationClaim>> GetListByUserIdAsync(Guid id)
    {
        return await Context.UserOperationClaims.Where(a => a.UserId == id).ToListAsync();
    }
    public async Task<int> DeleteUserOperationClaimsByUserId(Guid id)
    {
        return await Context.ExecuteSqlQueryAsync("delete from SPM.dbo.UserOperationClaims where UserId = '" + id.ToString() + "' ");
    }
}