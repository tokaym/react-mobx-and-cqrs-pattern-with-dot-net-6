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
using Application.Services.IEstimateRepository;
using Application.Services.IZm20HistoryRepository;
using Application.Services.Repositories;

namespace Application.Features.MainReports.Queries;
public class GetEstimationQuery : IRequest<List<EstimationDto>>
{
    public PageRequest? PageRequest { get; set; }
    public OrderByRequest? OrderByRequest { get; set; }
    public class GetEstimationQueryandler : IRequestHandler<GetEstimationQuery, List<EstimationDto>>
    {
        private readonly IEstimateRepository _estimateRepository;
        private readonly IZm20HistoryRepository _zm20HistoryRepository;
        private readonly IMb51Repository _mb51Repository;
        private readonly IMipRepository _mipRepository;
        private readonly IMapper _mapper;
        public GetEstimationQueryandler(IEstimateRepository estimateRepository, IZm20HistoryRepository zm20HistoryRepository,
        IMipRepository mipRepository, IMb51Repository mb51Repository, IMapper mapper)
        {
            _mapper = mapper;
            _estimateRepository = estimateRepository;
            _zm20HistoryRepository = zm20HistoryRepository;
            _mb51Repository = mb51Repository;
            _mipRepository = mipRepository;
        }

        public async Task<List<EstimationDto>> Handle(GetEstimationQuery request, CancellationToken cancellationToken)
        {
            List<EstimationDto> result = new();
            DateTime date = DateTime.Now;
            List<Estimate> estimates = await _estimateRepository.GetEstimatesByDate(date.Year, date.Month);
            List<Mb51> mb51s = await _mb51Repository.GetAllAsync(643);
            List<Zm20History> zm20Histories = await _zm20HistoryRepository.GetListByMonth(date);
            List<Mip> mips = await _mipRepository.GetAllAsync();
            List<Zm20> zm20SKUandSatGroup = zm20Histories.GroupBy(g => new { g.MaterialSKU, g.MaterialName, g.SatSasNo, g.ReleaseDate, g.OpenAmount })
                                         .Select(s => new Zm20
                                         {
                                             MaterialSKU = s.First().MaterialSKU,
                                             MaterialName = s.First().MaterialName,
                                             SatSasNo = s.First().SatSasNo,
                                             ReleaseDate = s.First().ReleaseDate,
                                             OpenAmount = s.First().OpenAmount,
                                             TesMip = s.First().TesMip
                                         }).ToList();
            List<Mb51> mb51SkuGroup = mb51s.GroupBy(g => new { g.MaterialSKU })
                                .Select(s => new Mb51
                                {
                                    MaterialSKU = s.First().MaterialSKU,
                                    Amount = -s.Sum(a => a.Amount)
                                }).ToList();
            List<Zm20forReportDto> zm20SKUGroup = zm20SKUandSatGroup.GroupBy(g => new { g.MaterialSKU }).Select(s => new Zm20forReportDto
            {
                MaterialSKU = s.First().MaterialSKU,
                OpenAmount = s.Sum(a => a.OpenAmount)
            }).ToList();

            foreach (Zm20forReportDto item in zm20SKUGroup)
            {
                EstimationDto resultItem = new();
                resultItem.CD = mips.FirstOrDefault(a => a.Code == zm20SKUandSatGroup.FirstOrDefault(a => a.MaterialSKU == item.MaterialSKU)?.TesMip)?.CD;
                resultItem.MaterialName = zm20SKUandSatGroup.FirstOrDefault(a => a.MaterialSKU == item.MaterialSKU)?.MaterialName;
                resultItem.MaterialSKU = item.MaterialSKU;
                resultItem.Order = item.OpenAmount;
                resultItem.Estimate = estimates.FirstOrDefault(a => a.MaterialSKU == item.MaterialSKU)?.Quantity;
                resultItem.Estimate = resultItem.Estimate == null ? 0 : resultItem.Estimate;
                resultItem.Sent = mb51SkuGroup.FirstOrDefault(a => a.MaterialSKU == item.MaterialSKU)?.Amount;
                resultItem.Sent = resultItem.Sent == null ? 0 : resultItem.Sent;
                resultItem.Stock = zm20Histories.Where(a => a.MaterialSKU == item.MaterialSKU).OrderByDescending(a => a.ReportDate).Take(1).First().RemainingStock;
                try
                {
                    if (resultItem.Stock != 0)
                    {
                        double score = 0.0;
                        if (resultItem.Estimate >= resultItem.Order)
                        {
                            score = (double)((double)(resultItem.Stock) / ((resultItem.Estimate - resultItem.Sent)));
                        }
                        else
                        {
                            score = (double)((double)(resultItem.Stock) / ((resultItem.Order - resultItem.Sent)));
                        }
                        if (Double.IsInfinity(score) || Double.IsNaN(score))
                            resultItem.StockScore = resultItem.Stock.ToString();
                        else
                            resultItem.StockScore = score.ToString();
                    }
                    else
                        resultItem.StockScore = "0";
                }
                catch (DivideByZeroException)
                {
                    resultItem.StockScore = "0";
                }
                result.Add(resultItem);
            }
            return result;
        }
    }
}

