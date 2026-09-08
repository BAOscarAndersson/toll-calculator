using TollFeeCalculatorLibrary;

namespace TollFeeCalculatorTests;

public class TollCalculatorMultipleDatesTests
{
    private readonly TollCalculator sut;
    private readonly TestVehicle car;
    private readonly TestVehicle tollFree;

    public TollCalculatorMultipleDatesTests()
    {
        sut = new TollCalculator();
        car = new TestVehicle("Car");
        tollFree = new TestVehicle(TollCalculator.TollFreeVehicles.Emergency.ToString());
    }

    [Fact]
    public void ShouldSumFeesFromDifferentHours()
    {
        var dates = new[]
        {
            new DateTime(2023, 10, 10, 7, 0, 0),  // 18 SEK
            new DateTime(2023, 10, 10, 15, 30, 0) // 18 SEK
        };

        int result = sut.GetTollFee(car, dates);

        Assert.Equal(36, result);
    }

    [Fact]
    public void SameHour_ShouldOnlyChargeOnceAtHighestRate()
    {
        var dates = new[]
        {
            new DateTime(2023, 10, 10, 7, 10, 0),
            new DateTime(2023, 10, 10, 7, 40, 0)
        };

        int result = sut.GetTollFee(car, dates);

        Assert.Equal(18, result);
    }

    [Fact]
    public void ShouldCapAtSixtySek()
    {
        var dates = new[]
        {
            new DateTime(2023, 10, 10, 7, 0, 0),
            new DateTime(2023, 10, 10, 15, 30, 0),
            new DateTime(2023, 10, 10, 16, 0, 0),
            new DateTime(2023, 10, 10, 17, 0, 0)
        };

        int result = sut.GetTollFee(car, dates);

        Assert.Equal(60, result);
    }

    [Fact]
    public void ShouldThrowExceptionIfSpanningMultipleDays()
    {
        DateTime[]? dates =
        [
            new DateTime(2023, 10, 10, 10, 0, 0),
            new DateTime(2023, 10, 11, 10, 0, 0) // Precondition violation
        ];

        Assert.Throws<Exception>(() => sut.GetTollFee(car, dates));
    }

    [Fact]
    public void HolidayLogic_ShouldReturnZero_OnlyIn2013()
    {
        DateTime holiday2013 = new (2013, 1, 1, 12, 0, 0);
        DateTime holiday2024 = new (2026, 1, 1, 12, 0, 0);

        int fee2013 = sut.GetTollFee(holiday2013, car);
        int fee2023 = sut.GetTollFee(holiday2024, car);

        Assert.Equal(0, fee2013);
        Assert.Equal(0, fee2023);
    }
}