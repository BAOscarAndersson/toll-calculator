using TollFeeCalculatorLibrary;

namespace TollFeeCalculatorTests;

public class TollCalculatorTests
{
    [Fact]
    public void GetTollFeeVehicleAndDates()
    {
        var vehicles = TestInputs.Vehicles();

        var dates = TestInputs.DateTimes();

        var testData = vehicles
            .SelectMany(_ => dates, (vehicle, date) => (vehicle, date))
            .ToArray();

        var calc = new TollCalculator();

        var outputs = testData
            .Select(x => calc.GetTollFee(x.vehicle, x.date))
            .Select(x => x.ToString())
            .ToArray();

        var vehicleAndDatesPath = @".\vehicleAndDates.txt";

        string content = string.Join(",", outputs);

        File.WriteAllText(vehicleAndDatesPath, content);

        Console.WriteLine("Done");
    }

    [Fact]
    public void GetTollFeeDateAndVehicle()
    {
        var vehicles = TestInputs.Vehicles();

        var dates = TestInputs.DateTimes();
        
        var testData = vehicles
            .SelectMany(_ => dates, (vehicle, date) => (vehicle, date))
            .ToArray();

        var calc = new TollCalculator();

        var outputs = testData
            .Select(x => calc.GetTollFee(x.date[0], x.vehicle))
            .Select(x => x.ToString())
            .ToArray();

        var dateAndVehiclePath = @".\dateAndVehicle.txt";

        string content = string.Join(",", outputs);

        File.WriteAllText(dateAndVehiclePath, content);

        Console.WriteLine("Done");
    }
}
