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

namespace Application.Features.MainReports.Queries;
public class GetOrderRatesQuery : IRequest<List<PieChartDto>>
{
    public PageRequest? PageRequest { get; set; }
    public OrderByRequest? OrderByRequest { get; set; }
    public string PlantCode { get; set; }
    public class GetOrderRatesQueryHandler : IRequestHandler<GetOrderRatesQuery, List<PieChartDto>>
    {
        private readonly IMainReportRepository _mainReportRepository;
        private readonly IMb51Repository _mb51Repository;
        private readonly IMapper _mapper;
        public GetOrderRatesQueryHandler(IMainReportRepository mainReportRepository, IMb51Repository mb51Repository, IMapper mapper)
        {
            _mainReportRepository = mainReportRepository;
            _mb51Repository = mb51Repository;
            _mapper = mapper;
        }

        public async Task<List<PieChartDto>> Handle(GetOrderRatesQuery request, CancellationToken cancellationToken)
        {
            var remainingOrder = _mainReportRepository.GetByReportDate(_mainReportRepository.GetMaxDate(request.PlantCode), request.PlantCode);
            var sendOrder = _mb51Repository.GetAll(request.PlantCode.TryInt32Parse()).Where(a=>a.RegisterDate >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
            List<PieChartDto> chart = sendOrder
                        .GroupBy(a => 1)
                        .Select(r => new PieChartDto
                        {
                            Name = "Gönderilen Sipariş",
                            Value = r.Sum(a => a.Amount) * (-1)
                        }).ToList();

            chart.AddRange(remainingOrder
                        .GroupBy(a => new { a.ReportDate })
                        .Select(r => new PieChartDto
                        {
                            Name = "Kalan Sipariş",
                            Value = r.Sum(a => a.OpenAmount)
                        }));

            return chart;
        }
    }
}

