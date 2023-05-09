using Domain.Entities;
using MediatR;
using Application.Services.IZs14HistoryRepository;
using AutoMapper;

namespace Application.Features.Zs14Histories.Commands;

public class CreateZs14HistoryCommand : IRequest<Zs14History>
{
    public string? Star { get; set; }
    public DateTime InstantDate { get; set; }
    public string? TYeri { get; set; }
    public string? MaterialSKU { get; set; }
    public string? MipArea { get; set; }
    public string? MaterialName{get;set;}
    public string? Empty1 { get; set; }
    public string? Definition { get; set; }
    public int HfOrder { get; set; }
    public int YsOrder { get; set; }
    public int YDKIOrder { get; set; }
    public int YDKDOrder { get; set; }
    public int MIhrTes { get; set; }
    public DateTime YIIlkSip { get; set; }
    public DateTime YDIlkSip { get; set; }
    public int Stnkrz { get; set; }
    public int CgrSas { get; set; }
    public int CgrSat { get; set; }
    public int UYAmbar { get; set; }
    public int UYDiger { get; set; }
    public string? YP { get; set; }
    public int IsSafety { get; set; }
    public int? MP { get; set; }
    public int Mip { get; set; }
    public string? SG { get; set; }
    public string? Dr { get; set; }
    public string? Dr2 { get; set; }
    public DateTime SasDelivery { get; set; }
    public DateTime SasConfirm { get; set; }
    public DateTime DeadlineDate { get; set; }
    public int Sat { get; set; }
    public int Sas { get; set; }
    public int Teslpln { get; set; }
    public int ConsumptionValue { get; set; }
    public int TransportStock { get; set; }

    public class CreateZs14HistoryCommandHandler : IRequestHandler<CreateZs14HistoryCommand, Zs14History>
    {
        private readonly IZs14HistoryRepository _zs14HistoryRepository;
        private readonly IMapper _mapper;

        public CreateZs14HistoryCommandHandler(
            IZs14HistoryRepository zs14HistoryRepository,
            IMapper mapper)
        {
            _zs14HistoryRepository = zs14HistoryRepository;
            _mapper = mapper;
        }

        public async Task<Zs14History> Handle(CreateZs14HistoryCommand request, CancellationToken cancellationToken){
            Zs14History zs14History = _mapper.Map<Zs14History>(request);
            Zs14History createdZs14 = await _zs14HistoryRepository.AddAsync(zs14History);

            return createdZs14;
        }
    }
}