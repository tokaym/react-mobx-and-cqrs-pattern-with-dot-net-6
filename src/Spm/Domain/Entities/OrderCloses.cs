using Core.Persistence;
using Core.Security.Entities;

namespace Domain.Entities;

public class OrderCloses
{
    public string MaterialSat { get; set; }
    public string MaterialSKU { get; set; }
    public string MaterialName { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime ClosedDate { get; set; }
    public int Quantity { get; set; }
}
