namespace Application.Features.MainReports.Dtos;

public class ReportLast3DayDto{
    public ColumnDate Date1 { get; set; }
    public ColumnDate Date2 { get; set; }
    public ColumnDate Date3 { get; set; }

    public List<ReportTable> ReportTables{get;set;}
}

public class ReportTable
{
    public string MaterialName { get; set; }
    public string MaterialSKU { get; set; }
    public int Date1Value { get; set; }
    public int Date2Value { get; set; }
    public int Date3Value { get; set; }

}

public class ColumnDate
{
    public string Name { get; set; }
    public int Sum { get; set; }
}