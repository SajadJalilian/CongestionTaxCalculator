using FluentValidation;

namespace CongestionTaxCalculator.Api.Features.CongestionTax;

public class GetTaxRequestValidator : AbstractValidator<GetTaxRequest>
{
    public GetTaxRequestValidator()
    {
        RuleFor(x => x.VehicleType)
            .IsInEnum()
            .WithMessage("VehicleType is not valid");

        RuleFor(x => x.Dates)
            .ForEach(x => x.SetValidator(new DateValidator()));
    }
}

public class DateValidator : AbstractValidator<DateTime>
{
    public DateValidator()
    {
        RuleFor(x => x.Year)
            .Must(x => x == 2013)
            .WithMessage("Year must be 2013!!");
    }
}