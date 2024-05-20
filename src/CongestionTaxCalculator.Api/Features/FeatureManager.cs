using CongestionTaxCalculator.Api.Features.CongestionTax;

namespace CongestionTaxCalculator.Api.Features;

public static class FeatureManager
{
    public const string EndpointTagName = "Congestion Tax";


    public static IServiceCollection ConfigureCongestionTaxFeature(this IServiceCollection services)
    {
        services.AddScoped<CongestionTaxService>();

        return services;
    }

    public static IEndpointRouteBuilder MapCongestionFeatures(this IEndpointRouteBuilder endpoint)
    {
        var groupEndpoint = endpoint.MapGroup("/tax")
            .WithTags(EndpointTagName)
            .WithDescription("Provides endpoints related to Congestion Tax Calculation");

        groupEndpoint.GetCongestionTaxEndpoint();

        return endpoint;
    }
}