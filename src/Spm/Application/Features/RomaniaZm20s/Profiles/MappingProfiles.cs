using Application.Features.RomaniaZm20s.Commands;
using Application.Features.ZM20S.Commands;
using AutoMapper;
using Core.Application;
using Domain.Entities;
using MediatR;

namespace Application.Features.RomaniaZm20s.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<bool, CreateRomaniaZm20Command>().ReverseMap();
            CreateMap<bool, DeleteRomaniaZm20Command>().ReverseMap();
        }
    }
}