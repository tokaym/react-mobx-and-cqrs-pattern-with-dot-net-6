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
public class GetTodayTableQuery : IRequest<List<TodayTableDto>>
{
    public PageRequest? PageRequest { get; set; }
    public OrderByRequest? OrderByRequest { get; set; }
    public string PlantCode { get; set; }
    public class GetTodayTableQueryHandler : IRequestHandler<GetTodayTableQuery, List<TodayTableDto>>
    {
        private readonly IMainReportRepository _mainReportRepository;
        private readonly IMapper _mapper;
        public GetTodayTableQueryHandler(IMainReportRepository mainReportRepository, IMapper mapper)
        {
            _mainReportRepository = mainReportRepository;
            _mapper = mapper;
        }

        public async Task<List<TodayTableDto>> Handle(GetTodayTableQuery request, CancellationToken cancellationToken)
        {
            var reportDates = _mainReportRepository.GetReportDatesDateTime(request.PlantCode);
            int endDateIndex = reportDates.Count < 3 ? 0 : 2;
            var report = _mainReportRepository.GetByReportDate(reportDates[endDateIndex], reportDates[0], request.PlantCode).OrderByDescending(a => a.ReportDate);
            List<TodayTableDto> todaytable = report
                        .GroupBy(a => new { a.ReportDate })
                        .Select(r => new TodayTableDto
                        {
                            Date = r.First().ReportDate.ToString("dd.MM.yyyy"),
                            OpenAmount = r.Sum(a => a.OpenAmount),
                            Item = r.Sum(a => a.Item),
                            HF = r.Sum(a => a.HF),
                            Urgent = r.Sum(a => a.Urgent)
                        }).ToList();

            return todaytable;
        }
    }
}

