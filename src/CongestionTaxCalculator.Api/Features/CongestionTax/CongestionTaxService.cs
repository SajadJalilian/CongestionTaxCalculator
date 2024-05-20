using CongestionTaxCalculator.Api.Common.Data;
using CongestionTaxCalculator.Core;
using Microsoft.EntityFrameworkCore;

namespace CongestionTaxCalculator.Api.Features.CongestionTax;

public class CongestionTaxService
{
    private readonly ApplicationDbContext _dbContext;

    public CongestionTaxService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> GetTax(VehicleType vehicleType, DateTime[] dates, CancellationToken token)
    {
        var holidays = await _dbContext.Holidays.ToArrayAsync();
        var holidayDates = holidays.Select(x => x.Date);
        return CongestionTaxCalculator.Core.CongestionTaxCalculator.GetTax(vehicleType, dates, holidayDates.ToArray());
    }
}