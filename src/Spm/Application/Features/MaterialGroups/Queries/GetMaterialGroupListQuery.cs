using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Utilities;
using Domain.Entities;
using MediatR;


namespace Application.Features.MaterialGroups.Queries;
public class GetMaterialGroupListQuery : IRequest<IPaginate<MaterialGroup>>
{
    public PageRequest? PageRequest { get; set; }
    public OrderByRequest? OrderByRequest { get; set; }
    public string Search { get; set; }
    public class GetMaterialGroupListQueryHandler : IRequestHandler<GetMaterialGroupListQuery, IPaginate<MaterialGroup>>
    {
        private readonly IMaterialGroupRepository _materialGroupRepository;
        private readonly IMapper _mapper;

        public GetMaterialGroupListQueryHandler(
            IMaterialGroupRepository materialGroupRepository,
            IMapper mapper)
        {
            _materialGroupRepository = materialGroupRepository;
            _mapper = mapper;
        }


        public async Task<IPaginate<MaterialGroup>> Handle(GetMaterialGroupListQuery request, CancellationToken cancellationToken)
        {
            var result = await _materialGroupRepository.GetListAsync(
                        index: request.PageRequest.Page,
                        size: request.PageRequest.PageSize,
                        predicate: (a => (a.GroupName.Contains(request.Search) ||
                                         a.MaterialSKU.Contains(request.Search)
                                         )),
                        orderBy: OrderingLibrary.GetOrderBy<MaterialGroup>(request.OrderByRequest.ColumnName, request.OrderByRequest.Type)
                        );

            return result;
        }

    }
   
}