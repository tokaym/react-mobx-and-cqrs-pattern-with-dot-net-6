using Core.Persistence;

namespace Domain.Entities;
public class RomaniaZm20 : Entity
{
    public string? MaterialSat { get; set; }
    public int? Item { get; set; }
    public string? TrmSt { get; set; }
    public string? StBu { get; set; }
    public string? SalesInf { get; set; }
    public int? Item2 { get; set; }
    public string? SaRequest { get; set; }
    public int? Item3 { get; set; }
    public string? Orderer { get; set; }
    public string? Ad1 { get; set; }
    public string? UY { get; set; }
    public string? DPYR { get; set; }
    public string? UY2 { get; set; }
    public string? Sag { get; set; }
    public string? Definition { get; set; }
    public string? MaterialNo { get; set; }
    public string? MaterialName { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public DateTime? ArrivalDate { get; set; }
    public DateTime? ConfirmationDate { get; set; }
    public int? TermQuantity { get; set; }
    public int? Delivered { get; set; }
    public string? Unit { get; set; }
    public int? OpenQuantity { get; set; }
    public string? Suply { get; set; }
    public string? Mip { get; set; }
    public string? TesMip { get; set; }
    public string? Cd { get; set; }
    public string? SrvRef { get; set; }
    public string? Quality { get; set; }
    public string? AlanUYEm { get; set; }
    public string? AlanUYY { get; set; }
    public string? Star { get; set; }
    public string? TahKlm { get; set; }
    public string? Seller { get; set; }
    public string? Ad11 { get; set; }
    public string? Alternative { get; set; }
    public string? SasItem { get; set; }


    public RomaniaZm20()
    {
    }

    public RomaniaZm20(Guid id, string? materialSat, int? item, string? trmSt, string? stBu, string? salesInf, int? item2, string saRequest,
    int? item3, string? orderer, string? ad1, string? uy, string? dpyr, string? uy2, string? sag, string? definition, string? materialNo,
    string? materialName, DateTime? releaseDate, DateTime? arrivalDate, DateTime? confirmationDate, int? termQuantity, int? delivered, string? unit,
    int? openQuantity, string? suply, string? mip, string? tesMip, string? cd, string? srvRef, string? quality, string? alanUyEm, string? alanUYY,
    string? star, string? tahKlm, string? seller, string? ad11, string? alternative, string? sasItem) : base(id)
    {
        Id = id;
        MaterialSat = materialSat;
        Item = item;
        TrmSt = trmSt;
        StBu = stBu;
        SalesInf = salesInf;
        Item2 = item2;
        SaRequest = saRequest;
        Item3 = item3;
        Orderer = orderer;
        Ad1 = ad1;
        UY = uy;
        DPYR = dpyr;
        UY2 = uy2;
        Sag = sag;
        Definition = definition;
        MaterialNo = materialNo;
        MaterialName = materialName;
        ReleaseDate = releaseDate;
        ArrivalDate = arrivalDate;
        ConfirmationDate = confirmationDate;
        TermQuantity = termQuantity;
        Delivered = delivered;
        Unit = unit;
        OpenQuantity = openQuantity;
        Suply = suply;
        Mip = mip;
        TesMip = tesMip;
        Cd = cd;
        SrvRef = srvRef;
        Quality = quality;
        AlanUYEm = alanUyEm;
        AlanUYY = alanUYY;
        Star = star;
        TahKlm = tahKlm;
        Seller = seller;
        Ad11 = ad11;
        Alternative = alternative;
        SasItem = sasItem;
    }
}