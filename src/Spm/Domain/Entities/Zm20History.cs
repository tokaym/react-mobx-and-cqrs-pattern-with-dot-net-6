using System.ComponentModel.DataAnnotations.Schema;
using Core.Persistence;
using Core.Security.Entities;

namespace Domain.Entities;
public class Zm20History : Entity
{
    public string? MaterialSat {get;set;} 
    public string? Name { get; set; }
    public string? Star { get; set; }
    public string? UY { get; set; }
    public string? UYCode { get; set; }
    public string? DY { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public DateTime? ArrivalDate { get; set; }
    public string? MaterialSKU { get; set; }
    public string? MaterialName { get; set; }
    public string? Unit { get; set; }
    public int? AmountDelivered { get; set; }
    public int? OpenAmount { get; set; }
    public int? RemainingStock { get; set; }
    public int? QualityStock { get; set; }
    public string? SatSasNo { get; set; }
    public int? Item { get; set; }
    public DateTime? ConfirmationDate { get; set; }
    public int? Mip { get; set; }
    public string? TesMip { get; set; }
    public int? SrvRef { get; set; }
    public int? AlanUYEmnStok { get; set; }
    public string? AlanUYYuvDeg { get; set; }
    public string? Empty1 { get; set; }
    public string? Empty2 { get; set; }
    public string? Empty3 { get; set; }
    public string? Empty4 { get; set; }
    public string? TT { get; set; }
    public string? Empty5 { get; set; }
    public string? Seller { get; set; }
    public string? SellerName { get; set; }
    public string? Empty6 { get; set; }
    public string? Empty7 { get; set; }
    public string? Empty8 { get; set; }
    public string? ContractNo { get; set; }
    public string? Empty9 { get; set; }
    public string? Item2 { get; set; }
    public DateTime CreatedTime { get; set; }
    [Column(TypeName = "Date")]
    public DateTime ReportDate { get; set; }
    public Guid UserId { get; set; }

    public virtual User User { get; set; }

    public Zm20History()
    {
    }

    public Zm20History(Guid id,string materialSat, string star, string uY, string uYCode, string dY, DateTime releaseDate, DateTime arrivalDate, string materialSKU, string materialName, string unit, int amountDelivered, int openAmount,
    int remainingStock, int qualityStock, string satSasNo, int item, DateTime confirmationDate, int mip, string tesMip, int srvRef, int alanUYEmnStok, string alanUYYuvDeg, string empty1, string empty2, string empty3, string empty4, string tt,
    string empty5, string seller, string sellerName, string empty6, string empty7, string empty8, string contractNo, string empty9, string item2, DateTime createdTime, DateTime reportDate, Guid userId) : base(id)
    {
        Id = id;
        MaterialSat = materialSat;
        Star = star;
        UY = uY;
        UYCode = uYCode;
        DY = dY;
        ReleaseDate = releaseDate;
        ArrivalDate = arrivalDate;
        MaterialSKU = materialSKU;
        MaterialName = materialName;
        Unit = unit;
        AmountDelivered = amountDelivered;
        OpenAmount = openAmount;
        RemainingStock = remainingStock;
        QualityStock = qualityStock;
        SatSasNo = satSasNo;
        Item = item;
        ConfirmationDate = confirmationDate;
        Mip = mip;
        TesMip = tesMip;
        SrvRef = srvRef;
        AlanUYEmnStok = alanUYEmnStok;
        AlanUYYuvDeg = alanUYYuvDeg;
        Empty1 = empty1;
        Empty2 = empty2;
        Empty3 = empty3;
        Empty4 = empty4;
        TT = tt;
        Empty5 = empty5;
        Seller = seller;
        SellerName = sellerName;
        Empty6 = empty6;
        Empty7 = empty7;
        Empty8 = empty8;
        ContractNo = contractNo;
        Empty9 = empty9;
        Item2 = item2;
        CreatedTime = createdTime;
        UserId = userId;
        ReportDate = reportDate;
    }

}
