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

    private int maxRounds;
    private Appoinment[] currentAppoinments;
    private GameObject currentLevelGameObject;
    private bool isAssigned = false;

    private void Awake()
    {
        gameObject.SetActive(true);
        StartCoroutine(LoadLevel());
    }

    private void Update()
    {
        isAssigned = PlayerPrefs.GetInt("ValuesAssigned?") == 1;
    }
    public IEnumerator LoadLevel()
    {
        yield return new WaitUntil(predicate: () => isAssigned);
        ChangeCurrentLevel();
        UpdateLevel();
        UpdateAppointment();
    }
    public void UpdateLevel()
    {
        for(int i = 0; i < LevelPanel.childCount; i++)
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
        ChangeCurrentLevel();
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
        ChangeCurrentLevel();
        foreach (Transform child in AppointmentPanel)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < currentAppoinments.Length; i++)
        {
            GameObject appointment = Instantiate(AppointmentPrefab, AppointmentPanel);
            appointment.transform.GetChild(0).GetComponent<Text>().text = currentAppoinments[i].place;
            appointment.transform.GetChild(1).GetComponent<Text>().text = currentAppoinments[i].time;
        }
    }

    private void ChangeCurrentLevel()
    {
        currentLevelGameObject = LevelPanel.GetChild(currentLevel - 1).gameObject;
        maxRounds = currentLevelGameObject.GetComponent<Level>().RoundList.Length;
        currentAppoinments = currentLevelGameObject.GetComponent<Level>().RoundList[currentRound - 1].Appoinments;
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
        if(currentRound < maxRounds)
        {
            currentRound++;
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
        
        if(journalEntries.Length != level.RoundList[currentRound - 1].Answers.Length)
        {
            return false;
        }
        else
        {
            for(int i = 0; i < journalEntries.Length; i++)
            {
                if (journalEntries[i] != level.RoundList[currentRound - 1].Answers[i])
                {
                    return false;
                }
            }
        }

        return true;
    }
}
