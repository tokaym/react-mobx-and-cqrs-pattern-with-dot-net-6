using Application.Features.Users.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Security.Entities;
using Core.Utilities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries
{
    public class GetUserListQuery : IRequest<UserListModel>
    {
        public PageRequest? PageRequest { get; set; }
        public OrderByRequest? OrderByRequest { get; set; }
        public string Search { get; set; }

        public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, UserListModel>
        {
            IUserRepository _userRepository;
            IMapper _mapper;

            public GetUserListQueryHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<UserListModel> Handle(GetUserListQuery request, CancellationToken cancellationToken)
            {
                var users = await _userRepository.GetListAsync(
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize,
                    predicate: (a => a.Status == true && (a.EmployeeNo.Contains(request.Search) ||
                                                          a.Name.Contains(request.Search) || 
                                                          a.Surname.Contains(request.Search))),
                    orderBy: OrderingLibrary.GetOrderBy<User>(request.OrderByRequest.ColumnName, request.OrderByRequest.Type)
                    );

                var mappedUsers = _mapper.Map<UserListModel>(users);
                return mappedUsers;
            }

        }
    }
}