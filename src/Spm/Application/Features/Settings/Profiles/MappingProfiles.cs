using Application.Features.MainReports.Dtos;
using Application.Features.Mips.Commands;
using Application.Models;
using AutoMapper;

namespace Application.Features.Settings.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ReturnModel<MailSettingsDto>, UpdateMailSettingsCommand>().ReverseMap();
        }
    }
}