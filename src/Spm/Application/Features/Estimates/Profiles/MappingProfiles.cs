using Application.Features.Estimates.Commands;
using AutoMapper;

namespace Application.Features.Estimates.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<bool, CreateEstimateCommand>().ReverseMap();
            CreateMap<bool, DeleteEstimateCommand>().ReverseMap();
        }
    }
}