using UnityEngine;
using System.Collections;
using Firebase.Database;

public class DBManager : MonoBehaviour
{
    private static DatabaseReference dbReference = FirebaseDatabase.DefaultInstance.RootReference;

    public static void CreateUser(string email, string userID, int currentLevel, int currentRound, int levelsUnlocked)
    {
        User newUser = new User(email, userID, currentLevel, currentRound, levelsUnlocked);
        string json = JsonUtility.ToJson(newUser);
        dbReference.Child("user").Child(userID).SetRawJsonValueAsync(json);
        Debug.Log("User Created");
    }
   
}
