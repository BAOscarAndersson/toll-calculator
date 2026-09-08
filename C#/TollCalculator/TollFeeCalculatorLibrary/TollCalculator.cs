namespace TollFeeCalculatorLibrary;

public class TollCalculator
{
    readonly string[] TollFreeVehicleStrings;

    public TollCalculator()
    {
        TollFreeVehicleStrings = Enum
            .GetValues<TollFreeVehicles>()
            .Select(x => x.ToString())
            .ToArray();
    }

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

    public int GetTollFee(Vehicle vehicle, DateTime[] dates)
    {
        DateTime intervalStart = dates[0];
        int totalFee = 0;
        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(date, vehicle);
            int tempFee = GetTollFee(intervalStart, vehicle);

            long diffInMillies = date.Millisecond - intervalStart.Millisecond;
            long minutes = diffInMillies / 1000 / 60;

            if (minutes <= 60)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    public int GetTollFee(DateTime date, Vehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        int hour = date.Hour;
        int minute = date.Minute;

        return GetTollFee(hour, minute);
    }

    int GetTollFee(int hour, int minute)
    {
        // Convert everything to total minutes from midnight
        int m = (hour * 60) + minute;

        return m switch
        {
            >= 360 and < 390 => 8,  // 06:00 - 06:29
            >= 390 and < 420 => 13, // 06:30 - 06:59
            >= 420 and < 480 => 18, // 07:00 - 07:59
            >= 480 and < 510 => 13, // 08:00 - 08:29
            >= 510 and < 540 => 8,  // 08:30 - 08:59
            >= 570 and < 600 => 8,  // 09:30 - 09:59
            >= 630 and < 660 => 8,  // 10:30 - 10:59
            >= 690 and < 720 => 8,  // 11:30 - 11:59
            >= 750 and < 780 => 8,  // 12:30 - 12:59
            >= 810 and < 840 => 8,  // 13:30 - 13:59
            >= 870 and < 900 => 8,  // 14:30 - 14:59
            >= 900 and < 930 => 13, // 15:00 - 15:29
            >= 930 and < 1020 => 18, // 15:30 - 16:59
            >= 1020 and < 1080 => 13, // 17:00 - 17:59
            >= 1080 and < 1110 => 8,  // 18:00 - 18:29
            _ => 0
        };
    }

    bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;

        return TollFreeVehicleStrings.Contains(vehicle.GetVehicleType());
    }

    static bool IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday) 
            return true;

        /*
         * This seems very strange, the requirment states that holidays
         * are toll free, but here it is only for the year 2013.
         * Check with stakeholders what the intension is,
         * since it would be a really bad breaking change to fix this.
         */
        if (year == 2013)
        {
            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
        }
        return false;
    }

    public enum TollFreeVehicles
    {
        Motorbike = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }
}