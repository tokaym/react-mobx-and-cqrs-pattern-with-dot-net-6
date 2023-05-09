using Application.Services.IZm20HistoryRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ZM20Histories.Commands;

public class CreateZm20HistoryCommand : IRequest<bool>
{
    public List<Zm20> Zm20s { get; set; }
    public DateTime ReportDate { get; set; }

    public class CreateZm20HistoryCommandHandler : IRequestHandler<CreateZm20HistoryCommand, bool>
    {
        private readonly IZm20HistoryRepository _zm20HistoryRepository;
        private readonly IMapper _mapper;


        public CreateZm20HistoryCommandHandler(
            IZm20HistoryRepository zm20HistoryRepository,
            IMapper mapper
           )
        {
            _zm20HistoryRepository = zm20HistoryRepository;
            _mapper = mapper;
        }


        public async Task<bool> Handle(CreateZm20HistoryCommand request, CancellationToken cancellationToken)
        {
            var zm20Histories = _mapper.Map<List<Zm20History>>(request.Zm20s);
            foreach (var item in zm20Histories)
            {
                item.MaterialSat = item.MaterialSKU + item.SatSasNo;
                item.CreatedTime = DateTime.Now;
                item.ReportDate = request.ReportDate;
                item.UserId = new Guid("A00F2551-380A-4585-4BF9-08DA688EFF3C");
            }
            bool result = await _zm20HistoryRepository.AddListAsync(zm20Histories) > 0 ? true : false;
            return result;
        }

    }

}

