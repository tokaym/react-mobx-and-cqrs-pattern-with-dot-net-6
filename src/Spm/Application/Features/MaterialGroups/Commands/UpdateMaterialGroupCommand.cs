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

public class UpdateMaterialGroupCommand : IRequest<ReturnModel<MaterialGroup>>
{

    public MaterialGroup materialGroup { get; set; }
    public class UpdateMaterialGroupCommandHandler : IRequestHandler<UpdateMaterialGroupCommand, ReturnModel<MaterialGroup>>
    {
        private readonly IMaterialGroupRepository _materialGroupRepository;
        private readonly IMapper _mapper;


        public UpdateMaterialGroupCommandHandler(
            IMaterialGroupRepository materialGroupRepository,
            IMapper mapper
           )
        {
            _materialGroupRepository = materialGroupRepository;
            _mapper = mapper;
        }


        public async Task<ReturnModel<MaterialGroup>> Handle(UpdateMaterialGroupCommand request, CancellationToken cancellationToken)
        {
            ReturnModel<MaterialGroup> result = new();
            try
            {
                result.Data = await _materialGroupRepository.UpdateAsync(request.materialGroup);
                result.Message = "Malzeme grubu başarı ile güncellendi.";
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

