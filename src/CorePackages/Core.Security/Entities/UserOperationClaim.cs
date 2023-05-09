using System;
using System.Collections.Generic;
using System.Text;
using Core.Persistence;
using Core.Persistence.Repositories;

namespace Core.Security.Entities
{
    public class UserOperationClaim : Entity
    {
        public Guid UserId { get; set; }
        public Guid OperationClaimId { get; set; }
        public User User { get; set; }
        public OperationClaim OperationClaim { get; set; }

    }
}
