namespace Application.Features.RomaniaZM20Histories.Dtos;

public class RomaniaZm20forReportDto
{
    public Guid? Id { get; set; }
    public string? MaterialSKU { get; set; }
    public string? MaterialName { get; set; }
    public int? OpenAmount { get; set; }
    public string? SatSasNo { get; set; }
    public DateTime ReleaseDate { get; set; }
}