using Application.Features.MB51s.Commands;
using AutoMapper;
using Core.Application;
using Domain.Entities;
using MediatR;

namespace Application.Features.MB51s.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<bool, CreateMb51Command>().ReverseMap();
            CreateMap<bool, DeleteMb51Command>().ReverseMap();
            CreateMap<Mb51, Mb51History>().ReverseMap();
        }
    }
}