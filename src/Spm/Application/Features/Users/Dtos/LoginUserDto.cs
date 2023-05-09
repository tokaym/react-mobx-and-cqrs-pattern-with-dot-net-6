using Core.Security.Entities;
using Core.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Dtos
{
    public class LoginUserDto
    {        
        public string EmployeeNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public AccessToken AccessToken { get; set; }
        public bool isSuccess { get; set; }
        public string Message { get; set; }
    }
}
