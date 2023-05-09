namespace Application.Features.RomaniaZM20Histories.Dtos;

public class OrderFulfillmentDto
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
    public string DateDayDiff { get; set; }
    public int Quantity { get; set; }
}