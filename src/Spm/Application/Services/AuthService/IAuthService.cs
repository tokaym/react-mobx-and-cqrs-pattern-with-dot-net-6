using Core.Ldap;
using Core.Security.Entities;
using Core.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthService
{
   public interface IAuthService
   {
       Task<bool> UserExistsByMail(string email);
       Task<bool> UserExistsByEmployeeNo(string employeeNo);
       Task<AccessToken> CreateAccessToken(User user);
   }
}
