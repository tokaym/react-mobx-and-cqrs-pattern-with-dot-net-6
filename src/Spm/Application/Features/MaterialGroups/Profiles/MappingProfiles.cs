using Application.Features.MaterialGroups.Commands;
using Application.Features.MaterialGroups.Queries;
using Application.Models;
using AutoMapper;
using Core.Application;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.MaterialGroups.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ReturnModel<MaterialGroup>, CreateMaterialGroupCommand>().ReverseMap();
            CreateMap<ReturnModel<MaterialGroup>, DeleteMaterialGroupCommand>().ReverseMap();
            CreateMap<ReturnModel<MaterialGroup>, UpdateMaterialGroupCommand>().ReverseMap();
            CreateMap<MaterialGroup, GetMaterialGroupByIdQuery>().ReverseMap();
            CreateMap<IPaginate<MaterialGroup>, GetMaterialGroupListQuery>().ReverseMap();
        }
    }
}