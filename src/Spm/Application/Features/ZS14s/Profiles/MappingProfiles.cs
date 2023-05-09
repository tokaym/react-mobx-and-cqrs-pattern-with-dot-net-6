using Application.Features.Zs14s.Commands;
using AutoMapper;
using Core.Application;
using Domain.Entities;
using MediatR;

namespace Application.Features.Zs14s.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<bool, CreateZs14Command>().ReverseMap();
            CreateMap<bool, DeleteZs14Command>().ReverseMap();
        }
    }
}