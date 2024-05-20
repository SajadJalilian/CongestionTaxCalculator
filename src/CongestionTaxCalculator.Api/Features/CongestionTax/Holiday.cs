using System.ComponentModel.DataAnnotations;

namespace CongestionTaxCalculator.Api.Features.CongestionTax;

public class Holiday
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    [MaxLength(50)] public string Name { get; set; } = string.Empty;
}