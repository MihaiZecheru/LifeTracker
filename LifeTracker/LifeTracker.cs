using FireSharp.Interfaces;
using FireSharp;
using Spectre.Console;
using System.Net;
using FireSharp.Config;
using FireSharp.Response;
using System.Windows.Forms.VisualStyles;

namespace LifeTracker;

public class LifeTracker
{
    private static Calendar ActiveCalendar { get; set; }
    private static IFirebaseClient client = new FirebaseClient(new FirebaseConfig
    {
        AuthSecret = "AIzaSyCU_WVbyDAIqNJvm6X3lhJ0dTAAuHWETZM",
        BasePath = "https://lifetracker-7516f-default-rtdb.firebaseio.com/"
    });

    public static void Main()
    {
        // Title screen
        Console.CursorVisible = false;
        AnsiConsole.Write(new FigletText("LifeTracker").Color(Color.DeepPink3).Centered());
        Console.ReadKey();
        Console.Clear();

        // Make sure the user is logged in, and if he isn't, log him in
        Login().GetAwaiter().GetResult();

        // Create and display calendar
        ActiveCalendar = new Calendar();
        ActiveCalendar.Display();

        // Listen to console resize events in order to update the calendar after resize
        StartConsoleResizeListener();

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

            /*
             * Right & Left arrows: next/previous day
             * Down & Up arrows: next/previous week
             * 
             * Shift + Right & Left arrows: next/previous month
             * Shift + Down & Up arrows: next/previous month
             * 
             * Ctrl + Right & Left arrows: next/previous year
             * Ctrl + Down & Up arrows: next/previous year
             */

            switch (keyinfo.Key)
            {
                case ConsoleKey.RightArrow:
                    if (shift) ActiveCalendar.NextMonth();
                    else if (ctrl) ActiveCalendar.NextYear();
                    else ActiveCalendar.NextDay();
                    break;

                case ConsoleKey.LeftArrow:
                    if (shift) ActiveCalendar.PreviousMonth();
                    else if (ctrl) ActiveCalendar.PreviousYear();
                    else ActiveCalendar.PreviousDay();
                    break;

                case ConsoleKey.UpArrow:
                    if (shift) ActiveCalendar.PreviousMonth();
                    else if (ctrl) ActiveCalendar.PreviousYear();
                    else ActiveCalendar.PreviousWeek();
                    break;

                case ConsoleKey.DownArrow:
                    if (shift) ActiveCalendar.NextMonth();
                    else if (ctrl) ActiveCalendar.NextYear();
                    else ActiveCalendar.NextWeek();
                    break;

                case ConsoleKey.Enter:
                    DateOnly date = ActiveCalendar.SelectedDate();

                    // Users can create/edit entries for today and up to 5 days in the past

                    if (
                        date == DateOnly.FromDateTime(DateTime.Today) ||
                        date == DateOnly.FromDateTime(DateTime.Today.AddDays(-1)) ||
                        date == DateOnly.FromDateTime(DateTime.Today.AddDays(-2)) ||
                        date == DateOnly.FromDateTime(DateTime.Today.AddDays(-3)) ||
                        date == DateOnly.FromDateTime(DateTime.Today.AddDays(-4)) ||
                        date == DateOnly.FromDateTime(DateTime.Today.AddDays(-5))
                    )
                    {
                        // Check if an entry exists for the date
                        if (ActiveCalendar.Get(date) == null)
                        {
                            // Create new entry
                            CreateEntry(date);
                        }
                        else
                        {
                            // Edit existing entry
                            EditEntry(ActiveCalendar.Get(date)!);
                        }
                    }

                    break;
            }
        }
    }

    /// <summary>
    /// Listen to console resize events and update the calendar after resize
    /// </summary>
    private static void StartConsoleResizeListener()
    {
        new Thread(() =>
        {
            int x = Console.WindowWidth;
            int y = Console.WindowHeight;

            while (true)
            {
                if (x == Console.WindowWidth && y == Console.WindowHeight) continue;

                // Update calendar, console window has been resized
                x = Console.WindowWidth;
                y = Console.WindowHeight;
                ActiveCalendar.Display();
            }
        }).Start();
    }

    /// <summary>
    /// Open the <see cref="EntryEditor"/> to edit an existing <see cref="Entry"/>
    /// </summary>
    private static void OpenEntryEditor(string short_summary, string detailed_summary, DateOnly date)
    {
        new Thread(() => {
            EntryEditor.Program.Main(short_summary, detailed_summary, date);
        }).Start();
    }

    /// <summary>
    /// Open the <see cref="EntryEditor"/> to create a new <see cref="Entry"/>
    /// </summary>
    private static void OpenEntryEditor(DateOnly date)
    {
        new Thread(() => {
            EntryEditor.Program.Main(date);
        }).Start();
    }

    /// <summary>
    /// Edit an existing <see cref="Entry"/> using the <see cref="EntryEditor"/> WinForm
    /// </summary>
    /// <param name="entry">The existing <see cref="Entry"/> to edit</param>
    private static void EditEntry(Entry entry)
    {
        OpenEntryEditor(entry.OneSentenceSummary, entry.DetailedSummary, entry.For);
        ListenForFormEvents(entry.For);
    }

    /// <summary>
    /// Create an <see cref="Entry"/> using the <see cref="EntryEditor"/> WinForm
    /// </summary>
    /// <param name="date">The date to create the <see cref="Entry"/> on</param>
    private static void CreateEntry(DateOnly date)
    {
        OpenEntryEditor(date);
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
        HttpListenerResponse res = ctx.Response;

        HandleListenerResponse(req, date);
    }

    /// <summary>
    /// Handle responses from the <see cref="EntryEditor"/> WinForm
    /// </summary>
    private static void HandleListenerResponse(HttpListenerRequest req, DateOnly date)
    {
        if (req.Url.AbsolutePath == "/entry")
        {
            string short_summary = req.QueryString.GetValues("short_summary")![0];
            string detailed_summary = req.QueryString.GetValues("detailed_summary")![0];

            Entry entry = new Entry(short_summary, detailed_summary, date);
            ActiveCalendar.SetEntry(entry);
            ListenForKeypress = true;
        }
        // EntryEditor has been closed
        else if (req.Url.AbsolutePath == "/cancel")
        {
            ListenForKeypress = true;
        }
    }

    /// <summary>
    /// Check if a username already exists in the database
    /// </summary>
    /// <param name="username">The username to check</param>
    /// <returns>Boolean indiciating whether or not the username exists</returns>
    private static async Task<bool> UsernameExists(string username)
    {
        FirebaseResponse response = await client.GetAsync($"users/{username}");
        string result = response.ResultAs<string>();
        return result != null;
    }

    /// <summary>
    /// Create a new login and save it to login.txt
    /// </summary>
    /// <returns>The user's new login</returns>
    private static async Task<Dictionary<string, string>> CreateLogin()
    {
        Console.Clear();

        string username;
        while (true)
        {
            username = AnsiConsole.Ask<string>("Create a username: ");
            if (await UsernameExists(username))
            {
                AnsiConsole.Markup("[red]Username already exists[/]\n");
            } else break;
        }

        string password;
        while (true)
        {
            password = AnsiConsole.Ask<string>("Create a password: ");
            if (password.Length < 8)
            {
                AnsiConsole.Markup("[red]Password must be at least 8 characters long[/]");
            }
            else break;
        }

        Console.Clear();
        try
        {
            FileStream fstream = File.Create("login.txt");
            fstream.Close();
        }
        catch (Exception) { }
        File.WriteAllText("login.txt", $"{username}\n{password}");
        await client.SetAsync($"users/{username}", password);
        return new Dictionary<string, string>()
        {
            { "username", username },
            { "password", password }
        };
    }

    /// <summary>
    /// Get the user's login, saved in login.txt
    /// </summary>
    /// <returns>Dictionary containing the user's login information</returns>
    private static Dictionary<string, string> GetSavedLogin()
    {
        if (File.Exists("login.txt") == false)
        {
            return CreateLogin().GetAwaiter().GetResult();
        }

        string[] login;
        try
        {
            string txt = File.ReadAllText("login.txt");
            login = txt.Split("\n");
        }
        catch (IndexOutOfRangeException)
        {
            return CreateLogin().GetAwaiter().GetResult();
        }
        return new Dictionary<string, string>()
        {
            { "username", login[0] },
            { "password", login[1] }
        };
    }

    /// <summary>
    /// Logs the user into LifeTracker
    /// </summary>
    /// <returns>A Task that must be awaited, as the application will exit if the login is incorrect</returns>
    private static async Task Login()
    {
        Dictionary<string, string> login = GetSavedLogin();
        string username = login["username"];
        string password = login["password"];

        FirebaseResponse response = await client.GetAsync($"users/{username}");
        string real_password = response.ResultAs<string>();

        if (real_password == null)
        {
            await CreateLogin(); return;
        }

        if (password == real_password)
        {
            Console.Clear();
            AnsiConsole.Write(new Markup($"[green]Logged in as {username}...[/]").Centered());
            Console.ReadKey();
            Console.Clear();
        }
    }
}   