using MDB_SDK;
using Spectre.Console;

namespace LifeTracker;

public class LifeTracker
{
    private static User ActiveUser { get; set; }
    private static string AuthFilePath = "auth.txt";
    private static Style Pink = new Style(Color.DeepPink3);
    private static Style Yellow = new Style(Color.Yellow);
    private static Calendar ActiveCalendar { get; set; }

    public static void Main()
    {
        if (Environment.GetEnvironmentVariable("MDB_API_KEY") == null) throw new Exception("Set the MDB_API_KEY environment variable to continue");
        if (Environment.GetEnvironmentVariable("MDB_USER_ID") == null) throw new Exception("Set the MDB_USER_ID environment variable to continue");
        if (Environment.GetEnvironmentVariable("MDB_ENVIRONMENT_NAME") == null) throw new Exception("Set the MDB_ENVIRONMENT_NAME environment variable to continue");

        // Initialize the API
        MDB.Initialize(Environment.GetEnvironmentVariable("MDB_API_KEY")!, int.Parse(Environment.GetEnvironmentVariable("MDB_USER_ID")!), Environment.GetEnvironmentVariable("MDB_ENVIRONMENT_NAME")!);

        // Get auth for fetching user object from db
        Authentication auth = GetAuth();

        // Set ActiveUser
        AnsiConsole.Status().Start("[yellow]Fetching User[/]", async ctx =>
        {
            ctx.SpinnerStyle = Yellow;
            dynamic res = await MDB.Get($"/user/?username={auth.Username}&password={auth.Password}");

            if (res.Count == 0)
            {
                throw new Exception("Login Error");
            }

            res = res[0];

            Console.WriteLine(res);

            dynamic entryStatsRes = (await MDB.Get($"/entryStats/?user_id={res._id}")!)[0];
            dynamic entryInfosRes = (await MDB.Get($"/entry_info/?user_id={res._id}")!)[0];

            ActiveUser = new User(
                id: res._id,
                auth: auth,

                birthday: new DateOnly(
                    year: res.birthday.year,
                    month: res.birthday.month,
                    day: res.birthday.day),
                
                entryStats: new EntryStatistics(
                    
                    totalEntries: entryStatsRes.totalEntries,
                    
                    longestStreak: new DateRange(
                        
                        start: new DateOnly(
                            year: entryStatsRes.longestStreak.start.year,
                            month: entryStatsRes.longestStreak.start.month,
                            day: entryStatsRes.longestStreak.start.day),

                        end: new DateOnly(
                            year: entryStatsRes.longestStreak.end.year,
                            month: entryStatsRes.longestStreak.end.month,
                            day: entryStatsRes.longestStreak.end.day),
                        
                        length: entryStatsRes.longestStreak.length),
                    
                    currentStreak: new DateRange(
                        
                        start: new DateOnly(
                            year: entryStatsRes.currentStreak.start.year,
                            month: entryStatsRes.currentStreak.start.month,
                            day: entryStatsRes.currentStreak.start.day),

                        end: new DateOnly(
                            year: entryStatsRes.currentStreak.end.year,
                            month: entryStatsRes.currentStreak.end.month,
                            day: entryStatsRes.currentStreak.end.day),

                        length: entryStatsRes.currentStreak.length),

                    firstEntryDate: new DateOnly(
                        year: entryStatsRes.firstEntryDate.year,
                        month: entryStatsRes.firstEntryDate.month,
                        day: entryStatsRes.firstEntryDate.day),

                    lastEntryDate: new DateOnly(
                        year: entryStatsRes.lastEntryDate.year,
                        month: entryStatsRes.lastEntryDate.month,
                        day:entryStatsRes.lastEntryDate.day),

                    entryDates: entryStatsRes.entryDates),
                
                entryInfos: entryInfosRes);
        }).GetAwaiter().GetResult();

        // Create and display calendar
        ActiveCalendar = new Calendar(new EntryInfoMap(ActiveUser.EntryInfos));
        ActiveCalendar.Display();
    }

    public static Authentication GetAuth()
    {
        // The user has a saved login
        if (File.Exists(AuthFilePath))
        {
            string[] filetext = File.ReadAllLines(AuthFilePath);
            return new Authentication(username: filetext[0], password: filetext[1]);
        }
        // The user has to put in their username and password
        else
        {
            var auth = UserLogin();
            // Save login to file
            File.WriteAllLines(AuthFilePath, new[] { auth.Username, auth.Password });
            return auth;
        }
    }

    public static Authentication UserLogin()
    {
        var rule = new Rule("[deeppink3]Login[/]");
        rule.Style = Yellow;

        while (true)
        {
            Console.Clear();
            AnsiConsole.Write(rule);
            
            string username = AnsiConsole.Prompt<string>(new TextPrompt<string>("[yellow]Username: [/]").PromptStyle(Pink));
            string password = AnsiConsole.Prompt<string>(new TextPrompt<string>("[yellow]Password: [/]").PromptStyle(Pink).Secret());

            // Loading
            bool valid = false;
            AnsiConsole.Status().Start("[yellow]Validating Auth...[/]", async ctx =>
            {
                ctx.SpinnerStyle = Yellow;
                dynamic res = await MDB.Get($"/UserAuth/?username={username}&password={password}");
                valid = res.Count == 1;
            }).GetAwaiter().GetResult();

            if (valid) return new Authentication(username, password);
        }
    }
}