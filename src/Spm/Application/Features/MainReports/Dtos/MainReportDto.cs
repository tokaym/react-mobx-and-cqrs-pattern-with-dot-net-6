namespace Application.Features.MainReports.Dtos;

public class MainReportDto
{
    public Guid Id { get; set; }
    public string? MaterialSKU { get; set; }
    public string? MaterialName { get; set; }
    public int? OpenAmount { get; set; }
    public int? Item { get; set; }
    public int? HF { get; set; }
    public int? Urgent { get; set; }
    public DateTime? FirstOrderDate { get; set; }
    public string? Company { get; set; }
    public string? ProductClass { get; set; }
    public string? CD { get; set; }
    public int? Stock { get; set; }
    public int? SasCloses { get; set; }
    public int? UrgentCloses { get; set; }
    public int? HfCloses { get; set; }
    public int? ThStock { get; set; }
    public string? Mip { get; set; }
    public string? MipLiable { get; set; }
    public int? Sent { get; set; }
    public string? TT { get; set; }
    public DateTime? ReportDate { get; set; }
    public DateTime? CreatedTime {get;set;}
    public Guid UserId {get;set;}
    public Guid PlantId {get;set;}
}