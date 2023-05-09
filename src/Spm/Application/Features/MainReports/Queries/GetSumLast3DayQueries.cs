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
public class GetSumLast3DayQueries : IRequest<ReportLast3DayDto>
{
    public string Type { get; set; }
    public class GetSumLast3DayQueriesHandler : IRequestHandler<GetSumLast3DayQueries, ReportLast3DayDto>
    {
        private readonly IMainReportRepository _mainReportRepository;
        private readonly IMapper _mapper;
        public GetSumLast3DayQueriesHandler(IMainReportRepository mainReportRepository, IMapper mapper)
        {
            _mainReportRepository = mainReportRepository;
            _mapper = mapper;
        }

        public async Task<ReportLast3DayDto> Handle(GetSumLast3DayQueries request, CancellationToken cancellationToken)
        {
            var reportDates = _mainReportRepository.GetReportDatesDateTime("643").Take(3).ToList();
            var report = _mainReportRepository.GetByReportDate(reportDates[2], reportDates[0],"643");
            DateTime date = DateTime.Now.Date;
            ReportLast3DayDto result = new();
            if (request.Type == "OpenAmount")
            {
                var sumDate1 = report.Where(c => c.ReportDate.Date == reportDates[2].Date).Sum(a => a.OpenAmount).ToString();
                var sumDate2 = report.Where(c => c.ReportDate.Date == reportDates[1].Date).Sum(a => a.OpenAmount).ToString();
                var sumDate3 = report.Where(c => c.ReportDate.Date == reportDates[0].Date).Sum(a => a.OpenAmount).ToString();
                var pReport = report
                                    .GroupBy(g => new { g.MaterialSKU, g.MaterialName })
                                    .Select(a => new ReportTable
                                    {
                                        MaterialName = a.First().MaterialName,
                                        MaterialSKU = a.First().MaterialSKU,
                                        Date1Value = a.Where(c => c.ReportDate.Day == reportDates[2].Day).Sum(a => a.OpenAmount).ToString().TryInt32Parse(),
                                        Date2Value = a.Where(c => c.ReportDate.Day == reportDates[1].Day).Sum(a => a.OpenAmount).ToString().TryInt32Parse(),
                                        Date3Value = a.Where(c => c.ReportDate.Day == reportDates[0].Day).Sum(a => a.OpenAmount).ToString().TryInt32Parse()
                                    }).OrderByDescending(a => a.Date3Value).ToList();
                result = new ReportLast3DayDto
                {
                    Date1 = new ColumnDate
                    {
                        Name = reportDates[2].ToString("dd.MM.yyyy"),
                        Sum = sumDate1.TryInt32Parse()
                    },
                    Date2 = new ColumnDate
                    {
                        Name = reportDates[1].ToString("dd.MM.yyyy"),
                        Sum = sumDate2.TryInt32Parse()
                    },
                    Date3 = new ColumnDate
                    {
                        Name = reportDates[0].ToString("dd.MM.yyyy"),
                        Sum = sumDate3.TryInt32Parse()
                    },
                    ReportTables = pReport
                };
            }
            else if (request.Type == "Hf")
            {
                var sumDate1 = report.Where(c => c.ReportDate.Date == reportDates[2].Date).Sum(a => a.HF).ToString();
                var sumDate2 = report.Where(c => c.ReportDate.Date == reportDates[1].Date).Sum(a => a.HF).ToString();
                var sumDate3 = report.Where(c => c.ReportDate.Date == reportDates[0].Date).Sum(a => a.HF).ToString();
                var pReport = report
                                    .GroupBy(g => new { g.MaterialSKU, g.MaterialName })
                                    .Select(a => new ReportTable
                                    {
                                        MaterialName = a.First().MaterialName,
                                        MaterialSKU = a.First().MaterialSKU,
                                        Date1Value = a.Where(c => c.ReportDate.Day == reportDates[2].Day).Sum(a => a.HF).ToString().TryInt32Parse(),
                                        Date2Value = a.Where(c => c.ReportDate.Day == reportDates[1].Day).Sum(a => a.HF).ToString().TryInt32Parse(),
                                        Date3Value = a.Where(c => c.ReportDate.Day == reportDates[0].Day).Sum(a => a.HF).ToString().TryInt32Parse()
                                    }).OrderByDescending(a => a.Date3Value).ToList();
                result = new ReportLast3DayDto
                {
                    Date1 = new ColumnDate
                    {
                        Name = reportDates[2].ToString("dd.MM.yyyy"),
                        Sum = sumDate1.TryInt32Parse()
                    },
                    Date2 = new ColumnDate
                    {
                        Name = reportDates[1].ToString("dd.MM.yyyy"),
                        Sum = sumDate2.TryInt32Parse()
                    },
                    Date3 = new ColumnDate
                    {
                        Name = reportDates[0].ToString("dd.MM.yyyy"),
                        Sum = sumDate3.TryInt32Parse()
                    },
                    ReportTables = pReport
                };
            }
            else if (request.Type == "Urgent")
            {
                var sumDate1 = report.Where(c => c.ReportDate.Date == reportDates[2].Date).Sum(a => a.Urgent).ToString();
                var sumDate2 = report.Where(c => c.ReportDate.Date == reportDates[1].Date).Sum(a => a.Urgent).ToString();
                var sumDate3 = report.Where(c => c.ReportDate.Date == reportDates[0].Date).Sum(a => a.Urgent).ToString();
                var pReport = report
                                    .GroupBy(g => new { g.MaterialSKU, g.MaterialName })
                                    .Select(a => new ReportTable
                                    {
                                        MaterialName = a.First().MaterialName,
                                        MaterialSKU = a.First().MaterialSKU,
                                        Date1Value = a.Where(c => c.ReportDate.Day == reportDates[2].Day).Sum(a => a.Urgent).ToString().TryInt32Parse(),
                                        Date2Value = a.Where(c => c.ReportDate.Day == reportDates[1].Day).Sum(a => a.Urgent).ToString().TryInt32Parse(),
                                        Date3Value = a.Where(c => c.ReportDate.Day == reportDates[0].Day).Sum(a => a.Urgent).ToString().TryInt32Parse()
                                    }).OrderByDescending(a => a.Date3Value).ToList();
                result = new ReportLast3DayDto
                {
                    Date1 = new ColumnDate
                    {
                        Name = reportDates[2].ToString("dd.MM.yyyy"),
                        Sum = sumDate1.TryInt32Parse()
                    },
                    Date2 = new ColumnDate
                    {
                        Name = reportDates[1].ToString("dd.MM.yyyy"),
                        Sum = sumDate2.TryInt32Parse()
                    },
                    Date3 = new ColumnDate
                    {
                        Name = reportDates[0].ToString("dd.MM.yyyy"),
                        Sum = sumDate3.TryInt32Parse()
                    },
                    ReportTables = pReport
                };
            }

            return result;
        }
    }
}

