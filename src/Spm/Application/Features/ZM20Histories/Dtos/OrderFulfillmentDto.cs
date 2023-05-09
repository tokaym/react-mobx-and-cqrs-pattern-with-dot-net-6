namespace Application.Features.ZM20Histories.Dtos;

public class OrderFulfillmentDto
{
    public int? TotalOpenOrder { get; set; }
    public List<Zm20Material> Zm20Materials { get; set; }
}

public class Zm20Material
{
    public string MaterialName { get; set; }
    public string MaterialSKU { get; set; }
    public int Quantity { get; set; }    
    public List<MaterialSatSas> MaterialSatSass { get; set; }
}

public class MaterialSatSas
{
    public string SatSasNo { get; set; }
    public string OpenDate { get; set; }
    public string ClosedDate { get; set; }
    public int DateDayDiff { get; set; }
    public int Quantity { get; set; }
}