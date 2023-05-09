using Application.Models;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Mips.Commands;

public class CreateMipCommand : IRequest<ReturnModel<Mip>>
{

    public Mip mip { get; set; }
    public class CreateMipCommandHandler : IRequestHandler<CreateMipCommand, ReturnModel<Mip>>
    {
        private readonly IMipRepository _mipRepository;
        private readonly IMapper _mapper;


        public CreateMipCommandHandler(
            IMipRepository mipRepository,
            IMapper mapper
           )
        {
            _mipRepository = mipRepository;
            _mapper = mapper;
        }


        public async Task<ReturnModel<Mip>> Handle(CreateMipCommand request, CancellationToken cancellationToken)
        {
           ReturnModel<Mip> result = new();
            try
            {
                if (!await _mipRepository.AnyAsync(a => a.Code == request.mip.Code))
                {
                    result.Data = await _mipRepository.AddAsync(request.mip);
                    result.Message = "Mip başarıyla kayıt edildi.";
                    result.Status = ReturnTypeStatus.Success;
                }
                else
                {
                    result.Message = "Bu Mip için daha önceden kayıt oluşturulmuştur!";
                    result.Status = ReturnTypeStatus.UnSuccess;
                }
            }
            catch (System.Exception ex)
            {
                result.Status = ReturnTypeStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

    }

}

