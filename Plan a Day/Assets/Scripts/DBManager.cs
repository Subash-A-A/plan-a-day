using UnityEngine;
using Firebase.Database;

public class DBManager : MonoBehaviour
{
    private static DatabaseReference dbReference = FirebaseDatabase.DefaultInstance.RootReference;

    public static void CreateUser(string username, string email, string userID, int currentLevel, int currentRound, int levelsUnlocked, bool isAdmin, string timer)
    {
        User newUser = new User(username, email, userID, currentLevel, currentRound, levelsUnlocked, isAdmin, timer);
        string json = JsonUtility.ToJson(newUser);
        dbReference.Child("user").Child(userID).SetRawJsonValueAsync(json);
        Debug.Log("User Created");
    }

}
