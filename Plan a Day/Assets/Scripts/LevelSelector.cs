using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] GameObject levelUnitUnlocked;
    [SerializeField] GameObject levelUnitLocked;
    [SerializeField] Transform Grid;

    private int level;

    private void Awake()
    {
        DisplayLevels();
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    void DisplayLevels()
    {
        level = 1;
        for (; level <= LevelManager.noOfLevels; level++)
        {
            GameObject choosePrefab = (level <= LevelManager.levelsUnlocked) ? levelUnitUnlocked : levelUnitLocked;
            GameObject gridElement = Instantiate(choosePrefab, Grid);
            Text levelNumber = gridElement.GetComponentInChildren<Text>();
            levelNumber.text = level.ToString();
        }
    }
    public void CloseLevelSelector()
    {
        gameObject.SetActive(false);
    }
}
