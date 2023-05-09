using System.ComponentModel;

namespace Application.Features.MainReports.Dtos;

public class EstimationDto
{  
    [Description("SKU")]
    public string? MaterialSKU { get; set; }
    [Description("Malzeme Adı")]
    public string? MaterialName { get; set; }
    [Description("CD")]
    public string? CD { get; set; }
    [Description("Tahmin")]
    public int? Estimate { get; set; }
    [Description("Sipariş")]
    public int? Order { get; set; }
    [Description("Gönderilen")]
    public int? Sent { get; set; }
    [Description("Stok")]
    public int? Stock { get; set; }
    [Description("Stok Skor")]
    public string? StockScore { get; set; }
}