using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Queries;

public class GetUserByIdQuery : IRequest<UserDto>
{
    public Guid Id { get; set; }

    public class GetUserByIdResponseHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly IUserOperationClaimRepository _userOperationClaimRepo;
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;

        public GetUserByIdResponseHandler(IUserRepository userRepository, UserBusinessRules userBusinessRules, IUserOperationClaimRepository userOperationClaimRepo,
        IOperationClaimRepository operationClaimRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _userBusinessRules = userBusinessRules;
            _userOperationClaimRepo = userOperationClaimRepo;
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
        }


        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.UserCanNotBeEmptyWhenSelected(request.Id);
            var user = await _userRepository.GetAsync(b => b.Id == request.Id);
            var result = _mapper.Map<UserDto>(user);

            var operations = await _operationClaimRepository.GetAllAsync();
            var userOperations = await _userOperationClaimRepo.GetListByUserIdAsync(user.Id);
            foreach (var operation in operations)
            {
                result.Operations.Add(new OperationClaimDto
                {
                    Id = operation.Id,
                    Name = operation.Name,
                    Selected = userOperations.Any(a => a.OperationClaimId == operation.Id)
                });
                if (userOperations.Any(a => a.OperationClaimId == operation.Id))
                    result.OperationClaimIds.Add(operation.Id.ToString());
            }
            return result;
        }
    }
}