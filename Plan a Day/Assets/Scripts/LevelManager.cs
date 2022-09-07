using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int noOfLevels = 10;
    public int currentLevel = 1;
    public int currentRound = 1;
    public int levelsUnlocked = 5;
    
    [SerializeField] GameObject QuestionPrefab;
    [SerializeField] GameObject RoundPrefab;
    [SerializeField] GameObject AppointmentPrefab;
    [SerializeField] Transform LevelPanel;
    [SerializeField] Transform AppointmentPanel;

    private int maxRounds;
    private Appoinment[] currentAppoinments;
    private GameObject currentLevelGameObject;

    private void Awake()
    {
        gameObject.SetActive(true);
    }

    private void Start()
    {
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
        for(int i = 0; i < currentLevelGameObject.transform.childCount; i++)
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
            if(!AppointmentPanel.GetChild(0).Equals(child))
            {
                Destroy(child.gameObject);
            }
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

    public GameObject GetLevelPanel()
    {
        return LevelPanel.gameObject;
    }

    public void GoToNextRoundLevel()
    {
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
}
