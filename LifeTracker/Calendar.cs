using Spectre.Console;

namespace LifeTracker;

public class Calendar
{
    private Layout DisplayLayout;
    private DateOnly ActiveDate;

    public Calendar()
    {
        DateTime today = DateTime.Now;
        ActiveDate = new DateOnly(today.Year, today.Month, today.Day);
        
        DisplayLayout = new Layout("Root")
            .SplitRows(
                new Layout("CalendarDisplay"),
                new Layout("EntryDisplay"))
            .Ratio(2);
    }

    public void Display()
    {
        DisplayLayout["CalendarDisplay"].Update(
            Align.Center(new Spectre.Console.Calendar(ActiveDate.Year, ActiveDate.Month, ActiveDate.Day), VerticalAlignment.Middle)
        );

        Entry? selectedEntry = this.Get(ActiveDate.Year, ActiveDate.Month, ActiveDate.Day);

        if (selectedEntry == null)
        {
            DisplayLayout["EntryDisplay"].Update(new Text($"No entry made on {ActiveDate.Day}-{ActiveDate.Month}-{ActiveDate.Year}"));
        }
        else
        {
            DisplayLayout["EntryDisplay"].Update(
                new Text(
                    selectedEntry?.OneSentenceSummary +
                    "\n\n" +
                    selectedEntry?.DetailedSummary
                )
            );
        }

        AnsiConsole.Write(DisplayLayout);
    }

    /// <summary>
    /// Get an <see cref="Entry"/> from the filesystem
    /// </summary>
    /// <param name="year">The year the entry was written</param>
    /// <param name="month">The month the entry was written</param>
    /// <param name="day">The day the entry was written</param>
    /// <returns>The parsed <see cref="Entry"/> if an entry exists for the given day, otherwise <see langword="null"/></returns>
    internal Entry? Get(int year, int month, int day)
    {
        string filepath = @$"C:/LifeTracker/Entry/{year}/{month}/{day}.entry";
        if (!File.Exists(filepath)) return null;

        string[] lines = File.ReadAllLines(filepath);
        return new Entry(oneSentenceSummary:
            lines[(int)EntryFile.OneSentenceSummaryIndex],
            detailedSummary: lines[(int)EntryFile.DetailedSummaryIndex],
            _for: StringToDateOnly.Convert(lines[(int)EntryFile.ForIndex])
        );
    }
}
