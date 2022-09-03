using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;

public class DBManager : MonoBehaviour
{
    [SerializeField] InputField userNameInput;
    [SerializeField] InputField passwordInput;
    private string userID;
    private DatabaseReference dbReference;

    private void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void CreateUser()
    {
        User newUser = new User(userNameInput.text, passwordInput.text);
        string json = JsonUtility.ToJson(newUser);

        dbReference.Child("user").Child(userID).SetRawJsonValueAsync(json);
    }
}
