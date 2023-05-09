using Application.Features.MainReports.Dtos;
using Application.Models;
using Application.Services.ISettingRepository;
using Application.Services.IZm20Repository;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mips.Commands;

public class UpdateMailSettingsCommand : IRequest<ReturnModel<MailSettingsDto>>
{

    public MailSettingsDto mailSettingsDto { get; set; }
    public class UpdateMailSettingsCommandHandler : IRequestHandler<UpdateMailSettingsCommand, ReturnModel<MailSettingsDto>>
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IMapper _mapper;


        public UpdateMailSettingsCommandHandler(
            ISettingRepository settingRepository,
            IMapper mapper
           )
        {
            _settingRepository = settingRepository;
            _mapper = mapper;
        }


        public async Task<ReturnModel<MailSettingsDto>> Handle(UpdateMailSettingsCommand request, CancellationToken cancellationToken)
        {
            ReturnModel<MailSettingsDto> result = new();
            Setting setting = new Setting
            {
                Id = new Guid(request.mailSettingsDto.Id),
                Name = request.mailSettingsDto.Name,
                Description = request.mailSettingsDto.Description,
                Value = request.mailSettingsDto.To + "|" + request.mailSettingsDto.Cc + "|" + request.mailSettingsDto.From
            };
            try
            {
                await _settingRepository.UpdateAsync(setting);
                result.Data = request.mailSettingsDto;
                result.Message = "Mail ayarları başarıyla güncellendi.";
                result.Status = ReturnTypeStatus.Success;
            }
            catch (System.Exception ex)
            {
                result.Message = ex.Message;
                result.Status = ReturnTypeStatus.Error;
            }
            return result;
        }

    }

}

