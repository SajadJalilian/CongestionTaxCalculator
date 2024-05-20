using CongestionTaxCalculator.Api.Common.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTaxCalculator.Api.Features.CongestionTax;

public static class CongestionTaxEndpoints
{
    public static IEndpointRouteBuilder GetCongestionTaxEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/",
            async ([FromBody] GetTaxRequest request, CongestionTaxService service,
                CancellationToken cancellationToken) =>
            {
                var tax = await service.GetTax(request.VehicleType, request.Dates, cancellationToken);
                return Results.Ok(tax);
            }).Validator<GetTaxRequest>();

        return endpoint;
    }
}