using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> GetByEmployeeNo(string employeeNo)
        {
            var result = await this.userRepository.GetAsync(u => u.EmployeeNo == employeeNo);
            if (result is null)
            {
                throw new RepositoryException(Messages.UserDoesNotExist);
            }

            return result;
        }

        public async Task<User> GetByMail(string mail)
        {
            var result = await this.userRepository.GetAsync(u => u.Mail == mail);
            if (result is null)
            {
                throw new RepositoryException(Messages.UserDoesNotExist);
            }

            return result;
        }

        public Task<List<OperationClaim>> GetClaims(User user)
        {
            var result = Task.Run(() =>
            {
                var claims = this.userRepository.GetClaims(user);

                if (claims is null) throw new RepositoryException(Messages.UserDoesNotExist);

                return claims;
            });

            return result;

        }
    }
}
