using Firebase;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class AuthManager : MonoBehaviour
{
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public DatabaseReference DBreference;

    [Header("Login")]
    public InputField emailLoginField;
    public InputField passwordLoginField;
    public Text warningLoginText;
    public Text confirmLoginText;

    [Header("Register")]
    public InputField usernameRegisterField;
    public InputField emailRegisterField;
    public InputField passwordRegisterField;
    public InputField confirmPasswordRegisterField;
    public Text warningRegisterText;

    private bool isAdmin;

    [SerializeField] LoginPage page;
    [SerializeField] GameObject UserInstancePrefab;
    [SerializeField] Transform usersContent;

    string time;
    [Header("Debugging")]
    public bool DebuggingMode;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        InitializeFirebase();
        // FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        // {
        //     dependencyStatus = task.Result;
        //     if (dependencyStatus == DependencyStatus.Available)
        //     {
        //         InitializeFirebase();
        //     }
        //     else
        //     {
        //         Debug.Log("Could not resolve all firebase dependencies : " + dependencyStatus);
        //     }
        // });
    }
    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void LoginButton()
    {
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));

    }

    public void RegisterButton()
    {
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            user = LoginTask.Result;

            if (user.IsEmailVerified || DebuggingMode)
            {
                Debug.LogFormat("User signed in successfully: {0} ({1})", user.DisplayName, user.Email);
                warningLoginText.text = "";
                confirmLoginText.text = "Logged In";
                PlayerPrefs.SetString("email", _email);
                PlayerPrefs.SetString("password", _password);
                PlayerPrefs.Save();

                LoadUserData();
            }
            else
            {
                Debug.Log("Email Not Verified!");
            }
        }
    }

    private void LoadNextPage()
    {
        if (isAdmin == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            GetUsers();
            page.openAdminAuthPage();
        }
    }

    public void SignOut()
    {
        if (!isAdmin)
        {
            FindObjectOfType<ButtonControls>().UpdateData();
        }

        Destroy(FindObjectOfType<ExitMenu>().gameObject);
        auth.SignOut();
        Destroy(FindObjectOfType<AuthManager>().gameObject);

        SceneManager.LoadScene(0);
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            warningRegisterText.text = "Missing Username";
        }
        else if (passwordRegisterField.text != confirmPasswordRegisterField.text)
        {
            //If the password does not match show a warning
            warningRegisterText.text = "Password Does Not Match!";
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                user = RegisterTask.Result;

                if (user != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = user.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        LoginPage.FindObjectOfType<LoginPage>().openLoginPage();
                        warningRegisterText.text = "";
                        DBManager.CreateUser(_username, _email, user.UserId, 1, 1, 1, false, "30:00");
                        user.SendEmailVerificationAsync();
                    }
                }
            }
        }
    }

    private void LoadUserData()
    {
        //Get the currently logged in user data\
        FirebaseDatabase.DefaultInstance.RootReference.Child("user").Child(user.UserId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Error");
            }
            else if (task.IsCompleted)
            {
                PlayerPrefs.SetInt("ValuesAssigned?", 0);
                DataSnapshot snapshot = task.Result;
                LevelManager.levelsUnlocked = int.Parse(snapshot.Child("levelsUnlocked").GetValue(false).ToString());
                LevelManager.currentLevel = int.Parse(snapshot.Child("currentLevel").GetValue(false).ToString());
                LevelManager.currentRound = int.Parse(snapshot.Child("currentRound").GetValue(false).ToString());
                isAdmin = bool.Parse(snapshot.Child("isAdmin").GetValue(false).ToString());
                time = snapshot.Child("timer").GetValue(false).ToString();

                Debug.Log(LevelManager.levelsUnlocked + " " + LevelManager.currentLevel + " " + LevelManager.currentRound);

                if (isAdmin == false)
                {
                    float second = int.Parse(time.Split(":")[1]);
                    float minute = int.Parse(time.Split(":")[0]);
                    CountDownTimer.startMinute = minute;
                    CountDownTimer.startSecond = second;
                }

                PlayerPrefs.SetInt("ValuesAssigned?", 1);
                LoadNextPage();
            }
        });
    }
    public void UpdateUserData(int levelsUnlocked, int currentLevel, int currentRound, string timer)
    {
        DBreference.Child("user").Child(user.UserId).Child("levelsUnlocked").SetValueAsync(levelsUnlocked);
        DBreference.Child("user").Child(user.UserId).Child("currentLevel").SetValueAsync(currentLevel);
        DBreference.Child("user").Child(user.UserId).Child("currentRound").SetValueAsync(currentRound);
        DBreference.Child("user").Child(user.UserId).Child("timer").SetValueAsync(timer);
    }
    public void GetUsers()
    {   
        foreach(Transform child in usersContent)
        {
            Destroy(child.gameObject);
        }

        FirebaseDatabase.DefaultInstance.RootReference.Child("user").GetValueAsync().ContinueWithOnMainThread(task => 
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                string json = snapshot.GetRawJsonValue();
                var values = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);
                foreach (var uni in values)
                {   
                    bool isAdmin = bool.Parse(uni.Value["isAdmin"]);
                    string timer = uni.Value["timer"];
                    string userID = uni.Value["userID"];
                    string username = uni.Value["username"];

                    if (!isAdmin)
                    {
                        GameObject userInstance = Instantiate(UserInstancePrefab, usersContent);
                        userInstance.name = username;
                        userInstance.transform.GetChild(0).GetComponent<Text>().text = username;
                        userInstance.transform.GetChild(1).GetComponent<Text>().text = userID;
                        userInstance.transform.GetChild(2).GetComponent<InputField>().text = timer;
                    }
                }
            }
        });
    }

    public void UpdateTimer(string uid, string val)
    {
        DBreference.Child("user").Child(uid).Child("timer").SetValueAsync(val);
    }

    public void RefreshUsers()
    {
        foreach(Transform child in usersContent)
        {
            Destroy(child.gameObject);
        }

        GetUsers();
    }
}
