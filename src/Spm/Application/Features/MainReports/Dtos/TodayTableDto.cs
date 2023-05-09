using System.ComponentModel;

namespace Application.Features.MainReports.Dtos;

public class TodayTableDto
{  
    [Description("Tarih")]
    public string Date { get; set; }
    [Description("Açık Miktar")]
    public int? OpenAmount { get; set; }
    [Description("Kalem")]
    public int? Item { get; set; }
    [Description("HF")]
    public int? HF { get; set; }
    [Description("Acil")]
    public int? Urgent { get; set; }
}