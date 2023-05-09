using Core.Persistence;

namespace Domain.Entities;


public class Zs14 : Entity
{
    public string? Star { get; set; }
    public DateTime? InstantDate { get; set; }
    public string? TYeri { get; set; }
    public string? MaterialSKU { get; set; }
    public string? MipArea { get; set; }
    public string? MaterialName{get;set;}
    public string? Empty1 { get; set; }
    public string? Definition { get; set; }
    public int? HfOrder { get; set; }
    public int? YsOrder { get; set; }
    public int? YDKIOrder { get; set; }
    public int? YDKDOrder { get; set; }
    public int? MIhrTes { get; set; }
    public DateTime? YIIlkSip { get; set; }
    public DateTime? YDIlkSip { get; set; }
    public int? Stnkrz { get; set; }
    public int? CgrSas { get; set; }
    public int? CgrSat { get; set; }
    public int? UYAmbar { get; set; }
    public int? UYDiger { get; set; }
    public string? YP { get; set; }
    public int? IsSafety { get; set; }
    public int? MP { get; set; }
    public int? Mip { get; set; }
    public string? SG { get; set; }
    public string? Dr { get; set; }
    public string? Dr2 { get; set; }
    public DateTime? SasDelivery { get; set; }
    public DateTime? SasConfirm { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public int? Sat { get; set; }
    public int? Sas { get; set; }
    public int? Teslpln { get; set; }
    public int? ConsumptionValue { get; set; }
    public int? TransportStock { get; set; }




    public Zs14()
    {



    }

    public Zs14(Guid id, string star, DateTime instantDate, string tYeri, string materialSKU, string mipArea,string materialName, string empty1, string definition, int hfOrder,
    int ysOrder, int ydkiOrder, int ydkdOrder, int mihrTes, DateTime yilkSip, DateTime ydilkSip, int stnkrz, int cgrSas, int cgrSat, int uyAmbar, int uyDiger,
    string yp, int isSafety, int mp, int mip, string sg, string dr, string dr2, DateTime sasDelivery, DateTime sasConfirm, DateTime deadlineDate, int sat, int sas,
    int teslpln, int consumptionValue, int transportStock) : base(id)
    {
        Id = id;
        Star = star;
        InstantDate = instantDate;
        TYeri = tYeri;
        MaterialSKU = materialSKU;
        MipArea = mipArea;
        MaterialName = materialName;
        Empty1 = empty1;
        Definition = definition;
        HfOrder = hfOrder;
        YsOrder = ysOrder;
        YDKIOrder = ydkiOrder;
        YDKDOrder = ydkdOrder;
        MIhrTes = mihrTes;
        YIIlkSip = yilkSip;
        YDIlkSip = ydilkSip;
        Stnkrz = stnkrz;
        CgrSas = cgrSas;
        CgrSat = cgrSat;
        UYAmbar = uyAmbar;
        UYDiger = uyDiger;
        YP = yp;
        IsSafety = isSafety;
        MP = mp;
        Mip = mip;
        SG = sg;
        Dr = dr;
        Dr2 = dr2;
        SasDelivery = sasDelivery;
        SasConfirm = sasConfirm;
        DeadlineDate = deadlineDate;
        Sat = sat;
        Sas = sas;
        Teslpln = teslpln;
        ConsumptionValue = consumptionValue;
        TransportStock = transportStock;
    }
}