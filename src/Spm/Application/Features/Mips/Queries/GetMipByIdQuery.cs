using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Mips.Queries;
public class GetMipByIdQuery : IRequest<Mip>
{
    public Guid id { get; set; }
    public class GetMipByIdQueryHandler : IRequestHandler<GetMipByIdQuery, Mip>
    {
        private readonly IMipRepository _mipRepository;
        private readonly IMapper _mapper;


        // private readonly ICacheService _cacheService;
        //private readonly IMailService _mailService;


        public GetMipByIdQueryHandler(
            IMipRepository mipRepository,
            IMapper mapper)
        {
            _mipRepository = mipRepository;
            _mapper = mapper;
        }


        public async Task<Mip> Handle(GetMipByIdQuery request, CancellationToken cancellationToken)
        {
            return await _mipRepository.GetAsync(a => a.Id == request.id);
        }

    }

}