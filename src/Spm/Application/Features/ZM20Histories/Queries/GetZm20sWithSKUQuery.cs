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
public class GetZm20sWithSKUQuery : IRequest<List<Zm20forReportDto>>
{
    public string? MaterialSKU { get; set; }
    public class GetZm20sWithSKUQueryHandler : IRequestHandler<GetZm20sWithSKUQuery, List<Zm20forReportDto>>
    {
        private readonly IZm20Repository _zm20Repository;
        private readonly IMapper _mapper;
        public GetZm20sWithSKUQueryHandler(IZm20Repository zm20Repository, IMapper mapper)
        {
            _zm20Repository = zm20Repository;
            _mapper = mapper;
        }

        public async Task<List<Zm20forReportDto>> Handle(GetZm20sWithSKUQuery request, CancellationToken cancellationToken)
        {
            var zm20s = _zm20Repository.GetZm20s(request.MaterialSKU);
            var mappedZm20s = _mapper.Map<List<Zm20forReportDto>>(zm20s);
            return mappedZm20s;
        }
    }
}