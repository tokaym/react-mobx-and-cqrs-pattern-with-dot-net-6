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
using Application.Services.IPlantRepository;

namespace Application.Features.MainReports.Queries;
public class GetMainReportQuery : IRequest<MainReportListModel>
{
    public PageRequest? PageRequest { get; set; }
    public OrderByRequest? OrderByRequest { get; set; }
    public string Search { get; set; }
    public string Date { get; set; }
    public string Type { get; set; }
    public string PlantCode { get; set; }
    public class GetMainReportQueryHandler : IRequestHandler<GetMainReportQuery, MainReportListModel>
    {
        private readonly IMainReportRepository _mainReportRepository;
        private readonly IPlantRepository _plantRepository;
        private readonly IMapper _mapper;
        public GetMainReportQueryHandler(IMainReportRepository mainReportRepository,
        IPlantRepository plantRepository, IMapper mapper)
        {
            _mainReportRepository = mainReportRepository;
            _plantRepository = plantRepository;
            _mapper = mapper;
        }

        public async Task<MainReportListModel> Handle(GetMainReportQuery request, CancellationToken cancellationToken)
        {
            MainReportListModel result = new MainReportListModel();

            if (_mainReportRepository.DataCheck(request.PlantCode))
            {
                DateTime date = new DateTime();
                if (request.Date == null)
                    date = _mainReportRepository.GetMaxDate(request.PlantCode);
                else
                    date = Convert.ToDateTime(request.Date);
                    
                var plantId = _plantRepository.Get(a=>a.Code == request.PlantCode).Id;

                var mainReports = await _mainReportRepository.GetListAsync(
                        index: request.PageRequest.Page,
                        size: request.PageRequest.PageSize,
                        predicate: (a => a.ReportDate == date && a.PlantId == plantId &&
                                        (a.MaterialName.Contains(request.Search) ||
                                         a.MaterialSKU.Contains(request.Search) ||
                                         a.FirstOrderDate.ToString().Contains(request.Search)
                                         )),
                        orderBy: OrderingLibrary.GetOrderBy<MainReport>(request.OrderByRequest.ColumnName, request.OrderByRequest.Type)
                        );


                result = _mapper.Map<MainReportListModel>(mainReports);
            }

            return result;
        }
    }
}

