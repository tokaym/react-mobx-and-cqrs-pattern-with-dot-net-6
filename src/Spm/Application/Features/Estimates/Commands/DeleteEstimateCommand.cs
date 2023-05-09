using Application.Services.IZm20Repository;
using AutoMapper;
using MediatR;

namespace Application.Features.Estimates.Commands;

public class DeleteEstimateCommand : IRequest<bool>
{
    public class DeleteEstimateCommandHandler : IRequestHandler<DeleteEstimateCommand, bool>
    {
        private readonly IZm20Repository _zm20Repository;
        private readonly IMapper _mapper;


        // private readonly ICacheService _cacheService;
        //private readonly IMailService _mailService;


        public DeleteEstimateCommandHandler(
            IZm20Repository zm20Repository,
            IMapper mapper)
        {
            _zm20Repository = zm20Repository;
            _mapper = mapper;
        }


        public async Task<bool> Handle(DeleteEstimateCommand request, CancellationToken cancellationToken)
        {
            bool result = await _zm20Repository.DeleteAll() >= 0 ? true : false;
            return result;
        }

    }

}

