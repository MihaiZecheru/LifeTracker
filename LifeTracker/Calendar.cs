using Spectre.Console;

namespace LifeTracker;

public class Calendar
{
    private Layout DisplayLayout;
    private DateOnly ActiveDate;
    private EntryInfoMap EntriesMap;
    public Calendar(EntryInfoMap entriesMap)
    {
        EntriesMap = entriesMap;

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

        int selectedEntryID = EntriesMap.Get(ActiveDate.Year, ActiveDate.Month, ActiveDate.Day);
        //Entry entry = TODO: get from API

        DisplayLayout["EntryDisplay"].Update(
            new Text(""
                //selectedEntry.OneSentenceSummary +
                //"\n\n" +
                //selectedEntry.DetailedSummary
            )
        );

        AnsiConsole.Write(DisplayLayout);
    }
}
