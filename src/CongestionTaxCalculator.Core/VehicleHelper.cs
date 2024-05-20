namespace CongestionTaxCalculator.Core;

public static class VehicleHelper
{
    public static bool IsTollFreeVehicle(VehicleType vehicle)
    {
        return vehicle switch
        {
            VehicleType.Other => false,
            _ => true
        };
    }
}