using CongestionTaxCalculator.Core;

namespace CongestionTaxCalculator.Api.Features.CongestionTax;

public record GetTaxRequest(VehicleType VehicleType, DateTime[] Dates);