using Application.Services.ISettingRepository;
using Domain.Entities;
using MediatR;

namespace Application.Features.Settings.Profiles;

public class GetSettingsByNameQuery : IRequest<List<Setting>>
{
    public List<string> Names { get; set; }

    public class GetSettingsByNameQueryHandler : IRequestHandler<GetSettingsByNameQuery, List<Setting>>
    {
        private readonly ISettingRepository _settingRepository;

        public GetSettingsByNameQueryHandler(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public async Task<List<Setting>> Handle(GetSettingsByNameQuery request, CancellationToken cancellationToken)
        {
            List<Setting> result = new();
            foreach (string name in request.Names)
            {
                result.Add(_settingRepository.Get(a=>a.Name == name));
            }
            return result;
        }
    }
}