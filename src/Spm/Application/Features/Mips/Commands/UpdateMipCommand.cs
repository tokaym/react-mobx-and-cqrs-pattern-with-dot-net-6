using Application.Models;
using Application.Services.IZm20Repository;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mips.Commands;

public class UpdateMipCommand : IRequest<ReturnModel<Mip>>
{

    public Mip mip { get; set; }
    public class UpdateMipCommandHandler : IRequestHandler<UpdateMipCommand, ReturnModel<Mip>>
    {
        private readonly IMipRepository _mipRepository;
        private readonly IMapper _mapper;


        public UpdateMipCommandHandler(
            IMipRepository mipRepository,
            IMapper mapper
           )
        {
            _mipRepository = mipRepository;
            _mapper = mapper;
        }


        public async Task<ReturnModel<Mip>> Handle(UpdateMipCommand request, CancellationToken cancellationToken)
        {
            ReturnModel<Mip> result = new();
            try
            {
                result.Data = await _mipRepository.UpdateAsync(request.mip);
                result.Message = "Mip başarıyla güncellendi.";
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

