using Core.Persistence;
using Core.Security.Entities;

namespace Domain.Entities;

public class Estimate : Entity
{
    public string? MaterialSKU { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public int? Quantity { get; set; }
    public DateTime CreatedTime { get; set; }

    public Estimate()
    {

    }

    public Estimate(Guid id, string materialSKU, int month, int year, int quantity, DateTime createdTime) : base(id)
    {
        Id = id;
        MaterialSKU = materialSKU;
        Month = month;
        Year = year;
        Quantity = quantity;
        CreatedTime = createdTime;

    }
}