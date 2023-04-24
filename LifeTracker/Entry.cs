namespace LifeTracker;

public struct Entry
{
    /// <summary>
    /// One-sentence summary of what happened on this day
    /// </summary>
    public string OneSentenceSummary { get; }
    /// <summary>
    /// Broader, more detailed summary of what happened on this day
    /// </summary>
    public string DetailedSummary { get; }
    /// <summary>
    /// What day is this <see cref="Entry"/> for?
    /// </summary>
    public DateOnly For { get; }

    public Entry(string oneSentenceSummary, string detailedSummary, DateOnly _for)
    {
        OneSentenceSummary = oneSentenceSummary;
        DetailedSummary = detailedSummary;
        For = _for;
    }
}
