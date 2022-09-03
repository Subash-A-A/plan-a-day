using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int noOfLevels = 10;
    public int currentLevel = 1;
    public int levelsUnlocked = 5;

    [SerializeField] GameObject QuestionPrefab;
    [SerializeField] Transform LevelPanel;

    private void Start()
    {
        gameObject.SetActive(true);
        UpdateLevel();
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
    }

    public GameObject GetQuestionPrefab()
    {
        return QuestionPrefab;
    }

    public GameObject GetLevelPanel()
    {
        return LevelPanel.gameObject;
    }
}
