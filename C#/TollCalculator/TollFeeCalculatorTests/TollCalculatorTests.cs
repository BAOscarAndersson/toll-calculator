using TollFeeCalculatorLibrary;

namespace TollFeeCalculatorTests;

public class TollCalculatorTests
{
    readonly (Vehicle vehicle, DateTime[] date)[] testData;
    readonly TollCalculator sut;

    public TollCalculatorTests()
    {
        IEnumerable<Vehicle> vehicles = TestInputs.Vehicles();

        IEnumerable<DateTime[]> dates = TestInputs.DateTimes();

        testData = vehicles
            .SelectMany(_ => dates, (vehicle, date) => (vehicle, date))
            .ToArray();

        sut = new();
    }

    [Fact]
    public void GetTollFeeVehicleAndDatesCharacterization()
    {
        int[] outputs = testData
            .Select(x => sut.GetTollFee(x.vehicle, x.date))
            .ToArray();

        int[] expected = TestOutputs.VehicleAndDates();

        Assert.Equal(outputs, expected);
    }

    [Fact]
    public void GetTollFeeDateAndVehicleCharacterization()
    {
        int[] outputs = testData
            .Select(x => sut.GetTollFee(x.date[0], x.vehicle))
            .ToArray();

        int[] expected = TestOutputs.DateAndVehicle();

        Assert.Equal(outputs, expected);
    }

    [Fact]
    public void GetTollFeeVehicleAndDatesThrows_WhenDatesIsEmpty()
    {
        Assert.Throws<IndexOutOfRangeException>(() =>
        {
            int t = sut.GetTollFee(new TestVehicle(""), []);
        });
    }
}
