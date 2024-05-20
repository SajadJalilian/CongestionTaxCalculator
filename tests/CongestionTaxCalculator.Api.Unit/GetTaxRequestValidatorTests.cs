using CongestionTaxCalculator.Api.Features.CongestionTax;
using CongestionTaxCalculator.Core;
using FluentValidation.TestHelper;

namespace CongestionTaxCalculator.Api.Unit;

public class GetTaxRequestValidatorTests
{
    [Fact]
    public void GetTaxRequestValidator_ShouldReturnError_WhenVehicleTypeIsEmpty()
    {
        // Arrange
        var badVehicle = (VehicleType)10;
        var model = new GetTaxRequest(badVehicle, new[] { new DateTime(2013, 1, 1) });
        var validator = new GetTaxRequestValidator();

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.VehicleType);
    }

    [Fact]
    public void DateValidator_ShouldHaveError_WhenDateIsNot2013()
    {
        // Arrange
        var model = new DateTime(2014, 1, 1);
        var validator = new DateValidator();

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Year);
    }
}