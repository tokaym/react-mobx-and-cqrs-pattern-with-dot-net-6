using Domain.Entities;
using MediatR;
using AutoMapper;
using Core.Application.Requests;
using Application.Services.IRomaniaZm20HistoryRepository;
using Application.Features.RomaniaZM20Histories.Dtos;

namespace Application.Features.RomaniaZm20Histories.Queries;
public class GetOrderFulfillmentQuery : IRequest<List<OrderFulfillmentDto>>
{
    public PageRequest? PageRequest { get; set; }
    public OrderByRequest? OrderByRequest { get; set; }
    public class GetOrderFulfillmentQueryHandler : IRequestHandler<GetOrderFulfillmentQuery, List<OrderFulfillmentDto>>
    {
        private readonly IRomaniaZm20HistoryRepository _romaniaZm20HistoryRepository;
        private readonly IMapper _mapper;
        public GetOrderFulfillmentQueryHandler(IRomaniaZm20HistoryRepository romaniaZm20HistoryRepository, IMapper mapper)
        {
            _romaniaZm20HistoryRepository = romaniaZm20HistoryRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderFulfillmentDto>> Handle(GetOrderFulfillmentQuery request, CancellationToken cancellationToken)
        {
            DateTime now = DateTime.Now;
            DateTime currentMonthStart = new DateTime(now.Year, now.Month, 1);
            DateTime lastUploadDate = _romaniaZm20HistoryRepository.GetLastUploadDate();
            List<RomaniaZm20History> romaniaZm20Histories = await _romaniaZm20HistoryRepository.GetListAsync();
            List<OrderCloses> list = romaniaZm20Histories.
                                    GroupBy(a => new { a.MaterialSat, a.MaterialName, a.MaterialNo }).
                                    Where(w => w.Min(x => x.ReleaseDate) != Convert.ToDateTime("0001-01-01") &&
                                            w.Max(x => x.ReportDate) >= currentMonthStart &&
                                            w.Max(x => x.ReportDate) < lastUploadDate).
                                    Select(s => new OrderCloses
                                    {
                                        MaterialName = s.First().MaterialName,
                                        MaterialSat = s.First().MaterialSat,
                                        MaterialSKU = s.First().MaterialNo,
                                        OpenDate = s.Min(x => x.ReleaseDate) ?? currentMonthStart,
                                        ClosedDate = s.Max(x => x.ReportDate),
                                        Quantity = s.Sum(a => a.OpenQuantity) ?? 0
                                    }).ToList();

            List<OrderFulfillmentDto> result = list.GroupBy(a => new { a.MaterialSKU, a.MaterialName }).
                                                Select(s => new OrderFulfillmentDto
                                                {
                                                    MaterialName = s.First().MaterialName,
                                                    MaterialSKU = s.First().MaterialSKU,
                                                    Quantity = s.Sum(a=>a.Quantity)
                                                }).ToList();

            foreach (var item in result)
            {
                item.MaterialSatSass = list.Where(a => a.MaterialSKU == item.MaterialSKU)
                                            .Select(s => new MaterialSatSas
                                            {
                                                SatSasNo = s.MaterialSat.Substring(10, 10),
                                                OpenDate = s.OpenDate.ToString("dd.MM.yyyy"),
                                                ClosedDate = s.ClosedDate.ToString("dd.MM.yyyy"),
                                                DateDayDiff = (s.ClosedDate.Date - s.OpenDate.Date).TotalDays.ToString(),
                                                Quantity = s.Quantity
                                            }).ToList();
            }

            return result;
        }
    }
}

