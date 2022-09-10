public class User
{
    public string username;
    public string email;
    public string userID;
    public int currentLevel;
    public int currentRound;
    public int levelsUnlocked;
    public bool isAdmin;
    public string timer;

    public User()
    {
    }

    public User(string username, string email, string userID, int currentLevel, int currentRound, int levelsUnlocked, bool isAdmin, string timer)
    {
        this.username = username;
        this.email = email;
        this.userID = userID;
        this.currentLevel = currentLevel;
        this.currentRound = currentRound;
        this.levelsUnlocked = levelsUnlocked;
        this.isAdmin = false;
        this.timer = timer;
    }
}
