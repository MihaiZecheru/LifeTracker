namespace EntryEditor;

internal static class HttpServer
{
    private static readonly HttpClient client = new HttpClient();

    /// <summary>
    /// Send the completed <see cref="Entry"/> back to the <see cref="LifeTracker"/>
    /// </summary>
    /// <param name="short_summary">The given value for Entry.OneSentenceSummary/></param>
    /// <param name="detailed_summary">The given value for Entry.DetailedSummary</param>
    public async static Task SendEntry(string short_summary, string detailed_summary)
    {
        try
        {
            await client.GetStringAsync($"http://localhost:8001/entry?short_summary={short_summary}&detailed_summary={detailed_summary}");
        }
        // No response will be given by the server so this is to prevent the error that would otherwise be thrown
        catch (Exception) { };
    }

    /// <summary>
    /// Notify the server that the user has quit the <see cref="EntryEditor"/> without saving their entry; the creation of the entry was cancelled
    /// </summary>
    public async static Task SendCancel()
    {
        try
        {
            await client.GetStringAsync("http://localhost:8001/cancel");
        }
        // No response will be given by the server so this is to prevent the error that would otherwise be thrown
        catch (Exception) { };
    }
}
