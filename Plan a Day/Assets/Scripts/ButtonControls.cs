using UnityEngine;

public class ButtonControls : MonoBehaviour
{
    [SerializeField] GameObject leftPanel;
    [SerializeField] GameObject levelSelector;

    public void ShowStreetMap()
    {
        leftPanel.SetActive(false);
    }

    public void ShowLeftPanel()
    {
        leftPanel.SetActive(true);
    }

    public void ShowLevelSelector()
    {
        if (!levelSelector.activeSelf)
        {
            levelSelector.SetActive(true);
        }
    }
}
