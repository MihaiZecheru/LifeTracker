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

    private static string FormatDay(int day)
    {
        string dayString = day.ToString();
        if (dayString.Length == 1) return "0" + dayString;
        return dayString;
    }

    private static string FormatMonth(int month)
    {
        string monthString = month.ToString();
        if (monthString.Length == 1) return "0" + monthString;
        return monthString;
    }

    private static string FormatYear(int year)
    {
        string yearString = year.ToString();
        if (yearString.Length == 1) return "0" + yearString;
        return yearString;
    }

    /// <summary>
    /// Show the calendar and the currently selected entry in the console
    /// </summary>
    public void Display()
    {
        Console.Clear();
        Console.Write("\x1b[3J");
        Console.CursorVisible = false;

        DisplayLayout["CalendarDisplay"].Update(
            Align.Center(
                new Spectre.Console.Calendar(ActiveDate.Year, ActiveDate.Month, ActiveDate.Day).AddCalendarEvent(ActiveDate.ToDateTime(new TimeOnly(0, 0)))
                    .HighlightStyle(new Style(Color.DeepPink3))
                    .HeaderStyle(new Style(Color.DeepPink3))
                    .BorderStyle(new Style(Color.Yellow))
            )
        );

        Entry? selectedEntry = this.Get(ActiveDate.Year, ActiveDate.Month, ActiveDate.Day);

        // No entry exists - write "no entry written for {date}"
        if (selectedEntry == null)
        {
            var today = DateTime.Today;
            bool entryWrittenToday = selectedEntry?.For == new DateOnly(today.Year, today.Month, today.Day);

            DisplayLayout["EntryDisplay"].Update(
                new Panel(
                    new Rows(
                        new Text(
                            text: "No entry written " + (entryWrittenToday
                                ? "today"
                                : $"on {FormatDay(ActiveDate.Day)}-{FormatMonth(ActiveDate.Month)}-{FormatYear(ActiveDate.Year)}"),
                            style: new Style(Color.DeepPink3)
                        ).Centered(),
                        new Rule().RuleStyle(new Style(Color.Yellow))
                    )
                ).Expand().BorderColor(Color.Yellow)
            );
        }
        // Entry exists - display it to console
        else
        {
            DisplayLayout["EntryDisplay"].Update(
                new Panel(
                    new Rows(
                        new Text(selectedEntry?.OneSentenceSummary, new Style(Color.DeepPink3)).Centered(),
                        new Rule().RuleStyle(new Style(Color.Yellow)),
                        new Text(selectedEntry?.DetailedSummary, new Style(Color.White)).LeftJustified()
                    )
                ).Expand().BorderColor(Color.Yellow)
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
    public Entry? Get(int year, int month, int day)
    {
        string filepath = @$"C:/LifeTracker/Entry/{year}/{month}/{day}.entry";
        if (!File.Exists(filepath)) return null;
        return new Entry(filepath, new DateOnly(year, month, day));
    }

    /// <summary>
    /// Get an <see cref="Entry"/> from the filesystem
    /// </summary>
    /// <param name="date">The date the entry was written</param>
    /// <returns>The parsed <see cref="Entry"/> if an entry exists for the given day, otherwise <see langword="null"/></returns>
    public Entry? Get(DateOnly date)
    {
        return Get(date.Year, date.Month, date.Day);
    }

    /// <summary>
    /// Save an <see cref="Entry"/> for the given day in the calendar
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/> to save</param>
    public void SetEntry(Entry entry)
    {
        string parent_dir = $@"C:/LifeTracker/Entry/{entry.For.Year}/{entry.For.Month}/";
        string filepath = $@"C:/LifeTracker/Entry/{entry.For.Year}/{entry.For.Month}/{entry.For.Day}.entry";

        if (!Directory.Exists(parent_dir)) Directory.CreateDirectory(parent_dir);
        File.WriteAllText(filepath, entry.ToString());

        Display();
    }

    /// <summary>
    /// Highlight the next day in the <see cref="Calendar"/>
    /// </summary>
    public void NextDay()
    {
        ActiveDate = ActiveDate.AddDays(1);
        Display();
    }

    /// <summary>
    /// Highlight the day one week in the future in the <see cref="Calendar"/>
    /// </summary>
    public void NextWeek()
    {
        ActiveDate = ActiveDate.AddDays(7);
        Display();
    }

    /// <summary>
    /// Highlight the day one month in the future in the <see cref="Calendar"/>
    /// </summary>
    public void NextMonth()
    {
        ActiveDate = ActiveDate.AddMonths(1);
        Display();
    }

    /// <summary>
    /// Highlight the day one year in the future in the <see cref="Calendar"/>
    /// </summary>
    public void NextYear()
    {
        ActiveDate = ActiveDate.AddYears(1);
        Display();
    }

    /// <summary>
    /// Highlight the previous day in the <see cref="Calendar"/>
    /// </summary>
    public void PreviousDay()
    {
        ActiveDate = ActiveDate.AddDays(-1);
        Display();
    }

    /// <summary>
    /// Highlight the day one week in the past in the <see cref="Calendar"/>
    /// </summary>
    public void PreviousWeek()
    {
        ActiveDate = ActiveDate.AddDays(-7);
        Display();
    }

    /// <summary>
    /// Highlight the day one month in the past in the <see cref="Calendar"/>
    /// </summary>
    public void PreviousMonth()
    {
        ActiveDate = ActiveDate.AddMonths(-1);
        Display();
    }

    /// <summary>
    /// Highlight the day one year in the past in the <see cref="Calendar"/>
    /// </summary>
    public void PreviousYear()
    {
        ActiveDate = ActiveDate.AddYears(-1);
        Display();
    }

    /// <summary>
    /// Get the currently selected date
    /// </summary>
    public DateOnly SelectedDate()
    {
        return ActiveDate;
    }

    /// <summary>
    /// Get the currently selected date as a string
    /// </summary>
    public string SelectedDateFilePath()
    {
        return @$"C:/LifeTracker/Entry/{ActiveDate.Year}/{ActiveDate.Month}/{ActiveDate.Day}.entry";
    }
}
