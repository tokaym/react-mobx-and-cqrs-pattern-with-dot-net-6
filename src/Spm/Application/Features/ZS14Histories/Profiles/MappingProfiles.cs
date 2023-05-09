using Application.Features.Zs14Histories.Commands;
using AutoMapper;
using Core.Application;
using Domain.Entities;
using MediatR;

namespace Application.Features.Zs14Histories.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Zs14History, CreateZs14HistoryCommand>().ReverseMap();
            CreateMap<Result<Unit>, DeleteZs14HistoryCommand>().ReverseMap();
        }
    }
}