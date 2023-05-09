using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Dtos
{
    public class UserDto
    {
        public UserDto()
        {
            Operations = new List<OperationClaimDto>();
            OperationClaimIds = new List<string>();
        }
        public Guid Id { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmployeeNo { get; set; }
        public List<String> OperationClaimIds { get; set; }
        public List<OperationClaimDto> Operations { get; set; }
    }

    public class OperationClaimDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
    }
}
