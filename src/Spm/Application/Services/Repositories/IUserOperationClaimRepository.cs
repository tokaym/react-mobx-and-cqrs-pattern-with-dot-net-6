using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories;

public interface IUserOperationClaimRepository : IAsyncRepository<UserOperationClaim>, ISyncRepository<UserOperationClaim>
{
    Task<List<UserOperationClaim>> GetListByUserIdAsync(Guid id);
    Task<int> DeleteUserOperationClaimsByUserId(Guid id);
}