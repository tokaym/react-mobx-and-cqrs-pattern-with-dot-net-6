using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Utilities;
using Domain.Entities;
using MediatR;


namespace Application.Features.Mips.Queries;
public class GetMipListQuery : IRequest<IPaginate<Mip>>
{
    public PageRequest? PageRequest { get; set; }
    public OrderByRequest? OrderByRequest { get; set; }
    public string Search { get; set; }
    public class GetMipListQueryHandler : IRequestHandler<GetMipListQuery, IPaginate<Mip>>
    {
        private readonly IMipRepository _mipRepository;
        private readonly IMapper _mapper;

        public GetMipListQueryHandler(
            IMipRepository mipRepository,
            IMapper mapper)
        {
            _mipRepository = mipRepository;
            _mapper = mapper;
        }


        public async Task<IPaginate<Mip>> Handle(GetMipListQuery request, CancellationToken cancellationToken)
        {
            var result = await _mipRepository.GetListAsync(
                        index: request.PageRequest.Page,
                        size: request.PageRequest.PageSize,
                        predicate: (a => (a.CD.Contains(request.Search) ||
                                         a.Code.Contains(request.Search)
                                         )),
                        orderBy: OrderingLibrary.GetOrderBy<Mip>(request.OrderByRequest.ColumnName, request.OrderByRequest.Type)
                        );

            return result;
        }

    }
   
}