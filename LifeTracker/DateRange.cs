namespace LifeTracker;

public struct DateRange
{
    public DateOnly Start { get; }
    public DateOnly End { get; private set; }
    public int Length { get; private set; }

    public DateRange(DateOnly start, DateOnly end, int length)
    {
        Start = start;
        End = end;
        Length = length;
    }

    public void AddDay(DateOnly day)
    {
        End = day;
        Length++;
    }
}

