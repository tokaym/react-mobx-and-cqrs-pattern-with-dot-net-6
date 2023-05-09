using Application.Features.ZM20S.Commands;
using AutoMapper;
using Core.Application;
using Domain.Entities;
using MediatR;

namespace Application.Features.ZM20S.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<bool, CreateZm20Command>().ReverseMap();
            CreateMap<bool, DeleteZm20Command>().ReverseMap();
        }
    }
}