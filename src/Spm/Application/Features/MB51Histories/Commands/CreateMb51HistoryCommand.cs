using Application.Services.IMb51HistoryRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MB51Histories.Commands;

public class CreateMb51HistoryCommand : IRequest<bool>
{
    public List<Mb51> Mb51s { get; set; }
    public DateTime ReportDate { get; set; }

    public class CreateMb51HistoryCommandHandler : IRequestHandler<CreateMb51HistoryCommand, bool>
    {
        private readonly IMb51HistoryRepository _mb51HistoryRepository;
        private readonly IMapper _mapper;


        public CreateMb51HistoryCommandHandler(
            IMb51HistoryRepository mb51HistoryRepository,
            IMapper mapper
           )
        {
            _mb51HistoryRepository = mb51HistoryRepository;
            _mapper = mapper;
        }


        public async Task<bool> Handle(CreateMb51HistoryCommand request, CancellationToken cancellationToken)
        {
            var mb51Histories = _mapper.Map<List<Mb51History>>(request.Mb51s);
            foreach (var item in mb51Histories)
            {
                item.CreatedTime = DateTime.Now;
                item.ReportDate = request.ReportDate;
                item.UserId = new Guid("A00F2551-380A-4585-4BF9-08DA688EFF3C");
            }
            bool result = await _mb51HistoryRepository.AddListAsync(mb51Histories) > 0 ? true : false;
            return result;
        }

    }

}

