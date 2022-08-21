using UnityEngine;

public class ButtonControls : MonoBehaviour
{
    [SerializeField] GameObject levelManager;
    [SerializeField] GameObject levelSelector;

    public void ShowStreetMap()
    {
        levelManager.SetActive(false);
    }

    public void ShowLeftPanel()
    {
        levelManager.SetActive(true);
    }

    public void ShowLevelSelector()
    {
        levelSelector.SetActive(true);
    }
}
