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
using Application.Services.Repositories;
using Application.Services.IPlantRepository;

namespace Application.Features.MainReports.Queries;

public class ResultLine
{
    public ResultLine() { }

    public string MaterialSKU { get; set; }
    public string Value { get; set; }
}

public class ReceiveReportQuery : IRequest<bool>
{
    public DateTime ReportDate { get; set; }
    public string PlantCode { get; set; }
    public class ReceiveReportQueryHandler : IRequestHandler<ReceiveReportQuery, bool>
    {
        private readonly IZm20Repository _zm20Repository;
        private readonly IMb51Repository _mb51Repository;
        private readonly IZs14Repository _zs14Repository;
        private readonly IMaterialGroupRepository _mgRepository;
        private readonly IMainReportRepository _mainReportRepository;
        private readonly IMipRepository _mipRepository;
        private readonly IPlantRepository _plantRepository;
        private readonly IMapper _mapper;

        public ReceiveReportQueryHandler(IZm20Repository zm20Repository, IMb51Repository mb51Repository,
         IZs14Repository zs14Repository, IMapper mapper, IMaterialGroupRepository mgRepository,
        IMainReportRepository mainReportRepository, IMipRepository mipRepository,IPlantRepository plantRepository)
        {
            _zm20Repository = zm20Repository;
            _mb51Repository = mb51Repository;
            _zs14Repository = zs14Repository;
            _mgRepository = mgRepository;
            _mainReportRepository = mainReportRepository;
            _mipRepository = mipRepository;
            _plantRepository = plantRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(ReceiveReportQuery request, CancellationToken cancellationToken)
        {
            List<MainReportDto> listMain = new List<MainReportDto>();
            List<Zm20> zm20s = _zm20Repository.GetAll();
            List<Mb51> mb51s = _mb51Repository.GetAll(request.PlantCode.TryInt32Parse());
            List<MaterialGroup> materialGroups = _mgRepository.GetAll();
            List<Mip> mips = await _mipRepository.GetAllAsync();

            listMain = zm20s
                        .GroupBy(a => new { a.MaterialSKU, a.MaterialName })
                        .Select(r => new MainReportDto
                        {
                            MaterialSKU = r.First().MaterialSKU,
                            MaterialName = r.First().MaterialName,
                            OpenAmount = r.Sum(a => a.OpenAmount),
                            Item = r.Count()
                        }).ToList();

            List<MainReportDto> listStock = zm20s
                        .GroupBy(a => new { a.MaterialSKU, a.RemainingStock })
                        .Select(r => new MainReportDto
                        {
                            MaterialSKU = r.First().MaterialSKU,
                            Stock = r.First().RemainingStock
                        }).ToList();

            List<MainReportDto> listCD = zm20s
                        .GroupBy(a => new { a.MaterialSKU, a.TesMip })
                        .Select(r => new MainReportDto
                        {
                            MaterialSKU = r.First().MaterialSKU,
                            CD = r.First().TesMip?.ToString().TryInt32Parse() > 0 ? "Demode" : "Cari"
                        }).ToList();

            List<MainReportDto> listTT = zm20s
                        .GroupBy(a => new { a.MaterialSKU, a.TesMip, a.TT })
                        .Select(r => new MainReportDto
                        {
                            MaterialSKU = r.First().MaterialSKU,
                            Mip = r.First().TesMip,
                            Company = r.First().TT
                        }).ToList();

            List<MainReportDto> listOrderDate = zm20s
                        .GroupBy(a => new { a.MaterialSKU, a.ReleaseDate })
                        .Select(r => new MainReportDto
                        {
                            MaterialSKU = r.First().MaterialSKU,
                            FirstOrderDate = r.Min(a => a.ReleaseDate)
                        }).ToList();

            List<MainReportDto> listSent = mb51s
                        .GroupBy(a => new { a.MaterialSKU })
                        .Select(r => new MainReportDto
                        {
                            MaterialSKU = r.First().MaterialSKU,
                            Sent = r.Sum(a => a.Amount) * -1
                        }).ToList();

            foreach (var item in listMain)
            {
                var zs14 = _zs14Repository.GetZs14(item.MaterialSKU);
                string mipCode = listTT.FirstOrDefault(a => a.MaterialSKU == item.MaterialSKU)?.Mip;
                item.Stock = listStock.FirstOrDefault(a => a.MaterialSKU == item.MaterialSKU)?.Stock;
                item.CD = mips.FirstOrDefault(a=>a.Code == mipCode)?.CD;
                item.Company = listTT.FirstOrDefault(a => a.MaterialSKU == item.MaterialSKU)?.Company;
                item.FirstOrderDate = listOrderDate.FirstOrDefault(a => a.MaterialSKU == item.MaterialSKU)?.FirstOrderDate;
                item.Sent = listSent.FirstOrDefault(a => a.MaterialSKU == item.MaterialSKU)?.Sent == null ? 0 : listSent.FirstOrDefault(a => a.MaterialSKU == item.MaterialSKU)?.Sent;
                item.Mip = mipCode;
                item.UserId = new Guid("A00F2551-380A-4585-4BF9-08DA688EFF3C");
                item.ProductClass = materialGroups.FirstOrDefault(a => a.MaterialSKU == item.MaterialSKU)?.GroupName;
                if (zs14.Star == "*")
                    item.Urgent = zs14.HfOrder + zs14.YsOrder + zs14.YDKIOrder + zs14.YDKDOrder + zs14.CgrSas + zs14.CgrSat;
                else
                    item.Urgent = 0;

                item.ThStock = zs14.UYAmbar;
                item.HF = zs14.HfOrder;
                item.ReportDate = request.ReportDate.Date;
                item.CreatedTime = DateTime.Now;
                item.PlantId = _plantRepository.Get(a=>a.Code == request.PlantCode).Id;

                if (item.Stock == 0)
                {
                    item.SasCloses = 0;
                    item.HfCloses = 0;
                    item.UrgentCloses = 0;
                }
                else
                {
                    if (item.Stock - item.OpenAmount >= 0)
                    {
                        item.SasCloses = 1;
                    }
                    else
                    {
                        item.SasCloses = 2;
                    }

                    if (item.Stock - item.Urgent >= 0)
                    {
                        item.UrgentCloses = 1;
                    }
                    else
                    {
                        item.UrgentCloses = 2;
                    }

                    if (item.Stock - item.HF >= 0)
                    {
                        item.HfCloses = 1;
                    }
                    else
                    {
                        item.HfCloses = 2;
                    }
                }

            }

            //MainReport Tablosuna kayıt ediliyor.
            var mappedReport = _mapper.Map<List<MainReport>>(listMain);

            _mainReportRepository.DeleteByReportDate(request.ReportDate,request.PlantCode);

            bool anydata = await _mainReportRepository.AddListAsync(mappedReport) > 0 ? true : false;

            return anydata;
        }
    }
}