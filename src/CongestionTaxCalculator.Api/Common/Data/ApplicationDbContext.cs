using CongestionTaxCalculator.Api.Features.CongestionTax;
using Microsoft.EntityFrameworkCore;

namespace CongestionTaxCalculator.Api.Common.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<Holiday> Holidays { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}