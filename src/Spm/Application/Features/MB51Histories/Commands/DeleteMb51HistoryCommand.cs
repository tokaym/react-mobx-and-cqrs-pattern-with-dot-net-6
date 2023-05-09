using Application.Services.IMb51Repository;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Application;
using Domain.Entities;
using MediatR;

namespace Application.Features.MB51Histories.Commands;

public class DeleteMb51HistoryCommand : IRequest<bool>
{
    public DateTime ReportDate { get; set; }
    public class DeleteMb51HistoryCommandHandler : IRequestHandler<DeleteMb51HistoryCommand, bool>
    {
        private readonly IMb51Repository _mb51Repository;
        private readonly IMapper _mapper;


        // private readonly ICacheService _cacheService;
        //private readonly IMailService _mailService;


        public DeleteMb51HistoryCommandHandler(
            IMb51Repository mb51Repository,
            IMapper mapper)
        {
            _mb51Repository = mb51Repository;
            _mapper = mapper;
        }


        public async Task<bool> Handle(DeleteMb51HistoryCommand request, CancellationToken cancellationToken)
        {
            bool result = await _mb51Repository.DeleteByDate(request.ReportDate.ToString("yyyy-MM-dd")) >= 0 ? true : false;
            return result;
        }

    }

}

