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

public class GetMainReportDatesQuery : IRequest<List<string>>
{
    public string PlantCode { get; set; }
    public class GetMainReportDatesQueryHandler : IRequestHandler<GetMainReportDatesQuery, List<string>>
    {
        private readonly IMainReportRepository _mainReportRepository;
        private readonly IMapper _mapper;
        private readonly IPlantRepository _plantRepository;

        public GetMainReportDatesQueryHandler(IMapper mapper, IMainReportRepository mainReportRepository
        ,IPlantRepository plantRepository)
        {
            _mainReportRepository = mainReportRepository;
            _plantRepository = plantRepository;
            _mapper = mapper;
        }

        public async Task<List<string>> Handle(GetMainReportDatesQuery request, CancellationToken cancellationToken)
        {
            return _mainReportRepository.GetReportDates(request.PlantCode);
        }
    }
}