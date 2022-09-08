using UnityEngine;
using UnityEngine.UI;

public class ButtonControls : MonoBehaviour
{
    [SerializeField] GameObject leftPanel;
    [SerializeField] GameObject levelSelector;
    [SerializeField] Transform journalContent;
    
    [SerializeField] Transform Buildings;
    [SerializeField] GameObject BackButtonGameObject;

    private FlagManager flagManager;

    private void Start()
    {
        flagManager = FindObjectOfType<FlagManager>();
    }

    private void Update()
    {
        if (journalContent.childCount <= 0)
        {
            BackButtonGameObject.SetActive(false);
        }
        else
        {
            BackButtonGameObject.SetActive(true);
        }
    }

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
    public void BackButton()
    {
        if (journalContent.childCount > 1)
        {
            Transform lastChildTransform = journalContent.GetChild(journalContent.childCount - 1);
            Transform secondLastChildTransform = journalContent.GetChild(journalContent.childCount - 1 - 1);
            string buildingName = secondLastChildTransform.GetComponent<Text>().text;
            
            Destroy(lastChildTransform.gameObject);
            MoveFlag(buildingName);
        }
        else
        {
            Transform lastChildTransform = journalContent.GetChild(0);
            Destroy(lastChildTransform.gameObject);
            MoveFlag("Home");
        }
        
    }

    private void MoveFlag(string buildingName)
    {   
        foreach(Transform building in Buildings)
        {
            if (building.name.Equals(buildingName))
            {
                flagManager.SetFlagTransform(building.GetChild(0).position, building.GetChild(0).rotation);
                return;
            }
        }
    }
}
