namespace LifeTracker;

internal static class API
{
    private static string MDB_AuthKey { get; set; }
    public static void Initialize(string MDB_auth_key)
    {
        MDB_AuthKey = MDB_auth_key;
    }

    /// <summary>
    /// Check if <see cref="MDB_AuthKey"/> has been set through the <see cref="Initialize(string)"/> method
    /// <br/><br/>
    /// This method is called at the beginning of each of the API methods
    /// </summary>
    /// <exception cref="Exception">The <see cref="MDB_AuthKey"/> has not been initialized yet</exception>
    private static void CheckForMissingAuthKey()
    {
        if (MDB_AuthKey == null)
        {
            throw new Exception("Missing MDB_AuthKey. Please call API.Initialize() before using the API");
        }
    }

    public static User GetUser(Authentication auth)
    {
        CheckForMissingAuthKey();
        // TODO: get the user and the user's entries
        return new User();
    }

    public static bool ValidLogin(string username, string password)
    {
        CheckForMissingAuthKey();
        return true; // TODO
    }
}
