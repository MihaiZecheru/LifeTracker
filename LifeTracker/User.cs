using Newtonsoft.Json;

namespace LifeTracker;



internal struct User
{
    /// <summary>
    /// The user's ID in the database
    /// </summary>
    public int ID { get; }
    /// <summary>
    /// The user's username and password
    /// </summary>
    public Authentication Auth { get; }
    /// <summary>
    /// The user's birthday
    /// </summary>
    public DateOnly Birthday { get; }
    /// <summary>
    /// The user's age
    /// </summary>
    public int Age {
        get
        {
            return (int)(DateTime.Now - Birthday.ToDateTime(new TimeOnly(0, 0, 0, 0))).TotalDays / 365;
        }
    }
    /// <summary>
    /// The user's <see cref="Entry"/> statistics; how many entries the user has made, longest streak, etc.
    /// </summary>
    public EntryStatistics EntryStats { get; }
    /// <summary>
    /// The IDs of every entry written by the user
    /// </summary>
    public List<int> EntryIDs { get; }

    public User(int id, Authentication auth, DateOnly birthday, EntryStatistics entryStats, List<int> entryIDs)
    {
        ID = id;
        Auth = auth;
        Birthday = birthday;
        EntryStats = entryStats;
        EntryIDs = entryIDs;
    }
}
