using Application.Features.MainReports.Dtos;
using Application.Services.MainReportRepositories;
using AutoMapper;
using Core.Application;
using Domain.Entities;
using MediatR;

namespace Application.Features.MainReports.Commands;

public class CreateMainReportCommand : IRequest<Result<Unit>>
{
    public List<MainReportDto> MainReports { get; set; }

    public class CreateMainReportCommandHandler : IRequestHandler<CreateMainReportCommand, Result<Unit>>
    {
        private readonly IMainReportRepository _mainReportRepository;
        private readonly IMapper _mapper;
        public CreateMainReportCommandHandler(IMainReportRepository mainReportRepository, IMapper mapper)
        {
            _mainReportRepository = mainReportRepository;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(CreateMainReportCommand request, CancellationToken cancellationToken)
        {
            Result<Unit> result = new Result<Unit>();

            var mappedReport = _mapper.Map<List<MainReport>>(request.MainReports);

            bool anydata = await _mainReportRepository.AddListAsync(mappedReport) > 0 ? true : false;

            return result;
        }
    }
}