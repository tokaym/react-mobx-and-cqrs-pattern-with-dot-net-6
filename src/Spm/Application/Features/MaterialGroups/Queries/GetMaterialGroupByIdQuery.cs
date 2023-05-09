using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.MaterialGroups.Queries;
public class GetMaterialGroupByIdQuery : IRequest<MaterialGroup>
{
    public Guid id { get; set; }
    public class GetMaterialGroupByIdQueryHandler : IRequestHandler<GetMaterialGroupByIdQuery, MaterialGroup>
    {
        private readonly IMaterialGroupRepository _materialGroupRepository;
        private readonly IMapper _mapper;


        // private readonly ICacheService _cacheService;
        //private readonly IMailService _mailService;


        public GetMaterialGroupByIdQueryHandler(
            IMaterialGroupRepository materialGroupRepository,
            IMapper mapper)
        {
            _materialGroupRepository = materialGroupRepository;
            _mapper = mapper;
        }


        public async Task<MaterialGroup> Handle(GetMaterialGroupByIdQuery request, CancellationToken cancellationToken)
        {
            return await _materialGroupRepository.GetAsync(a => a.Id == request.id);
        }

    }

}