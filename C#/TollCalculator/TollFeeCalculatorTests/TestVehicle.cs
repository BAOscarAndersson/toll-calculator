using TollFeeCalculatorLibrary;

namespace TollFeeCalculatorTests;

internal class TestVehicle(string VehicleType) : Vehicle
{
    readonly string vehicleType = VehicleType;

    public string GetVehicleType()
    {
        return vehicleType;
    }
}
