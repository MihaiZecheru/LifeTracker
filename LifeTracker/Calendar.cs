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
                new Layout("CalendarDisplay").Size(12),
                new Layout("EntryDisplay")
            );
    }

    public void Display()
    {
        Console.CursorVisible = false;

        DisplayLayout["CalendarDisplay"].Update(
            Align.Center(
                new Spectre.Console.Calendar(ActiveDate.Year, ActiveDate.Month, ActiveDate.Day)
                    .HighlightStyle(new Style(Color.DeepPink3))
                    .HeaderStyle(new Style(Color.DeepPink3))
                    .BorderStyle(new Style(Color.Yellow))
            )
        );

        Entry? selectedEntry = this.Get(ActiveDate.Year, ActiveDate.Month, ActiveDate.Day);

        if (selectedEntry == null)
        {
            var today = DateTime.Today;
            bool entryWrittenToday = selectedEntry?.For == new DateOnly(today.Year, today.Month, today.Day);

            DisplayLayout["EntryDisplay"].Update(
                new Panel(
                    new Rows(
                        new Text("No entry written " + (entryWrittenToday ? "today" : $"on {ActiveDate.Day}-{ActiveDate.Month}-{ActiveDate.Year}"), new Style(Color.DeepPink3)).Centered(),
                        new Rule().RuleStyle(new Style(Color.Yellow))
                    )
                ).Expand().BorderColor(Color.Yellow)
            );
        }
        else
        {
            DisplayLayout["EntryDisplay"].Update(
                new Panel(
                    new Rows(
                        new Text(selectedEntry?.OneSentenceSummary, new Style(Color.Yellow)).Centered(),
                        new Rule(),
                        new Text(selectedEntry?.DetailedSummary, new Style(Color.White)).LeftJustified()
                    )
                ).Expand()
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
