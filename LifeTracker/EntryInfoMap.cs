namespace LifeTracker;

public class EntryInfoMap
{
    /// <summary>
    /// Map of entries and their dates
    /// <br/><br/>
    /// Map[year][month][day] = Entry.ID
    /// </summary>
    private readonly Dictionary<int, Dictionary<int, Dictionary<int, int>>> Map = new();

    public EntryInfoMap(List<EntryInfo> entryInfos)
    {
        for (int i = 0; i < entryInfos.Count; i++)
        {
            EntryInfo entryInfo = entryInfos[i];
            Map[entryInfo.Year][entryInfo.Month][entryInfo.Day] = entryInfo.ID;
        }
    }

    /// <summary>
    /// Get the ID of the <see cref="Entry"/> written on the given date
    /// </summary>
    /// <param name="Year">The year the <see cref="Entry"/> was written on</param>
    /// <param name="Month">The month the <see cref="Entry"/> was written on</param>
    /// <param name="Day">The day the <see cref="Entry"/> was written on</param>
    /// <returns>The ID of the <see cref="Entry"/> on the given day</returns>
    public int Get(int Year, int Month, int Day)
    {
        return Map[Year][Month][Day];
    }
}
