using FluentAssertions;

namespace CongestionTaxCalculator.Core.Unit;

public class CongestionTaxCalculatorTest
{
    private readonly List<DateTime> _holidays = new()
    {
        new DateTime(2013, 01, 01),
        new DateTime(2013, 01, 06),
        new DateTime(2013, 03, 29),
        new DateTime(2013, 03, 31),
        new DateTime(2013, 04, 01),
        new DateTime(2013, 05, 01),
        new DateTime(2013, 05, 09),
        new DateTime(2013, 05, 19),
        new DateTime(2013, 06, 06),
        new DateTime(2013, 06, 22),
        new DateTime(2013, 07, 02),
        new DateTime(2013, 12, 25),
        new DateTime(2013, 12, 26),
    };

    private readonly List<DateTime> _vehicleRecordsOneFourteen = new()
    {
        new DateTime(2013, 1, 14, 21, 0, 0),
    };

    private readonly List<DateTime> _vehicleRecordsOneFifteen = new()
    {
        new DateTime(2013, 1, 15, 21, 0, 0),
    };

    private readonly List<DateTime> _vehicleRecordsTwoSeven = new()
    {
        new DateTime(2013, 2, 7, 6, 23, 27),
        new DateTime(2013, 2, 7, 15, 27, 00),
    };

    private readonly List<DateTime> _vehicleRecordsTwoEight = new()
    {
        new DateTime(2013, 2, 8, 6, 27, 00),
        new DateTime(2013, 2, 8, 6, 20, 27),
        new DateTime(2013, 2, 8, 14, 35, 00),
        new DateTime(2013, 2, 8, 15, 29, 00),
        new DateTime(2013, 2, 8, 15, 47, 00),
        new DateTime(2013, 2, 8, 16, 01, 00),
        new DateTime(2013, 2, 8, 16, 48, 00),
        new DateTime(2013, 2, 8, 17, 49, 00),
        new DateTime(2013, 2, 8, 18, 29, 00),
        new DateTime(2013, 2, 8, 18, 35, 00),
    };

    private readonly List<DateTime> _vehicleRecordsThreeTwentySix = new()
    {
        new DateTime(2013, 3, 26, 14, 25, 00),
    };

    private readonly List<DateTime> _vehicleRecordsThreeTwentyEight = new()
    {
        new DateTime(2013, 3, 28, 14, 07, 27),
    };

    // MethodName_Should_When
    [Theory]
    [InlineData(VehicleType.Diplomat)]
    [InlineData(VehicleType.Emergency)]
    [InlineData(VehicleType.Military)]
    public void GetTax_ShouldReturnZero_WhenVehicleIsNoTaxedType(VehicleType vType)
    {
        // Arrange

        // Act
        var result = CongestionTaxCalculator.GetTax(vType, [], []);

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public void GetTax_ShouldReturnZero_WhenDatesAreEmpty()
    {
        // Arrange
        var vType = VehicleType.Diplomat;

        // Act
        var result = CongestionTaxCalculator.GetTax(vType, [], []);

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public void GetTax_ShouldReturnEight_WhenVehicleIsOther_AndHasOneDayRecordAt629()
    {
        // Arrange
        var vType = VehicleType.Other;
        var records = new List<DateTime>()
        {
            _holidays[2],
            new DateTime(2013, 04, 09, 6, 27, 0),
        };

        // Act
        var result = CongestionTaxCalculator.GetTax(vType, records.ToArray(), _holidays.ToArray());

        // Assert
        result.Should().Be(8);
    }

    [Fact]
    public void GetTax_ShouldReturnZero_WhenVehicleIsFreeVehicle_AndHasOneDayRecordAt629()
    {
        // Arrange
        var vType = VehicleType.Diplomat;
        var records = new List<DateTime>()
        {
            _holidays[2],
            new DateTime(2013, 04, 09, 6, 27, 0),
        };

        // Act
        var result = CongestionTaxCalculator.GetTax(vType, records.ToArray(), _holidays.ToArray());

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public void GetTax_ShouldReturn29_WhenVehicleIsTaxed()
    {
        // Arrange
        var vType = VehicleType.Other;
        var records = new List<DateTime>()
        {
            _holidays[2],
            new DateTime(2013, 04, 09, 6, 28, 0), // 8
            new DateTime(2013, 04, 09, 6, 58, 0), // 13
            new DateTime(2013, 04, 09, 8, 31, 0), // 8
            _holidays[3],
            new DateTime(2013, 04, 09, 14, 01, 0), // 8
        };

        // Act
        var result = CongestionTaxCalculator.GetTax(vType, records.ToArray(), _holidays.ToArray());

        // Assert
        result.Should().Be(29);
    }

    [Fact]
    public void GetTax_ShouldReturn57_WhenVehicleIsTaxed()
    {
        // Arrange
        var vType = VehicleType.Other;
        var records = new List<DateTime>()
        {
            _holidays[2],
            new DateTime(2013, 04, 09, 7, 50, 0), // 18
            new DateTime(2013, 04, 09, 8, 28, 0), // 13
            new DateTime(2013, 04, 09, 15, 38, 0), // 18
            _holidays[4],
            new DateTime(2013, 04, 09, 16, 53, 0), // 18
        };

        // Act
        var result = CongestionTaxCalculator.GetTax(vType, records.ToArray(), _holidays.ToArray());

        // Assert
        result.Should().Be(54);
    }

    [Fact]
    public void GetTollFee_SampleDataTwoEight()
    {
        // Arrange
        var vType = VehicleType.Other;

        // Act
        var result = CongestionTaxCalculator.GetTax(vType, _vehicleRecordsTwoEight.ToArray(), _holidays.ToArray());

        // Assert
        result.Should().Be(60);
    }

    [Fact]
    public void GetTollFee_SampleDataTwoSeven()
    {
        // Arrange
        var vType = VehicleType.Other;

        // Act
        var result = CongestionTaxCalculator.GetTax(vType, _vehicleRecordsTwoSeven.ToArray(), _holidays.ToArray());

        // Assert
        result.Should().Be(21);
    }

    public static TheoryData<DateTime> HourSix6To629Data() =>
        new()
        {
            new DateTime(2013, 01, 01, 6, 00, 0),
            new DateTime(2013, 01, 01, 6, 09, 0),
            new DateTime(2013, 01, 01, 6, 20, 0),
            new DateTime(2013, 01, 01, 6, 29, 33),
        };

    [Theory]
    [MemberData(nameof(HourSix6To629Data))]
    public void GetTollFee_ShouldReturn8_WhenTimeIs6to629(DateTime time)
    {
        // Arrange
        var vType = VehicleType.Other;

        // Act
        var result = TollFeeHelper.GetTollFee(time, vType);

        // Assert
        result.Should().Be(8);
    }

    public static TheoryData<DateTime> HourSix30To50Data() =>
        new()
        {
            new DateTime(2013, 01, 01, 6, 30, 0),
            new DateTime(2013, 01, 01, 6, 39, 0),
            new DateTime(2013, 01, 01, 6, 40, 0),
            new DateTime(2013, 01, 01, 6, 59, 33),
        };

    [Theory]
    [MemberData(nameof(HourSix30To50Data))]
    public void GetTollFee_ShouldReturn13_WhenTimeIs630to659(DateTime time)
    {
        // Arrange
        var vType = VehicleType.Other;

        // Act
        var result = TollFeeHelper.GetTollFee(time, vType);

        // Assert
        result.Should().Be(13);
    }
}