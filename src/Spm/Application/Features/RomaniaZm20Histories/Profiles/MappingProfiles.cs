using Application.Features.MainReports.Dtos;
using Application.Features.RomaniaZM20Histories.Dtos;
using Application.Features.ZM20Histories.Commands;
using AutoMapper;
using Core.Application;
using Domain.Entities;
using MediatR;

namespace Application.Features.RomaniaZm20Histories.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<bool, CreateRomaniaZm20HistoryCommand>().ReverseMap();
            CreateMap<bool, DeleteRomaniaZm20HistoryCommand>().ReverseMap();
            CreateMap<RomaniaZm20, RomaniaZm20History>().ReverseMap();
            CreateMap<RomaniaZm20, RomaniaZm20forReportDto>().ReverseMap();
        }
    }
}