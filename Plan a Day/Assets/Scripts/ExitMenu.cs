using UnityEngine;

public class ExitMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject ExitMenuPanel;
    [SerializeField] GameObject SignoutButon;
    private AuthManager authManager;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        authManager = FindObjectOfType<AuthManager>();
        ExitMenuPanel.SetActive(false);
    }

    void Update()
    {   
        if(authManager.auth.CurrentUser != null)
        {
            SignoutButon.SetActive(true);
        }
        else
        {
            SignoutButon.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ExitMenuPanel.activeSelf)
            {
                ExitMenuPanel.SetActive(false);
            }
            else
            {
                ExitMenuPanel.SetActive(true);
            }
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    public void SignOut()
    {   
        authManager.SignOut();
        ExitMenuPanel.SetActive(false);
    }
}
