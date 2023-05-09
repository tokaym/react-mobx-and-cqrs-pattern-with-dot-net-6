using Application.Services.IRomaniaZm20Repository;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Application;
using Domain.Entities;
using MediatR;

namespace Application.Features.RomaniaZm20s.Commands;

public class DeleteRomaniaZm20Command : IRequest<bool>
{
    public class DeleteRomaniaZm20CommandHandler : IRequestHandler<DeleteRomaniaZm20Command, bool>
    {
        private readonly IRomaniaZm20Repository _romaniaZm20Repository;
        private readonly IMapper _mapper;


        // private readonly ICacheService _cacheService;
        //private readonly IMailService _mailService;


        public DeleteRomaniaZm20CommandHandler(
            IRomaniaZm20Repository romaniaZm20Repository,
            IMapper mapper)
        {
            _romaniaZm20Repository = romaniaZm20Repository;
            _mapper = mapper;
        }


        public async Task<bool> Handle(DeleteRomaniaZm20Command request, CancellationToken cancellationToken)
        {
            bool result = await _romaniaZm20Repository.DeleteAll() >= 0 ? true : false;
            return result;
        }

    }

}

