using Application.Services.IZs14HistoryRepository;
using Application.Services.IZs14Repository;
using AutoMapper;
using Core.Application;
using MediatR;

namespace Application.Features.Zs14Histories.Commands;

public class DeleteZs14HistoryCommand : IRequest<Result<Unit>>
{
    public class DeleteZs14HistoryCommandHandler: IRequestHandler<DeleteZs14HistoryCommand, Result<Unit>>
    {
        private readonly IZs14HistoryRepository _zs14HistoryRepository;
        private readonly IMapper _mapper;


        public DeleteZs14HistoryCommandHandler(
            IZs14HistoryRepository zs14HistoryRepository,
            IMapper mapper
        )
        {
            _zs14HistoryRepository = zs14HistoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(DeleteZs14HistoryCommand request, CancellationToken cancellationToken)
        {
            //TODO Delete process
            return Result<Unit>.Success(Unit.Value);
        }
    }
}