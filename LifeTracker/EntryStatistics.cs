namespace LifeTracker;

public class EntryStatistics
{
    /// <summary>
    /// The total amount of entries the user has made
    /// </summary>
    public int TotalEntries { get; private set; }
    /// <summary>
    /// The longest <see cref="Entry"/> streak the user has made
    /// </summary>
    public DateRange LongestStreak { get; private set; }
    /// <summary>
    /// The current <see cref="Entry"/> streak the user is on
    /// </summary>
    public DateRange CurrentStreak { get; }
    /// <summary>
    /// The date of the user's first <see cref="Entry"/>
    /// </summary>
    public DateOnly FirstEntryDate { get; }
    /// <summary>
    /// The last date the user made an <see cref="Entry"/>
    /// </summary>
    public DateOnly LastEntryDate { get; }
    /// <summary>
    /// All dates the user has made an <see cref="Entry"/> on
    /// </summary>
    public List<DateOnly> EntryDates { get; }

    public EntryStatistics(int totalEntries, DateRange longestStreak, DateRange currentStreak, DateOnly firstEntryDate, DateOnly lastEntryDate, List<DateOnly> entryDates)
    {
        TotalEntries = totalEntries;
        LongestStreak = longestStreak;
        CurrentStreak = currentStreak;
        FirstEntryDate = firstEntryDate;
        LastEntryDate = lastEntryDate;
        EntryDates = entryDates;
    }

    public void AddEntryDate(DateOnly newDate)
    {
        EntryDates.Add(newDate);
        TotalEntries++;
        
        CurrentStreak.AddDay(newDate);
        if (CurrentStreak.Days > LongestStreak.Days)
        {
            LongestStreak = CurrentStreak;
        }
    }
}
