using Application.Services.IZm20Repository;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Application;
using Domain.Entities;
using MediatR;

namespace Application.Features.ZM20S.Commands;

public class DeleteZm20Command : IRequest<bool>
{
    public class DeleteZm20CommandHandler : IRequestHandler<DeleteZm20Command, bool>
    {
        private readonly IZm20Repository _zm20Repository;
        private readonly IMapper _mapper;


        // private readonly ICacheService _cacheService;
        //private readonly IMailService _mailService;


        public DeleteZm20CommandHandler(
            IZm20Repository zm20Repository,
            IMapper mapper)
        {
            _zm20Repository = zm20Repository;
            _mapper = mapper;
        }


        public async Task<bool> Handle(DeleteZm20Command request, CancellationToken cancellationToken)
        {
            bool result = await _zm20Repository.DeleteAll() >= 0 ? true : false;
            return result;
        }

    }

}

