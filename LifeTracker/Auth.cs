namespace LifeTracker;

public struct Authentication
{
    public string Username { get; set; }
    public string Password { get; set; }

    public Authentication(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
