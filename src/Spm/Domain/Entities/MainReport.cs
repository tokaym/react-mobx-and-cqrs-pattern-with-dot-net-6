using System.ComponentModel.DataAnnotations.Schema;
using Core.Persistence;
using Core.Security.Entities;

namespace Domain.Entities;

public class MainReport : Entity
{
    public string? MaterialSKU { get; set; }
    public string? MaterialName { get; set; }
    public int? OpenAmount { get; set; }
    public int? Item { get; set; }
    public int? HF { get; set; }
    public int? Urgent { get; set; }
    public DateTime FirstOrderDate { get; set; }
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
    public DateTime CreatedTime { get; set; }
    [Column(TypeName = "Date")]
    public DateTime ReportDate { get; set; }

    public Guid? PlantId { get; set; }
    public Plant? Plant { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }

    public MainReport()
    {

    }

    public MainReport(Guid id, string materialSKU, string materialName, int openAmount, int item, int hf, int urgent, DateTime firstOrderDate, string company, string productClass, string cd, int stock, int sasCloses,
    int urgentCloses, int hfCloses, int thStock, string mip, string mipLiable, int sent, string tt, DateTime createdTime, DateTime reportDate, Guid userId, Guid plantId) : base(id)
    {
        Id = id;
        MaterialSKU = materialSKU;
        MaterialName = materialName;
        OpenAmount = openAmount;
        Item = item;
        HF = hf;
        Urgent = urgent;
        FirstOrderDate = firstOrderDate;
        Company = company;
        ProductClass = productClass;
        CD = cd;
        Stock = stock;
        SasCloses = sasCloses;
        UrgentCloses = urgentCloses;
        HfCloses = hfCloses;
        ThStock = thStock;
        Mip = mip;
        MipLiable = mipLiable;
        Sent = sent;
        TT = tt;
        CreatedTime = createdTime;
        ReportDate = reportDate;
        UserId = userId;
        PlantId = plantId;
    }
}