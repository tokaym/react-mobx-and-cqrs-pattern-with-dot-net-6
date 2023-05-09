namespace Application.Models;

public class ForcastMonthModel
{
    public int Month { get; set; } = 0;
    public string MonthName { get; set; } = "";
}


public class Forcast
{
    public static List<ForcastMonthModel> GetForcastMonth()
    {
        List<ForcastMonthModel> months = new List<ForcastMonthModel>();
        months.Add(new ForcastMonthModel() { Month = 1, MonthName = "Oca." });
        months.Add(new ForcastMonthModel() { Month = 2, MonthName = "Şub." });
        months.Add(new ForcastMonthModel() { Month = 3, MonthName = "Mar." });
        months.Add(new ForcastMonthModel() { Month = 4, MonthName = "Nis." });
        months.Add(new ForcastMonthModel() { Month = 5, MonthName = "May." });
        months.Add(new ForcastMonthModel() { Month = 6, MonthName = "Haz." });
        months.Add(new ForcastMonthModel() { Month = 7, MonthName = "Tem." });
        months.Add(new ForcastMonthModel() { Month = 8, MonthName = "Ağu." });
        months.Add(new ForcastMonthModel() { Month = 9, MonthName = "Eyl." });
        months.Add(new ForcastMonthModel() { Month = 10, MonthName = "Eki." });
        months.Add(new ForcastMonthModel() { Month = 11, MonthName = "Kas." });
        months.Add(new ForcastMonthModel() { Month = 12, MonthName = "Ara." });

        return months;
    }
}