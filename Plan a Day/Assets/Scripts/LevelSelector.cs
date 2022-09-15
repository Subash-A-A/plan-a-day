using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] GameObject levelUnitUnlocked;
    [SerializeField] GameObject levelUnitLocked;
    [SerializeField] Transform Grid;

    private int level;
    private bool isAssigned = false;

    private void Awake()
    {
        gameObject.SetActive(true);
    }

    private void Start()
    {
        StartCoroutine(DisplayLevels());
    }

    private void Update()
    {
        isAssigned = PlayerPrefs.GetInt("ValuesAssigned?") == 1;
    }

    IEnumerator DisplayLevels()
    {   

        yield return new WaitUntil(predicate: () => isAssigned);
        UpdateLevel();
    }
    public void UpdateLevel()
    {
        DeleteChildren();
        level = 1;
        for (; level <= LevelManager.noOfLevels; level++)
        {
            GameObject choosePrefab = (level <= LevelManager.levelsUnlocked) ? levelUnitUnlocked : levelUnitLocked;
            GameObject gridElement = Instantiate(choosePrefab, Grid);
            Text levelNumber = gridElement.GetComponentInChildren<Text>();
            levelNumber.text = level.ToString();
        }
    }
    public void DeleteChildren()
    {
        foreach (Transform child in Grid.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
