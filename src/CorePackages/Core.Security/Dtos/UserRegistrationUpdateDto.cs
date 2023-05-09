using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Dtos
{
    public class UserRegistrationUpdateDto
    {
        public UserRegistrationUpdateDto()
        {
            OperationClaimIds = new List<string>();
        }
        public Guid Id { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> OperationClaimIds { get; set; }
    }
}
