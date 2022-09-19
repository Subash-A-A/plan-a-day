using UnityEngine;
using UnityEngine.UI;

public class UserInstance : MonoBehaviour
{
    [SerializeField] Text uid;
    [SerializeField] InputField timerField;

    private AuthManager authManager;
    private AdminManager adminManager;
    private Image bg;

    private void Start()
    {
        bg = GetComponent<Image>();
        authManager = FindObjectOfType<AuthManager>();
        adminManager = FindObjectOfType<AdminManager>();
    }

    public void UpdateUserTimer()
    {
        authManager.UpdateTimer(uid.text, timerField.text);
        StartCoroutine(adminManager.GetConformationPopup(bg));
    }
}
