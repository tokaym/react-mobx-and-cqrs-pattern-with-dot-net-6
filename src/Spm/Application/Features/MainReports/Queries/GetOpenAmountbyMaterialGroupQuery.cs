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
public class GetOpenAmountbyMaterialGroupQuery : IRequest<List<BarChartDto>>
{
    public PageRequest? PageRequest { get; set; }
    public OrderByRequest? OrderByRequest { get; set; }
    public string PlantCode { get; set; }
    public class GetOpenAmountbyMaterialGroupQueryHandler : IRequestHandler<GetOpenAmountbyMaterialGroupQuery, List<BarChartDto>>
    {
        private readonly IMainReportRepository _mainReportRepository;
        private readonly IMapper _mapper;
        public GetOpenAmountbyMaterialGroupQueryHandler(IMainReportRepository mainReportRepository, IMapper mapper)
        {
            _mainReportRepository = mainReportRepository;
            _mapper = mapper;
        }

        public async Task<List<BarChartDto>> Handle(GetOpenAmountbyMaterialGroupQuery request, CancellationToken cancellationToken)
        {
            var report = _mainReportRepository.GetByReportDate(_mainReportRepository.GetMaxDate(request.PlantCode),request.PlantCode);
            List<BarChartDto> charts = report
                        .GroupBy(a => new { a.ProductClass })
                        .Select(r => new BarChartDto
                        {
                            Name = r.First().ProductClass,
                            Cari = r.Where(a => a.CD == "Cari").Sum(a => a.OpenAmount),
                            Demode = r.Where(a => a.CD == "Demode").Sum(a => a.OpenAmount),
                            Toplam = r.Sum(a => a.OpenAmount)
                        }).OrderByDescending(a => a.Toplam).ToList();

            List<BarChartDto> top7 = charts.Take(7).ToList();
            BarChartDto other = new BarChartDto
            {
                Name = "Diğer",
                Cari = charts.Where(a => !top7.Select(b => b.Name).Contains(a.Name)).Sum(a => a.Cari),
                Demode = charts.Where(a => !top7.Select(b => b.Name).Contains(a.Name)).Sum(a => a.Demode),
                Toplam = charts.Where(a => !top7.Select(b => b.Name).Contains(a.Name)).Sum(a => a.Cari) + charts.Where(a => !top7.Select(b => b.Name).Contains(a.Name)).Sum(a => a.Demode)
            };
            charts.Clear();
            charts.AddRange(top7);
            charts.Add(other);

            BarChartDto total = new BarChartDto() { Name = "Total", Cari = 0, Demode = 0, Toplam = 0 };
            foreach (BarChartDto item in charts)
            {
                total.Cari = item.Cari + total.Cari;
                total.Demode = item.Demode + total.Demode;
                total.Toplam = item.Toplam + total.Toplam;
            }

            charts.Add(total);
            foreach (BarChartDto item in charts)
            {
                item.Oran = Math.Round(((double)item.Toplam / (double)total.Toplam) * 100, 2).ToString() + "%";
            }

            return charts;
        }
    }
}

