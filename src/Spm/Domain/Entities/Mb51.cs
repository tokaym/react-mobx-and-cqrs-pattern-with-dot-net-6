using Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public class Mb51 : Entity
{
    public string? MaterialSKU { get; set; }
    public string? MaterialName { get; set; }
    public string? Reference { get; set; }
    public DateTime? RegisterDate { get; set; }
    public int? Amount { get; set; }
    public string? ITU { get; set; }
    public int? Dpyr { get; set; }
    public string? HrkTUMtn { get; set; }
    public string? MaterialInfo { get; set; }
    public int? Item { get; set; }
    public string? Customer { get; set; }
    public string? SatSasNo { get; set; }
    public Mb51()
    {

    }

    public Mb51(Guid id, string materialSKU, string materialName, string reference, DateTime registerDate, int amount, string itu, int dpyr, string hrkTUMtn, string materialInfo, int item, string customer, string satSasNo) : base(id)
    {
        MaterialSKU = materialSKU;
        MaterialName = materialName;
        Reference = reference;
        RegisterDate = registerDate;
        Amount = amount;
        ITU = itu;
        Dpyr = dpyr;
        HrkTUMtn = hrkTUMtn;
        MaterialInfo = materialInfo;
        Item = item;
        Customer = customer;
        SatSasNo = satSasNo;
    }
}

