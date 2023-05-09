using Application.Services.IEstimateRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Estimates.Commands;

public class CreateEstimateCommand : IRequest<bool>
{

    public List<Estimate> Estimates { get; set; }
    public class CreateEstimateCommandHandler : IRequestHandler<CreateEstimateCommand, bool>
    {
        private readonly IEstimateRepository _estimateRepository;
        private readonly IMapper _mapper;


        public CreateEstimateCommandHandler(
            IEstimateRepository estimateRepository,
            IMapper mapper
           )
        {
            _estimateRepository = estimateRepository;
            _mapper = mapper;
        }


        public async Task<bool> Handle(CreateEstimateCommand request, CancellationToken cancellationToken)
        {
            bool result = await _estimateRepository.AddListAsync(request.Estimates) > 0 ? true : false;
            return result;
        }

    }

}

