namespace EntryEditor;

public static class Program
{
    /// <summary>
    /// Craete a new <see cref="Entry"/>
    /// </summary>
    [STAThread]
    public static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new EntryEditor());
    }

    /// <summary>
    ///  Edit an existing <see cref="Entry"/>
    /// </summary>
    [STAThread]
    public static void Main(string short_summary, string detailed_summary)
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new EntryEditor(short_summary, detailed_summary));
    }
}
