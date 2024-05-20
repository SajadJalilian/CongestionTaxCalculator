namespace CongestionTaxCalculator.Core;

public static class TaxDateHelper
{
    public static DateTime[] GetTaxedDates(DateTime[] dates, DateTime[] holidayDates)
    {
        return dates.Where(date => !IsTollFreeDate(date, holidayDates)).ToArray();
    }

    public static bool IsTollFreeDate(DateTime date, DateTime[] holidayDates)
    {
        var onlyDate = new DateTime(date.Year, date.Month, date.Day);
        if (onlyDate.Month == 7) return true; // Month July
        if (onlyDate.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday) return true; // Weekends
        if (holidayDates.Contains(onlyDate)) return true; // Holiday
        if (holidayDates.Contains(onlyDate.AddDays(1))) return true; // One day before holiday

        return false;
    }
}