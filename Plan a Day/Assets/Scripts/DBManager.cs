using UnityEngine;
using Firebase.Database;

public class DBManager : MonoBehaviour
{
    private static DatabaseReference dbReference = FirebaseDatabase.DefaultInstance.RootReference;

    public static void CreateUser(string email, string userID, int currentLevel, int currentRound, int levelsUnlocked, bool isAdmin)
    {
        User newUser = new User(email, userID, currentLevel, currentRound, levelsUnlocked, isAdmin);
        string json = JsonUtility.ToJson(newUser);
        dbReference.Child("user").Child(userID).SetRawJsonValueAsync(json);
        Debug.Log("User Created");
    }

}
