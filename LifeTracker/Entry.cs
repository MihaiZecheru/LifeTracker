namespace LifeTracker;

public class Entry
{
    /// <summary>
    /// The set of chars used to separate the <see cref="Summary"/> and <see cref="Details"/> in the file containing the <see cref="Entry"/>
    /// </summary>
    public static readonly string SEP = "{<@SEP>}";

    /// <summary>
    /// Answer to the prompt: Summary - What do you want to remember doing today?
    /// </summary>
    public string Summary { get; }
    /// <summary>
    /// Answer to the prompt: Details - describe your day
    /// </summary>
    public string Details { get; }
    /// <summary>
    /// What date is this <see cref="Entry"/> for?
    /// </summary>
    public DateOnly For { get; }

    /// <summary>
    /// Create an <see cref="Entry"/> object
    /// </summary>
    /// <param name="summary">A summary of what happened on that day</param>
    /// <param name="details">The details describing what happened on that day</param>
    /// <param name="_for">The day this entry is for</param>
    public Entry(string summary, string details, DateOnly _for)
    {
        Summary = summary;
        Details = details;
        For = _for;
    }

    /// <summary>
    /// Load an <see cref="Entry"/> object from a file
    /// </summary>
    /// <param name="filepath">The path to the <see cref="Entry"/> in the filesystem</param>
    /// <param name="date">The date the <see cref="Entry"/> was written on</param>
    public Entry(string filepath, DateOnly date)
    {
        string[] lines = File.ReadAllText(filepath).Split(SEP);
        Summary = lines[0];
        Details = lines[1];
        For = date;
    }

    /// <summary>
    /// Convert the <see cref="Entry"/> to a string for saving to file
    /// </summary>
    public override string ToString()
    {
        return $"{Summary}{SEP}{Details}";
    }
}
