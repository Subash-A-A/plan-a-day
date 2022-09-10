using UnityEngine;
using UnityEngine.UI;

public class UserInstance : MonoBehaviour
{
    [SerializeField] Text uid;
    [SerializeField] InputField timerField;

    private AuthManager authManager;
    private AdminManager adminManager;

    private void Start()
    {
        authManager = FindObjectOfType<AuthManager>();
        adminManager = FindObjectOfType<AdminManager>();
    }

    public void UpdateUserTimer()
    {
        authManager.UpdateTimer(uid.text, timerField.text);
        GameObject popup = adminManager.GetConformationPopup();
        popup.SetActive(true);
    }
}
