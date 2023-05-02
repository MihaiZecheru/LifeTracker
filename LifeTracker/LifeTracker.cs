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

        // Listen to key events in order to update the calendar
        StartKeypressListener();
    }

    /// <summary>
    /// Listen for arrow keypress events in order to move the user's cursor on the <see cref="Calendar"/>
    /// </summary>
    private static void StartKeypressListener()
    {
        while (true)
        {
            ConsoleKeyInfo keyinfo = Console.ReadKey(true);
            bool ctrl = keyinfo.Modifiers.HasFlag(ConsoleModifiers.Control);
            bool shift = keyinfo.Modifiers.HasFlag(ConsoleModifiers.Shift);

            switch (keyinfo.Key)
            {
                case ConsoleKey.RightArrow:
                case ConsoleKey.DownArrow:
                    if (ctrl && shift) ActiveCalendar.NextYear();
                    else if (ctrl)     ActiveCalendar.NextMonth();
                    else if (shift)    ActiveCalendar.NextWeek();
                    else               ActiveCalendar.NextDay();
                    break;

                case ConsoleKey.LeftArrow:
                case ConsoleKey.UpArrow:
                    if (ctrl && shift) ActiveCalendar.PreviousYear();
                    else if (ctrl)     ActiveCalendar.PreviousMonth();
                    else if (shift)    ActiveCalendar.PreviousWeek();
                    else               ActiveCalendar.PreviousDay();
                    break;

                case ConsoleKey.Enter:
                    if (ActiveCalendar.Get())

                    break;
            }
        }
    }
}