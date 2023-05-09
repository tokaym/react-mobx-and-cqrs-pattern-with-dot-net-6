using System.ComponentModel;

namespace Application.Features.MainReports.Dtos;

public class BarChartDto
{
    [Description("Adı")]
    public string Name { get; set; }

    [Description("Cari")]
    public int? Cari { get; set; }

    [Description("Demode")]
    public int? Demode { get; set; }

    [Description("Toplam")]
    public int? Toplam { get; set; }

    [Description("Oran")]
    public string? Oran { get; set; }

}