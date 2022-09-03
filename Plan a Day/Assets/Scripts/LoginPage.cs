using UnityEngine.UI;
using UnityEngine;

public class LoginPage : MonoBehaviour
{
    [SerializeField] GameObject login;
    [SerializeField] GameObject register;

    void Start()
    {
        login.SetActive(true);
        register.SetActive(false);
    }

    public void openRegistrationPage()
    {
        login.SetActive(false);
        register.SetActive(true);
    }

    public void openLoginPage()
    {
        login.SetActive(true);
        register.SetActive(false);
    }
}
