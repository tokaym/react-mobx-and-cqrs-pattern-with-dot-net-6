using System.ComponentModel;

namespace Application.Features.MainReports.Dtos;

public class PieChartDto
{
    [Description("Adı")]
    public string Name { get; set; }

    [Description("Değer")]
    public int? Value { get; set; }
}