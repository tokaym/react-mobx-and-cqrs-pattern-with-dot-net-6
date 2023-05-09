using Application.Services.IRomaniaZm20HistoryRepository;
using AutoMapper;
using MediatR;

namespace Application.Features.ZM20Histories.Commands;

public class DeleteRomaniaZm20HistoryCommand : IRequest<bool>
{
    public DateTime ReportDate { get; set; }
    public class DeleteZm20CommandHandler : IRequestHandler<DeleteRomaniaZm20HistoryCommand, bool>
    {
        private readonly IRomaniaZm20HistoryRepository _romaniaZm20HistoryRepository;
        private readonly IMapper _mapper;

        // private readonly ICacheService _cacheService;
        //private readonly IMailService _mailService;


        public DeleteZm20CommandHandler(
            IRomaniaZm20HistoryRepository romaniaZm20HistoryRepository,
            IMapper mapper)
        {
            _romaniaZm20HistoryRepository = romaniaZm20HistoryRepository;
            _mapper = mapper;
        }


        public async Task<bool> Handle(DeleteRomaniaZm20HistoryCommand request, CancellationToken cancellationToken)
        {
            bool result = await _romaniaZm20HistoryRepository.DeleteByDate(request.ReportDate.ToString("yyyy-MM-dd")) >= 0 ? true : false;
            return result;
        }

    }

}

