namespace LifeTracker;

public struct DateRange
{
    /// <summary>
    /// The day the range starts with
    /// </summary>
    public DateOnly Start { get; }
    
    /// <summary>
    /// The day the range ends with
    /// </summary>
    public DateOnly End { get; private set; }
    
    /// <summary>
    /// The length of the range
    /// </summary>
    public int Length { get; private set; }

    public DateRange(DateOnly start, DateOnly end, int length)
    {
        Start = start;
        End = end;
        Length = length;
    }

    /// <summary>
    /// Add a day to the <see cref="DateRange"/>
    /// <br/><br/>
    /// Will extend the <see cref="DateRange"/> from <see cref="DateRange.Start"/> to the given <paramref name="day"/>
    /// </summary>
    /// <param name="day">The day to extend the range to</param>
    public void AddDay(DateOnly day)
    {
        End = day;
        Length++;
    }
}
