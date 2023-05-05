namespace LifeTracker;

public class Entry
{
    /// <summary>
    /// The set of chars used to separate the <see cref="OneSentenceSummary"/> and <see cref="DetailedSummary"/> in the file containing the <see cref="Entry"/>
    /// </summary>
    public static readonly string SEP = "{<@SEP>}";

    /// <summary>
    /// One-sentence summary of what happened on this day
    /// </summary>
    public string OneSentenceSummary { get; }
    /// <summary>
    /// Broader, more detailed summary of what happened on this day
    /// </summary>
    public string DetailedSummary { get; }
    /// <summary>
    /// What date is this <see cref="Entry"/> for?
    /// </summary>
    public DateOnly For { get; }

    /// <summary>
    /// Create an <see cref="Entry"/> object
    /// </summary>
    /// <param name="oneSentenceSummary">A one-sentence summary of what happened on that day</param>
    /// <param name="detailedSummary">A more in-depth summary of what happened on that day</param>
    /// <param name="_for">The day this entry is for</param>
    public Entry(string oneSentenceSummary, string detailedSummary, DateOnly _for)
    {
        OneSentenceSummary = oneSentenceSummary;
        DetailedSummary = detailedSummary;
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
        OneSentenceSummary = lines[0];
        DetailedSummary = lines[1];
        For = date;
    }

    /// <summary>
    /// Convert the <see cref="Entry"/> to a string for saving to file
    /// </summary>
    public override string ToString()
    {
        return $"{OneSentenceSummary}{SEP}{DetailedSummary}";
    }
}
