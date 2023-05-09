using Application.Services.IZm20HistoryRepository;
using Application.Services.IZm20Repository;
using AutoMapper;
using Core.Application;
using MediatR;

namespace Application.Features.ZM20Histories.Commands;

public class DeleteZm20HistoryCommand : IRequest<bool>
{
    public DateTime ReportDate { get; set; }
    public class DeleteZm20CommandHandler : IRequestHandler<DeleteZm20HistoryCommand, bool>
    {
        private readonly IZm20HistoryRepository _zm20HistoryRepository;
        private readonly IMapper _mapper;

        // private readonly ICacheService _cacheService;
        //private readonly IMailService _mailService;


        public DeleteZm20CommandHandler(
            IZm20HistoryRepository zm20HistoryRepository,
            IMapper mapper)
        {
            _zm20HistoryRepository = zm20HistoryRepository;
            _mapper = mapper;
        }


        public async Task<bool> Handle(DeleteZm20HistoryCommand request, CancellationToken cancellationToken)
        {
            bool result = await _zm20HistoryRepository.DeleteByDate(request.ReportDate.ToString("yyyy-MM-dd")) >= 0 ? true : false;
            return result;
        }

    }

}

