namespace TollFeeCalculatorLibrary;

public class TollCalculator
{
    readonly string[] TollFreeVehicleStrings;
    readonly int[] tollFee = new int[1440]; // There are 1440 minutes in a day.
    static readonly HashSet<(int Month, int Day)> FixedHolidays =
    [
        (1, 1),   // Nyårsdagen
        (1, 6),   // Trettondedag jul
        (5, 1),   // Första maj
        (6, 6),   // Nationaldagen
        (12, 25), // Juldagen
        (12, 26)  // Annandag jul
    ];


    public TollCalculator()
    {
        TollFreeVehicleStrings = Enum
            .GetValues<TollFreeVehicles>()
            .Select(x => x.ToString())
            .ToArray();

        /* For the fun of it we use a LUT for toll fees.
         * Could even be a thing in some high perf scenarios.
         */
        for (int m = 0; m < 1440; m++)
            tollFee[m] = TollFee(m);

        static int TollFee(int minutesFromMidnight)
        {
            return minutesFromMidnight switch
            {
                >= 390 and < 420 => 13, // 06:30 - 06:59
                >= 420 and < 480 => 18, // 07:00 - 07:59
                >= 480 and < 510 => 13, // 08:00 - 08:29
                >= 900 and < 930 => 13, // 15:00 - 15:29
                >= 930 and < 1020 => 18, // 15:30 - 16:59
                >= 1020 and < 1080 => 13, // 17:00 - 17:59
                _ => 8
            };
        }
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
        /* This precondition is important and should
         * probably be guarded with the type system. */
        if (dates.GroupBy(x => x.Date).Count() > 1)
            throw new Exception("This should be a better exception!");

        if (IsTollFreeVehicle(vehicle)) return 0;

        var allFees = dates
            .Select(x => (x, GetTollFee(x)))
            .GroupBy(d => d.Item1.Hour)
            .Select(x => x.Max(y => y.Item2));

        int maxFee = Math.Min(allFees.Sum(), 60);

        return maxFee;
    }

    public int GetTollFee(DateTime date, Vehicle vehicle)
    {
        if (IsTollFreeVehicle(vehicle)) return 0;

        return GetTollFee(date);
    }

    int GetTollFee(DateTime date)
    {
        if (IsTollFreeDate(date)) return 0;

        // Convert everything to total minutes from midnight
        int m = (date.Hour * 60) + date.Minute;

        return tollFee[m];
    }

    bool IsTollFreeVehicle(Vehicle vehicle)
    {
        return TollFreeVehicleStrings.Contains(vehicle.GetVehicleType());
    }

    static bool IsTollFreeDate(DateTime date)
    {
        return IsWeekend(date) || IsPublicHoliday(date);
    }

    static bool IsWeekend(DateTime date)
    {
        return date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
    }

    static bool IsPublicHoliday(DateTime date)
    {
        int month = date.Month;
        int day = date.Day;

        // Fixed-date holidays.
        if (FixedHolidays.Contains((date.Month, date.Day)))
            return true;

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