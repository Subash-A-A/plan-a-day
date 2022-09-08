using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Auth;

public class ButtonControls : MonoBehaviour
{
    [SerializeField] GameObject leftPanel;
    [SerializeField] LevelSelector levelSelector;
    [SerializeField] Transform journalContent;
    [SerializeField] LevelManager levelManager;
    [SerializeField] GameObject LevelCompletePopup;
    
    [SerializeField] Transform Buildings;
    [SerializeField] GameObject BackButtonGameObject;

    private FlagManager flagManager;

    private void Start()
    {
        flagManager = FindObjectOfType<FlagManager>();
        LevelCompletePopup.SetActive(false);
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
        if (!levelSelector.gameObject.activeSelf)
        {
            levelSelector.gameObject.SetActive(true);
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
    public void FinishButton()
    {
        string[] arr = GetJournalEntries();
        if (levelManager.CheckAnswer(arr))
        {
            levelManager.GoToNextRoundLevel();
            levelSelector.UpdateLevel();
            ClearJournal();
            MoveFlag("Home");
            ShowLevelCompletePopup();
            UpdateData();
        }
        else
        {
            Debug.LogError("Wrong Answer");
        }
    }

    private void ClearJournal()
    {
        foreach (Transform journalEntry in journalContent)
        {
            Destroy(journalEntry.gameObject);
        }
    }

    public void UpdateData()
    {
        AuthManager authManager = FindObjectOfType<AuthManager>();
        authManager.UpdateUserData(LevelManager.levelsUnlocked, LevelManager.currentLevel, LevelManager.currentRound);
    }

    private string[] GetJournalEntries()
    {
        string[] arr = new string[journalContent.childCount];

        for(int i = 0; i < journalContent.childCount; i++)
        {
            arr[i] = journalContent.GetChild(i).GetComponent<Text>().text;
        }
        foreach(string journalEntry in arr)
        {
            Debug.Log(journalEntry);
        }
        return arr;
    }
    public void CloseLevelSelector()
    {
        levelSelector.gameObject.SetActive(false);
    }
    public void CloseLevelCompletePopup()
    {
        LevelCompletePopup.SetActive(false);
    }
    private void ShowLevelCompletePopup()
    {
        LevelCompletePopup.SetActive(true);
    }
}
