using Application.Services.IZm20Repository;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Application;
using Domain.Entities;
using MediatR;
using Application.Services.Repositories;
using Application.Models;

namespace Application.Features.MaterialGroups.Commands;

public class DeleteMaterialGroupCommand : IRequest<ReturnModel<MaterialGroup>>
{
    public Guid id { get; set; }
    public class DeleteMaterialGroupCommandHandler : IRequestHandler<DeleteMaterialGroupCommand, ReturnModel<MaterialGroup>>
    {
        private readonly IMaterialGroupRepository _materialGroupRepository;
        private readonly IMapper _mapper;


        // private readonly ICacheService _cacheService;
        //private readonly IMailService _mailService;


        public DeleteMaterialGroupCommandHandler(
            IMaterialGroupRepository materialGroupRepository,
            IMapper mapper)
        {
            _materialGroupRepository = materialGroupRepository;
            _mapper = mapper;
        }


        public async Task<ReturnModel<MaterialGroup>> Handle(DeleteMaterialGroupCommand request, CancellationToken cancellationToken)
        {
            ReturnModel<MaterialGroup> result = new();
            try
            {
                MaterialGroup materialGroup = await _materialGroupRepository.GetAsync(a => a.Id == request.id);
                result.Data = await _materialGroupRepository.DeleteAsync(materialGroup);
                result.Message = "Malzeme grubu başarı ile silindi.";
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

