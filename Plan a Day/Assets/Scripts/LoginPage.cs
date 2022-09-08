using UnityEngine.UI;
using UnityEngine;

public class LoginPage : MonoBehaviour
{
    [SerializeField] GameObject login;
    [SerializeField] GameObject register;
    [SerializeField] GameObject adminAuth;
    [SerializeField] GameObject timerPage;

    void Start()
    {
        login.SetActive(true);
        register.SetActive(false);
    }

    public void openAdminAuthPage()
    {
        login.SetActive(false);
        adminAuth.SetActive(true);
    }

    public void openRegistrationPage()
    {
        login.SetActive(false);
        register.SetActive(true);
    }

    public void openTimerPage()
    {
        adminAuth.SetActive(false);
        timerPage.SetActive(true);
    }

    public void openLoginPage()
    {
        login.SetActive(true);
        register.SetActive(false);
    }
}
