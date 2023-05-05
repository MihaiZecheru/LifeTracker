namespace EntryEditor;

public static class Program
{
    public static void Main() { }

    /// <summary>
    /// Craete a new <see cref="Entry"/>
    /// </summary>
    [STAThread]
    public static void Main(DateOnly date)
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new EntryEditor(date.ToLongDateString()));
    }

    /// <summary>
    ///  Edit an existing <see cref="Entry"/>
    /// </summary>
    [STAThread]
    public static void Main(string short_summary, string detailed_summary, DateOnly date)
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new EntryEditor(short_summary, detailed_summary, date.ToLongDateString()));
    }
}
