using Application.Services.IZs14Repository;
using AutoMapper;
using Core.Application;
using MediatR;

namespace Application.Features.Zs14s.Commands;

public class DeleteZs14Command : IRequest<bool>
{
    public class DeleteZs14CommandHandler : IRequestHandler<DeleteZs14Command, bool>
    {
        private readonly IZs14Repository _zs14Repository;
        private readonly IMapper _mapper;


        public DeleteZs14CommandHandler(
            IZs14Repository zs14Repository,
            IMapper mapper
        )
        {
            _zs14Repository = zs14Repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteZs14Command request, CancellationToken cancellationToken)
        {
            bool result = await _zs14Repository.DeleteAll() >= 0 ? true : false;

            return result;
        }
    }
}