using Application.Services.IMb51Repository;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Application;
using Domain.Entities;
using MediatR;

namespace Application.Features.MB51s.Commands;

public class DeleteMb51Command : IRequest<bool>
{
    public class DeleteMb51CommandHandler : IRequestHandler<DeleteMb51Command, bool>
    {
        private readonly IMb51Repository _mb51Repository;
        private readonly IMapper _mapper;


        // private readonly ICacheService _cacheService;
        //private readonly IMailService _mailService;


        public DeleteMb51CommandHandler(
            IMb51Repository mb51Repository,
            IMapper mapper)
        {
            _mb51Repository = mb51Repository;
            _mapper = mapper;
        }


        public async Task<bool> Handle(DeleteMb51Command request, CancellationToken cancellationToken)
        {
            bool result = await _mb51Repository.DeleteAll() >= 0 ? true : false;
            return result;
        }

    }

}

