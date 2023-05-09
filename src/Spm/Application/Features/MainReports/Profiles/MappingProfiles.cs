using Application.Features.MainReports.Commands;
using Application.Features.MainReports.Dtos;
using Application.Features.Users.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
namespace Application.Features.MainReports.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<bool, CreateMainReportCommand>().ReverseMap();
            CreateMap<MainReport, MainReportDto>().ReverseMap();
            CreateMap<MainReport, MainReportListDto>().ReverseMap();
            CreateMap<IPaginate<MainReport>, MainReportListModel>().ReverseMap();
        }
    }
}