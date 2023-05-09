using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Utilities;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Commands;

public class UpdateUserCommand : IRequest<ReturnModel<UpdateUserDto>>
{
    public UserRegistrationUpdateDto UserRegistrationUpdateDto { get; set; }

    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, ReturnModel<UpdateUserDto>>
    {
        private IUserRepository _userRepository;
        private IUserOperationClaimRepository _userOperationClaimRepo;
        private IMapper _mapper;
        private UserBusinessRules _userBusinessRules;


        public UpdateUserHandler(UserBusinessRules userBusinessRules, IUserRepository userRepository, IMapper mapper, IUserOperationClaimRepository userOperationClaimRepo)
        {
            _userBusinessRules = userBusinessRules;
            _userOperationClaimRepo = userOperationClaimRepo;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ReturnModel<UpdateUserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            ReturnModel<UpdateUserDto> result = new();
            try
            {
                var userToUpdate = await _userRepository.GetAsync(x => x.Id == request.UserRegistrationUpdateDto.Id);

                if (userToUpdate == null) throw new BusinessException(Messages.UserDoesNotExist);

                var mapperUser = _mapper.Map<UserRegistrationUpdateDto, User>(request.UserRegistrationUpdateDto, userToUpdate);
                var updatedUser = await _userRepository.UpdateAsync(mapperUser);

                if (await _userOperationClaimRepo.DeleteUserOperationClaimsByUserId(userToUpdate.Id) >= 0)
                {
                    foreach (var operationClaimId in request.UserRegistrationUpdateDto.OperationClaimIds)
                    {
                        UserOperationClaim op = new UserOperationClaim
                        {
                            OperationClaimId = new Guid(operationClaimId),
                            UserId = updatedUser.Id
                        };
                        await _userOperationClaimRepo.AddAsync(op);
                    }
                }

                result.Data = _mapper.Map<UpdateUserDto>(updatedUser);
                result.Message = "Kullanıcı başarıyla güncellendi.";
                result.Status = ReturnTypeStatus.Success;
            }
            catch (System.Exception ex)
            {

                result.Message = ex.Message;
                result.Status = ReturnTypeStatus.Error;
            }

            return result;
        }
    }

}