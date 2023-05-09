using Application.Features.MB51Histories.Commands;
using AutoMapper;
using Core.Application;
using Domain.Entities;
using MediatR;

namespace Application.Features.MB51Histories.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Mb51History, CreateMb51HistoryCommand>().ReverseMap();
            CreateMap<bool, DeleteMb51HistoryCommand>().ReverseMap();
        }
    }
}