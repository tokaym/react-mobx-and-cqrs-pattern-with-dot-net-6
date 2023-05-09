using MediatR;
using AutoMapper;
using Application.Features.RomaniaZM20Histories.Dtos;
using Application.Services.IRomaniaZm20Repository;

namespace Application.Features.RomaniaZM20Histories.Queries;
public class GetRomaniaZm20sWithSKUQuery : IRequest<List<RomaniaZm20forReportDto>>
{
    public string? MaterialSKU { get; set; }
    public class GetRomaniaZm20sWithSKUQueryHandler : IRequestHandler<GetRomaniaZm20sWithSKUQuery, List<RomaniaZm20forReportDto>>
    {
        private readonly IRomaniaZm20Repository _romaniaZm20Repository;
        private readonly IMapper _mapper;
        public GetRomaniaZm20sWithSKUQueryHandler(IRomaniaZm20Repository romaniaZm20Repository, IMapper mapper)
        {
            _romaniaZm20Repository = romaniaZm20Repository;
            _mapper = mapper;
        }

        public async Task<List<RomaniaZm20forReportDto>> Handle(GetRomaniaZm20sWithSKUQuery request, CancellationToken cancellationToken)
        {
            var romaniaZm20s = _romaniaZm20Repository.GetRomaniaZm20s(request.MaterialSKU);
            var mappedZm20s = _mapper.Map<List<RomaniaZm20forReportDto>>(romaniaZm20s);
            return mappedZm20s;
        }
    }
}