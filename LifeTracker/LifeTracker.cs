using Spectre.Console;

namespace LifeTracker;

public class LifeTracker
{
    private static Calendar ActiveCalendar { get; set; }

    public static void Main()
    {
        // Title screen
        Console.CursorVisible = false;
        AnsiConsole.Write(new FigletText("LifeTracker").Color(Color.DeepPink3).Centered());
        Console.ReadKey();
        Console.Clear();

        // Create and display calendar
        ActiveCalendar = new Calendar();
        ActiveCalendar.Display();
        Console.ReadKey();
    }
}