using Application.Features.MainReports.Dtos;
using Application.Features.ZM20Histories.Commands;
using AutoMapper;
using Core.Application;
using Domain.Entities;
using MediatR;

namespace Application.Features.ZM20Histories.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<bool, CreateZm20HistoryCommand>().ReverseMap();
            CreateMap<bool, DeleteZm20HistoryCommand>().ReverseMap();
            CreateMap<Zm20, Zm20History>().ReverseMap();
            CreateMap<Zm20, Zm20forReportDto>().ReverseMap();
        }
    }
}