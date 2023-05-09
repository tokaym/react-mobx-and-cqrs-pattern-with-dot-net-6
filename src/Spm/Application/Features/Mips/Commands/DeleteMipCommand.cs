using Application.Services.IZm20Repository;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Application;
using Domain.Entities;
using MediatR;
using Application.Services.Repositories;
using Application.Models;

namespace Application.Features.Mips.Commands;

public class DeleteMipCommand : IRequest<ReturnModel<Mip>>
{
    public Guid id { get; set; }
    public class DeleteMipCommandHandler : IRequestHandler<DeleteMipCommand, ReturnModel<Mip>>
    {
        private readonly IMipRepository _mipRepository;
        private readonly IMapper _mapper;


        // private readonly ICacheService _cacheService;
        //private readonly IMailService _mailService;


        public DeleteMipCommandHandler(
            IMipRepository mipRepository,
            IMapper mapper)
        {
            _mipRepository = mipRepository;
            _mapper = mapper;
        }


        public async Task<ReturnModel<Mip>> Handle(DeleteMipCommand request, CancellationToken cancellationToken)
        {
            ReturnModel<Mip> result = new();
            try
            {
                Mip Mip = await _mipRepository.GetAsync(a => a.Id == request.id);
                result.Data = await _mipRepository.DeleteAsync(Mip);
                result.Message = "Mip başarıyla silindi.";
                result.Status = ReturnTypeStatus.Success;
            }
            catch (System.Exception ex)
            {
                result.Message = ex.Message;
                result.Status = ReturnTypeStatus.Error;
            }
            return result;
        }

    }

}

