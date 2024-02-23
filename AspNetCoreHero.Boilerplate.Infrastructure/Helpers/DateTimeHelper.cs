using System.Globalization;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Helpers;

public class DateTimeHelper
{
    public static DateTime Parse(string datetime, string format)
    {
        return DateTime.ParseExact(datetime, format, CultureInfo.InvariantCulture);
    }

    public static DateTime FromEpoch(double epochSeconds)
    {
        return Epoch().AddSeconds(epochSeconds);
    }

    public static DateTime Epoch()
    {
        return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }

}
