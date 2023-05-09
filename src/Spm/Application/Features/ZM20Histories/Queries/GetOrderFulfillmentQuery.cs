using Domain.Entities;
using MediatR;
using AutoMapper;
using Application.Services.IZm20Repository;
using Application.Services.IMb51Repository;
using Application.Services.IZs14Repository;
using Core.Utilities;
using Application.Features.MainReports.Dtos;
using Core.Application.Requests;
using Application.Services.MainReportRepositories;
using Application.Features.Users.Models;
using Application.Features.ZM20Histories.Dtos;
using Application.Services.IZm20HistoryRepository;

namespace Application.Features.ZM20Histories.Queries;
public class GetOrderFulfillmentQuery : IRequest<OrderFulfillmentDto>
{
    public PageRequest? PageRequest { get; set; }
    public OrderByRequest? OrderByRequest { get; set; }
    public class GetOrderFulfillmentQueryHandler : IRequestHandler<GetOrderFulfillmentQuery, OrderFulfillmentDto>
    {
        private readonly IZm20HistoryRepository _zm20HistoryRepository;
        private readonly IMapper _mapper;
        public GetOrderFulfillmentQueryHandler(IZm20HistoryRepository zm20HistoryRepository, IMapper mapper)
        {
            _zm20HistoryRepository = zm20HistoryRepository;
            _mapper = mapper;
        }

        public async Task<OrderFulfillmentDto> Handle(GetOrderFulfillmentQuery request, CancellationToken cancellationToken)
        {
            DateTime now = DateTime.Now;
            DateTime currentMonthStart = new DateTime(now.Year, now.Month, 1);
            DateTime lastUploadDate = _zm20HistoryRepository.GetLastUploadDate();
            List<Zm20History> zm20Histories = await _zm20HistoryRepository.GetListAsync();
            List<Zm20History> openOrders = zm20Histories.Where(a => a.ReportDate == lastUploadDate).ToList();
            List<OrderCloses> list = zm20Histories.
                                    GroupBy(a => new { a.MaterialSat, a.MaterialName, a.MaterialSKU }).
                                    Where(w => w.Min(x => x.ReleaseDate) != Convert.ToDateTime("0001-01-01") &&
                                            w.Max(x => x.ReportDate) >= currentMonthStart &&
                                            w.Max(x => x.ReportDate) < lastUploadDate).
                                    Select(s => new OrderCloses
                                    {
                                        MaterialName = s.First().MaterialName,
                                        MaterialSat = s.First().MaterialSat,
                                        MaterialSKU = s.First().MaterialSKU,
                                        OpenDate = s.Min(x => x.ReleaseDate) ?? currentMonthStart,
                                        ClosedDate = s.Max(x => x.ReportDate),
                                        Quantity = s.Sum(a => a.OpenAmount) ?? 0
                                    }).ToList();

            OrderFulfillmentDto result = new OrderFulfillmentDto
            {
                TotalOpenOrder = openOrders.Count()
            };

            result.Zm20Materials = list.GroupBy(a => new { a.MaterialSKU, a.MaterialName }).
                                                Select(s => new Zm20Material
                                                {
                                                    MaterialName = s.First().MaterialName,
                                                    MaterialSKU = s.First().MaterialSKU,
                                                    Quantity = s.Sum(a => a.Quantity)
                                                }).ToList();

            foreach (var item in result.Zm20Materials)
            {
                item.MaterialSatSass = list.Where(a => a.MaterialSKU == item.MaterialSKU)
                                            .Select(s => new MaterialSatSas
                                            {
                                                SatSasNo = s.MaterialSat.Substring(10, 10),
                                                OpenDate = s.OpenDate.ToString("dd.MM.yyyy"),
                                                ClosedDate = s.ClosedDate.ToString("dd.MM.yyyy"),
                                                DateDayDiff = (s.ClosedDate.Date - s.OpenDate.Date).TotalDays.ToString().TryInt32Parse(),
                                                Quantity = s.Quantity,
                                            }).ToList();
            }

            return result;
        }
    }
}

