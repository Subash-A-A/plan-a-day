using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static int noOfLevels = 14;
    public static int currentLevel = 1;
    public static int currentRound = 1 ;
    public static int levelsUnlocked = 1;
    
    [SerializeField] GameObject QuestionPrefab;
    [SerializeField] GameObject RoundPrefab;
    [SerializeField] GameObject AppointmentPrefab;
    [SerializeField] Transform LevelPanel;
    [SerializeField] Transform LevelSelector;
    [SerializeField] Transform AppointmentPanel;
    [SerializeField] GameObject JournalInput;
    [SerializeField] GameObject PathButton;
    [SerializeField] Transform JournalContent;
    [SerializeField] Transform PathButtonContent;
    [SerializeField] GameObject PathSelectPanel;

    private int maxRounds;
    private Appoinment[] currentAppoinments;
    private GameObject currentLevelGameObject;
    private bool isAssigned = false;

    public bool isLevelCat2;
    private bool isJournalContentEmpty = false;

    private void Awake()
    {
        gameObject.SetActive(true);
        StartCoroutine(LoadLevel());
    }
    private void LateUpdate()
    {
        if(isLevelCat2 && isJournalContentEmpty)
        {
            LoadJournalInput();
        }

        PathSelectPanel.SetActive(isLevelCat2);
    }
    private void Update()
    {
        isLevelCat2 = LevelPanel.GetChild(currentLevel - 1).gameObject.GetComponent<Level>().isCat2;
        isJournalContentEmpty = JournalContent.childCount == 0;
        isAssigned = PlayerPrefs.GetInt("ValuesAssigned?") == 1;

        AppoinmentJournalVisibility();
    }
    public IEnumerator LoadLevel()
    {
        yield return new WaitUntil(predicate: () => isAssigned);
        ChangeCurrentLevel();
        UpdateLevel();
        UpdateAppointment();
        if (isLevelCat2)
        {
            LoadJournalInput();
        }
    }
    private void AppoinmentJournalVisibility()
    {   
        foreach(Transform appoinment in AppointmentPanel)
        {
            if (!InJournal(appoinment.name))
            {
                appoinment.gameObject.SetActive(true);
            }
            else
            {
                appoinment.gameObject.SetActive(false);
            }
        }
    }

    private bool InJournal(string name)
    {
        foreach(Transform child in JournalContent)
        {
            if(child.name == name)
            {
                return true;
            }
        }
        return false;
    }
    public void UpdateLevel()
    {   
        for (int i = 0; i < LevelPanel.childCount; i++)
        {
            if((i + 1) == currentLevel)
            {
                LevelPanel.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                LevelPanel.GetChild(i).gameObject.SetActive(false);
            }
        }
        // ChangeCurrentLevel();
        for (int i = 0; i < currentLevelGameObject.transform.childCount; i++)
        {
            if ((i + 1) == currentRound)
            {
                currentLevelGameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                currentLevelGameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void UpdateAppointment()
    {
        // ChangeCurrentLevel();
        foreach (Transform child in AppointmentPanel)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < currentAppoinments.Length; i++)
        {
            GameObject appointment = Instantiate(AppointmentPrefab, AppointmentPanel);
            appointment.transform.GetChild(0).GetComponent<Text>().text = currentAppoinments[i].place;
            appointment.transform.GetChild(1).GetComponent<Text>().text = currentAppoinments[i].time;
            appointment.name = currentAppoinments[i].place;
        }
    }

    public void ChangeCurrentLevel()
    {
        currentLevelGameObject = LevelPanel.GetChild(currentLevel - 1).gameObject;
        maxRounds = currentLevelGameObject.GetComponent<Level>().RoundList.Length;
        currentAppoinments = currentLevelGameObject.GetComponent<Level>().RoundList[currentRound - 1].Appoinments;
    }

    public void LoadJournalInput()
    {   
        string[] answers = currentLevelGameObject.GetComponent<Level>().RoundList[currentRound - 1].Answers;
        string[] paths = currentLevelGameObject.GetComponent<JournalInputData>().roundFromTos[currentRound - 1].fromToPaths;

        int currentAnswerLength = answers.Length;

        foreach (Transform child in JournalContent)
        {
            Destroy(child.gameObject);
        }

        foreach(Transform child in PathButtonContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentAnswerLength; i++)
        {   
            GameObject journalInput = Instantiate(JournalInput, JournalContent);
            GameObject pathButton = Instantiate(PathButton, PathButtonContent);
            
            journalInput.GetComponent<FromToData>().fromTo = paths[i];
            pathButton.GetComponentInChildren<Text>().text = paths[i];
        }
    }
    public Transform GetJournalContent()
    {
        return JournalContent;
    }
    public GameObject GetAppoinmentPanel()
    {
        return AppointmentPanel.gameObject;
    }
    public GameObject GetQuestionPrefab()
    {
        return QuestionPrefab;
    }
    public GameObject GetRoundPrefab()
    {
        return RoundPrefab;
    }
    public GameObject GetLevelSelector()
    {
        return LevelSelector.gameObject;
    }
    public GameObject GetLevelPanel()
    {
        return LevelPanel.gameObject;
    }
    public void GoToNextRoundLevel()
    {
        LevelPanel.gameObject.SetActive(true);
        if (currentRound < maxRounds)
        {
            currentRound++;
        }
        else if(currentLevel == noOfLevels && currentRound == maxRounds)
        {
            currentRound = 1;
            currentLevel = noOfLevels;
        }
        else
        {
            currentRound = 1;
            if(currentLevel < levelsUnlocked)
            {
                currentLevel++;
            }
            else
            {
                levelsUnlocked++;
                currentLevel++;
            }
        }
        
        ChangeCurrentLevel();
        UpdateLevel();
        UpdateAppointment();
    }

    public bool CheckAnswer(string[] journalEntries)
    {
        Level level = LevelPanel.GetChild(currentLevel - 1).GetComponent<Level>();
        string[] answers = level.RoundList[currentRound - 1].Answers;

        if(journalEntries.Length != level.RoundList[currentRound - 1].Answers.Length)
        {
            return false;
        }
        else
        {
            for(int i = 0; i < journalEntries.Length; i++)
            {
                if (journalEntries[i] != answers[i])
                {
                    return false;
                }
            }
        }

        return true;
    }
}
