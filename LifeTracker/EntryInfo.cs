namespace LifeTracker;

public struct EntryInfo
{
    /// <summary>
    /// The year the Entry was written
    /// </summary>
    public int Year { get; }
    /// <summary>
    /// The month the Entry was written
    /// </summary>
    public int Month { get; }
    /// <summary>
    /// The day the Entry was written
    /// </summary>
    public int Day { get; }
    
    /// <summary>
    /// The ID of the Entry in the database
    /// </summary>
    public int ID { get; }
}
