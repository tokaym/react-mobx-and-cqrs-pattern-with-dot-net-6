using System.ComponentModel;

namespace Application.Features.MainReports.Dtos;

public class MailSettingsDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string To { get; set; }
    public string Cc { get; set; }
    public string From { get; set; }
    public string Description { get; set; }
}