public class User
{
    public string email;
    public string userID;
    public int currentLevel;
    public int currentRound;
    public int levelsUnlocked;

    public User()
    {
    }

    public User(string email, string userID, int currentLevel, int currentRound, int levelsUnlocked)
    {
        this.email = email;
        this.userID = userID;
        this.currentLevel = currentLevel;
        this.currentRound = currentRound;
        this.levelsUnlocked = levelsUnlocked;
    }
}
