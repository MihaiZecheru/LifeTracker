using Spectre.Console;
using System.Net;

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
    /// When false, the <see cref="StartKeypressListener"/> will stop listening for keypress events
    /// <br/><br/>
    /// Used for pausing the LifeTracker console screen while the <see cref="EntryEditor"/> is open
    /// </summary>
    private static bool ListenForKeypress = true;

    /// <summary>
    /// Listen for arrow keypress events in order to move the user's cursor on the <see cref="Calendar"/>
    /// </summary>
    private static void StartKeypressListener()
    {
        while (true)
        {
            if (!ListenForKeypress) continue;

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
                    DateOnly date = ActiveCalendar.SelectedDate();

                    // Edit existing entry if it exists
                    if (ActiveCalendar.Get(date) != null)
                    {
                        EditEntry(ActiveCalendar.Get(ActiveCalendar.SelectedDate());
                    }
                    // Only create new entry if the entry is from today
                    else if (date == DateOnly.FromDateTime(DateTime.Today))
                    {
                        CreateEntry(date);
                    }
                    
                    break;
            }
        }
    }

    /// <summary>
    /// Open the <see cref="EntryEditor"/>
    /// </summary>
    private static void OpenEntryEditor()
    {
        EntryEditor.Program.Main();
    }

    /// <summary>
    /// Edit an existing <see cref="Entry"/> using the <see cref="EntryEditor"/> WinForm
    /// </summary>
    /// <param name="entry">The existing <see cref="Entry"/> to edit</param>
    private static void EditEntry(Entry entry)
    {
        OpenEntryEditor(); // TODO: send the entry OSS & DS to the winform
        ListenForFormEvents(entry.For);
    }

    /// <summary>
    /// Create an <see cref="Entry"/> using the <see cref="EntryEditor"/> WinForm
    /// </summary>
    /// <param name="date">The date to create the <see cref="Entry"/> on</param>
    private static void CreateEntry(DateOnly date)
    {
        OpenEntryEditor();
        ListenForFormEvents(date);
    }

    /// <summary>
    /// Listen for events coming from the <see cref="EntryEditor"/> WinForm
    /// <br/><br/>
    /// If the status code is 200, meaning the user has finished editing his <see cref="Entry"/>, the entry is parsed from the response, saved, and added to the <see cref="ActiveCalendar"/><br/>
    /// Otherwise, if the status code is 100, meaning the user has closed out of the <see cref="EntryEditor"/>, the <see cref="StartKeypressListener"/> is resumed
    /// </summary>
    private static void ListenForFormEvents(DateOnly date)
    {
        using var listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8001/");

        listener.Start();

        HttpListenerContext ctx = listener.GetContext();
        HttpListenerRequest req = ctx.Request;

        // EntryEditor has returned the written entry as a string in the format of: "oneSentenceSummary{<@SEP>}detailedSummary"
        if (ctx.Response.StatusCode == 200)
        {
            string text;
            using (var reader = new StreamReader(req.InputStream, req.ContentEncoding))
            {
                text = reader.ReadToEnd();
            }

            Entry entry = Entry.ParseFromString(text, date);
            ActiveCalendar.SetEntry(entry);
        }
        // EntryEditor has been closed
        else if (ctx.Response.StatusCode == 100)
        {
            ListenForKeypress = true;
        }
    }
}

// TODO: create the winform and have it send a message to the http listener which contains the entry info (entry.ToString() is the format)
// https://zetcode.com/csharp/httplistener/