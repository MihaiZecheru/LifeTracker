using Spectre.Console;

namespace LifeTracker;

public class LifeTracker
{
    private static User ActiveUser { get; set; }
    private static string AuthFilePath = "auth.txt";
    private static Style Pink = new Style(Color.DeepPink3);
    private static Style Yellow = new Style(Color.Yellow);
    public static void Main()
    {
        // Get auth for fetching user object from db
        var auth = GetAuth();
        
        // Set ActiveUser
        AnsiConsole.Status().Start("[yellow]Fetching User[/]", ctx =>
        {
            ctx.SpinnerStyle = Yellow;
            ActiveUser = API.GetUser(auth);
        });
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
            AnsiConsole.Status().Start("[yellow]Validating Auth...[/]", ctx =>
            {
                ctx.SpinnerStyle = Yellow;
                valid = API.ValidLogin(username, password);
            });

            if (valid) return new Authentication(username, password);
        }
    }
}