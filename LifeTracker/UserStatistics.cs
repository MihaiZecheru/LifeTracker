namespace LifeTracker;

public enum UserStatisticsFile
{
    TotalEntriesIndex = 0,
    LongestStreakStartIndex = 1,
    LongestStreakEndIndex = 2,
    LongestStreakLengthIndex = 3,
    CurrentStreakStartIndex = 4,
    CurrentStreakEndIndex = 5,
    CurrentStreakLengthIndex = 6,
    FirstEntryDateIndex = 7,
    LastEntryDateIndex = 8,
    EntryDatesIndex = 9
}

public class UserStatistics
{
    /// <summary>
    /// The path to the file where the <see cref="UserStatistics"/> are stored
    /// </summary>
    private static string UserStatsFile = @"C:/FileTracker/User.stats";

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
    public DateOnly LastEntryDate { get; private set; }
    /// <summary>
    /// All dates the user has made an <see cref="Entry"/> on
    /// </summary>
    public List<DateOnly> EntryDates { get; }

    public UserStatistics()
    {
        string[] lines = File.ReadAllLines(UserStatsFile);

        TotalEntries = Convert.ToInt32(lines[(int)UserStatisticsFile.TotalEntriesIndex]);

        LongestStreak = new DateRange(
            start: DateOnly.Parse(lines[(int)UserStatisticsFile.LongestStreakStartIndex]),
            end: DateOnly.Parse(lines[(int)UserStatisticsFile.LongestStreakEndIndex]),
            length: Convert.ToInt32(lines[(int)UserStatisticsFile.LongestStreakLengthIndex])
        );

        CurrentStreak = new DateRange(
            start: DateOnly.Parse(lines[(int)UserStatisticsFile.CurrentStreakStartIndex]),
            end: DateOnly.Parse(lines[(int)UserStatisticsFile.CurrentStreakStartIndex]),
            length: Convert.ToInt32(lines[(int)UserStatisticsFile.CurrentStreakLengthIndex])
        );

        FirstEntryDate = DateOnly.Parse(lines[(int)UserStatisticsFile.FirstEntryDateIndex]);
        LastEntryDate = DateOnly.Parse(lines[(int)UserStatisticsFile.LastEntryDateIndex]);
        
        string entryDatesLine = lines[(int)UserStatisticsFile.EntryDatesIndex];
        EntryDates = entryDatesLine.Split("{<@SEP>}").Select(date => DateOnly.Parse(date)).ToList();
    }

    /// <summary>
    /// Account for a new <see cref="Entry"/> in the <see cref="UserStatistics"/>
    /// </summary>
    /// <param name="newDate">The date of the <see cref="Entry"/> to add</param>
    public void AddEntryDate(DateOnly newDate)
    {
        EntryDates.Add(newDate);
        TotalEntries++;
        
        CurrentStreak.AddDay(newDate);
        if (CurrentStreak.Length > LongestStreak.Length)
        {
            LongestStreak = CurrentStreak;
        }

        LastEntryDate = newDate;

        SaveToFile();
    }

    /// <summary>
    /// Save the <see cref="UserStatistics"/> to file
    /// </summary>
    public void SaveToFile()
    {
        File.WriteAllLines(UserStatsFile, new[]
        {
            TotalEntries.ToString(),
            LongestStreak.Start.ToString(),
            LongestStreak.End.ToString(),
            LongestStreak.Length.ToString(),
            CurrentStreak.Start.ToString(),
            CurrentStreak.End.ToString(),
            CurrentStreak.Length.ToString(),
            FirstEntryDate.ToString(),
            LastEntryDate.ToString(),
            String.Join("{<@SEP>}", EntryDates.Select(date => date.ToString()))
        });
    }
}
