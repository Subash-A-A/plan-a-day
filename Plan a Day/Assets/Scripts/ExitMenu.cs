using UnityEngine;

public class ExitMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject ExitMenuPanel;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        ExitMenuPanel.SetActive(false);
    }

    void Update()
    {
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
}
