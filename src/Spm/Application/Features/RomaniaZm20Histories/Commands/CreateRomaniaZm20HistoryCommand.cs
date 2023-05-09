using Application.Services.IRomaniaZm20HistoryRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ZM20Histories.Commands;

public class CreateRomaniaZm20HistoryCommand : IRequest<bool>
{
    public List<RomaniaZm20> RomaniaZm20s { get; set; }
    public DateTime ReportDate { get; set; }

    public class CreateRomaniaZm20HistoryCommandHandler : IRequestHandler<CreateRomaniaZm20HistoryCommand, bool>
    {
        private readonly IRomaniaZm20HistoryRepository _zm20HistoryRepository;
        private readonly IMapper _mapper;


        public CreateRomaniaZm20HistoryCommandHandler(
            IRomaniaZm20HistoryRepository zm20HistoryRepository,
            IMapper mapper
           )
        {
            _zm20HistoryRepository = zm20HistoryRepository;
            _mapper = mapper;
        }


        public async Task<bool> Handle(CreateRomaniaZm20HistoryCommand request, CancellationToken cancellationToken)
        {
            var zm20Histories = _mapper.Map<List<RomaniaZm20History>>(request.RomaniaZm20s);
            foreach (var item in zm20Histories)
            {
                item.CreatedTime = DateTime.Now;
                item.ReportDate = request.ReportDate;
                item.UserId = new Guid("A00F2551-380A-4585-4BF9-08DA688EFF3C");
            }
            bool result = await _zm20HistoryRepository.AddListAsync(zm20Histories) > 0 ? true : false;
            return result;
        }

    }

}

