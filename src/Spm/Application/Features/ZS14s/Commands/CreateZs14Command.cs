using Domain.Entities;
using MediatR;
using Application.Services.IZs14Repository;
using AutoMapper;

namespace Application.Features.Zs14s.Commands;

public class CreateZs14Command : IRequest<bool>
{
    public List<Zs14>? Zs14s { get; set; }

    public class CreateZs14CommandHandler : IRequestHandler<CreateZs14Command, bool>
    {
        private readonly IZs14Repository _zs14Repository;
        private readonly IMapper _mapper;

        public CreateZs14CommandHandler(
            IZs14Repository zs14Repository,
            IMapper mapper)
        {
            _zs14Repository = zs14Repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateZs14Command request, CancellationToken cancellationToken)
        {
            bool result = await _zs14Repository.AddListAsync(request.Zs14s) > 0 ? true :false;
            return result;
        }
    }
}