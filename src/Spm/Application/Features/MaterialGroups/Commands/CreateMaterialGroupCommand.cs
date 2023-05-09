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

namespace Application.Features.MaterialGroups.Commands;

public class CreateMaterialGroupCommand : IRequest<ReturnModel<MaterialGroup>>
{

    public MaterialGroup materialGroup { get; set; }
    public class CreateMaterialGroupCommandHandler : IRequestHandler<CreateMaterialGroupCommand, ReturnModel<MaterialGroup>>
    {
        private readonly IMaterialGroupRepository _materialGroupRepository;
        private readonly IMapper _mapper;


        public CreateMaterialGroupCommandHandler(
            IMaterialGroupRepository materialGroupRepository,
            IMapper mapper
           )
        {
            _materialGroupRepository = materialGroupRepository;
            _mapper = mapper;
        }


        public async Task<ReturnModel<MaterialGroup>> Handle(CreateMaterialGroupCommand request, CancellationToken cancellationToken)
        {
            ReturnModel<MaterialGroup> result = new();
            try
            {
                if (!await _materialGroupRepository.AnyAsync(a => a.Id == request.materialGroup.Id))
                {
                    result.Data = await _materialGroupRepository.AddAsync(request.materialGroup);
                    result.Message = "Malzeme Grubu başarıyla kayıt edildi.";
                    result.Status = ReturnTypeStatus.Success;
                }
                else
                {
                    result.Message = "Bu Malzeme SKU ile daha önceden kayıt oluşturulmuştur!";
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

