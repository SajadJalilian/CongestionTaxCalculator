namespace CongestionTaxCalculator.Core;

public static class CongestionTaxCalculator
{
    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total congestion tax for that day
     */
    public static int GetTax(VehicleType vehicleType, DateTime[] dates, DateTime[] holidayDates)
    {
        if (VehicleHelper.IsTollFreeVehicle(vehicleType)) return 0;
        if (dates.Length == 0) return 0;
        var taxedDates = TaxDateHelper.GetTaxedDates(dates, holidayDates);
        if (taxedDates.Length == 0) return 0;

        DateTime intervalStart = taxedDates[0];
        int totalFee = 0;
        foreach (DateTime date in taxedDates)
        {
            if (totalFee >= CoreConstants.TotalFee) break;

            int nextFee = TollFeeHelper.GetTollFee(date, vehicleType);
            int tempFee = TollFeeHelper.GetTollFee(intervalStart, vehicleType);

            var diff = date - intervalStart;
            var minutes = diff.TotalMinutes;

            if (minutes <= 60)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                intervalStart = date;
                totalFee += nextFee;
            }
        }

        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }
}