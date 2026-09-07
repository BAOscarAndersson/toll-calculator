using TollFeeCalculatorLibrary;

namespace TollFeeCalculatorTests;

public class TollCalculatorTests
{
    [Fact]
    public void Test1()
    {
        var vehicles = GenerateVehicles()
            .Select(x => x.GetVehicleType())
            .ToArray();

        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        string vehiclesFilePath = Path.Combine(baseDirectory, "vehicles.txt");

        File.WriteAllLines(vehiclesFilePath, vehicles);

        var dates = GenerateDateTimes()
            .Select(x => string.Join(',', x))
            .ToArray();

        var datesFilePath = @".\dates.txt";

        File.WriteAllLines(datesFilePath, dates);

        Console.WriteLine("Done");
    }

    IEnumerable<DateTime[]> GenerateDateTimes()
    {
       return Enumerable
            .Range(0, 13)
            .Select(x => GenerateDateTimes(x));
    }

    DateTime[] GenerateDateTimes(int nrToGenerate)
    {
        return Enumerable
            .Range(0, nrToGenerate)
            .Select(_ => RandomDateTime())
            .ToArray();

        static DateTime RandomDateTime()
        {
            long minTicks = DateTime.MinValue.Ticks;
            long maxTicks = DateTime.MaxValue.Ticks;

            double range = (double)(maxTicks - minTicks);
            long randomOffset = (long)(Random.Shared.NextDouble() * range);

            return new DateTime(minTicks + randomOffset);
        }
    }

    IEnumerable<Vehicle> GenerateVehicles()
    {
        IEnumerable<Vehicle> tollFreeVehicles = Enum
            .GetValues<TollCalculator.TollFreeVehicles>()
            .Select(x => x.ToString())
            .Select(x => new TestVehicle(x));

        IEnumerable<Vehicle> arbitraryVehicles = RandomVehicles(103, 13);

        IEnumerable<Vehicle> t = tollFreeVehicles.Concat(arbitraryVehicles);

        return t;
    }

    IEnumerable<Vehicle> RandomVehicles(int nrToGenerate, int maxLength)
    {
        string chars = @""" !\""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~"";";

        return Enumerable
            .Range(0, nrToGenerate)
            .Select(_ => Random.Shared.GetString(chars, maxLength))
            .Select(x => new TestVehicle(x));
    }

    class TestVehicle(string VehicleType) : Vehicle
    {
        readonly string vehicleType = VehicleType;

        public string GetVehicleType()
        {
            return vehicleType;
        }
    }
}
