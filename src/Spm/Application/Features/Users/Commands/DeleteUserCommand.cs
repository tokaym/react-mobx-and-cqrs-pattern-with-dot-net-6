using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands
{
    public class DeleteUserCommand : IRequest<ReturnModel<User>>
    {
        public Guid Id { get; set; }


        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ReturnModel<User>>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;

            public DeleteUserCommandHandler(IUserRepository userRepository,
                IMapper mapper,
                UserBusinessRules userBusinessRules
                )
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<ReturnModel<User>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                ReturnModel<User> result = new();
                var deletedUser = await _userRepository.GetAsync(a => a.Id == request.Id);
                try
                {
                    deletedUser.Status = false;
                    User user = await _userRepository.UpdateAsync(deletedUser);
                    result.Data = user;
                    result.Message = "Kullanıcı başarıyla silindi.";
                    result.Status = ReturnTypeStatus.Success;
                }
                catch (System.Exception ex)
                {
                    result.Message = "Bir hata oluştu: " + ex.Message;
                    result.Status = ReturnTypeStatus.Error;
                }

                return result;
            }

        }

    }
}
