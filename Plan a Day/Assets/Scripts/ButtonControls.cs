using UnityEngine;
using UnityEngine.UI;

public class ButtonControls : MonoBehaviour
{
    [SerializeField] GameObject leftPanel;
    [SerializeField] LevelSelector levelSelector;
    [SerializeField] Transform journalContent;
    [SerializeField] LevelManager levelManager;
    [SerializeField] GameObject LevelCompletePopup;
    [SerializeField] GameObject WrongAnswerPopup;
    [SerializeField] Text Timer;
    
    [SerializeField] Transform Buildings;
    [SerializeField] GameObject BackButtonGameObject;

    private FlagManager flagManager;
    private AuthManager authManager;

    private void Start()
    {
        authManager = FindObjectOfType<AuthManager>();
        flagManager = FindObjectOfType<FlagManager>();
        LevelCompletePopup.SetActive(false);
        WrongAnswerPopup.SetActive(false);
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
            WrongAnswerPopup.SetActive(true);
            ClearJournal();
            MoveFlag("Home");
        }
        if (levelManager.isLevelCat2)
        {
            levelManager.LoadJournalInput();
        }
    }

    public void ClearJournal()
    {
        foreach (Transform journalEntry in journalContent)
        {
            Destroy(journalEntry.gameObject);
        }
    }

    public void UpdateData()
    {
        authManager.UpdateUserData(LevelManager.levelsUnlocked, LevelManager.currentLevel, LevelManager.currentRound, Timer.text);
    }

    private string[] GetJournalEntries()
    {
        string[] arr = new string[journalContent.childCount];
        
        for (int i = 0; i < journalContent.childCount; i++)
        {   
            if(journalContent.GetChild(i).TryGetComponent<InputField>(out InputField field))
            {
                arr[i] = field.text;
            }
            else if(journalContent.GetChild(i).TryGetComponent<Text>(out Text answer))
            {
                arr[i] = answer.text;
            }
        }
        
        return arr;
    }
    public void CloseButton(GameObject target)
    {
        target.SetActive(false);
    }
    private void ShowLevelCompletePopup()
    {
        LevelCompletePopup.SetActive(true);
    }

    private void OnApplicationQuit()
    {
        UpdateData();
    }
}
