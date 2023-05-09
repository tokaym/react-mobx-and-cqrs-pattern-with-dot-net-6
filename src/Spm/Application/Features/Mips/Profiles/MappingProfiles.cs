using Application.Features.Mips.Commands;
using Application.Features.Mips.Queries;
using Application.Models;
using AutoMapper;
using Core.Application;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Mips.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ReturnModel<Mip>, CreateMipCommand>().ReverseMap();
            CreateMap<ReturnModel<Mip>, DeleteMipCommand>().ReverseMap();
            CreateMap<ReturnModel<Mip>, UpdateMipCommand>().ReverseMap();
            CreateMap<Mip, GetMipByIdQuery>().ReverseMap();
            CreateMap<IPaginate<Mip>, GetMipListQuery>().ReverseMap();
        }
    }
}