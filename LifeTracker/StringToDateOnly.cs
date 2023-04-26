namespace LifeTracker;

/// <summary>
/// Convert a <see langword="string"/> in the format of year-month-day to a <see cref="DateOnly"/> object
/// </summary>
public static class StringToDateOnly
{
    /// <summary>
    /// Convert a <see langword="string"/> in the format of year-month-day to a <see cref="DateOnly"/> object
    /// </summary>
    /// <param name="str">The string to convert to a <see cref="DateOnly"/> object, which is in the format of yyyy-mm-dd</param>
    /// <returns>The parsed <see cref="DateOnly"/> object</returns>
    public static DateOnly Convert(string str)
    {
        int[] lines = str.Split('-').Select(x => int.Parse(x)).ToArray();
        return new DateOnly(year: lines[0], month: lines[1], day: lines[2]);
    }
}
