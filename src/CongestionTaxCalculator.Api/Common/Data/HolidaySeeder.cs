using CongestionTaxCalculator.Api.Features.CongestionTax;
using Newtonsoft.Json;

namespace CongestionTaxCalculator.Api.Common.Data;

public class HolidaySeeder
{
    public async Task Seed(ApplicationDbContext appDbContext)
    {
        if (File.Exists(ApiConstants.FilePath))
        {
            var servicesData = await File.ReadAllTextAsync(ApiConstants.FilePath);
            var jsonHolidays = JsonConvert.DeserializeObject<List<Holiday>>(servicesData, new JsonSerializerSettings
            {
                DateFormatString = "YYYY-MM-DD"
            });

            foreach (var jsonHoliday in jsonHolidays)
            {
                var date = appDbContext.Holidays.FirstOrDefault(x => x.Date.Year == jsonHoliday.Date.Year
                                                                     || x.Date.Month == jsonHoliday.Date.Month
                                                                     || x.Date.Day == jsonHoliday.Date.Day);
                if (date is not null) continue;

                await appDbContext.Holidays.AddAsync(new Holiday
                {
                    Date = jsonHoliday.Date,
                    Name = jsonHoliday.Name
                });
            }

            await appDbContext.SaveChangesAsync();
        }
    }
}

internal static class SeedMiddleware
{
    public static async void UseSeeder(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var holidaySeeder = new HolidaySeeder();
        await holidaySeeder.Seed(dbContext);
    }
}