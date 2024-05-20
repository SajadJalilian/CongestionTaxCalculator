namespace CongestionTaxCalculator.Core;

public static class TollFeeHelper
{
    public static int GetTollFee(DateTime date, VehicleType vehicleType)
    {
        if (VehicleHelper.IsTollFreeVehicle(vehicleType)) return 0;

        int hour = date.Hour;
        int minute = date.Minute;

        return hour switch
        {
            6 when minute >= 0 && minute <= 29 => 8,
            6 when minute >= 30 && minute <= 59 => 13,
            7 => 18,
            8 when minute >= 0 && minute <= 29 => 13,
            8 when minute >= 30 => 8,
            >= 9 and <= 14 => 8,
            15 when minute >= 0 && minute <= 29 => 13,
            15 when minute >= 30 => 18,
            16 => 18,
            17 => 13,
            18 when minute >= 0 && minute <= 29 => 8,
            _ => 0
        };
    }
}