namespace LifeTracker;

public struct DateRange
{
    public DateOnly Start { get; }
    public DateOnly End { get; private set; }
    public int Days { get; private set; }

    public DateRange(DateOnly start, DateOnly end, int days)
    {
        Start = start;
        End = end;
        Days = days;
    }

    public void AddDay(DateOnly day)
    {
        End = day;
        Days++;
    }
}

