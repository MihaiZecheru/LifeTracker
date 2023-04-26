using Spectre.Console;

namespace LifeTracker;

public class LifeTracker
{
    private static Style Pink = new Style(Color.DeepPink3);
    private static Style Yellow = new Style(Color.Yellow);
    private static Calendar ActiveCalendar { get; set; }

    public static void Main()
    {
        // Set ActiveUser
        AnsiConsole.Status().Start("[yellow]Fetching User Data[/]", async ctx =>
        {
            ctx.SpinnerStyle = Yellow;
        });

        // Create and display calendar
        ActiveCalendar = new Calendar();
        ActiveCalendar.Display();
    }
}