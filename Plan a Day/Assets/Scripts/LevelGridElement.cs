using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelGridElement : MonoBehaviour
{
    private LevelSelector selector;
    private LevelManager manager;
    private Text levelText;

    private int levelNumber;
    private void Awake()
    {
        manager = FindObjectOfType<LevelManager>();
        levelText = GetComponentInChildren<Text>();
        selector = GetComponentInParent<LevelSelector>();
    }
    private void Start()
    {
        levelNumber = int.Parse(levelText.text);
    }
    public void ChangeLevel()
    {
        LevelManager.currentRound = 1;
        LevelManager.currentLevel = levelNumber;
        StartCoroutine(UpdateLevelAppoinments());
    }

    IEnumerator UpdateLevelAppoinments()
    {
        bool check = LevelManager.currentLevel == levelNumber && LevelManager.currentRound == 1;
        yield return new WaitUntil(predicate: () => check);

        manager.UpdateLevel();
        manager.UpdateAppointment();
        selector.gameObject.SetActive(false);
    }
}
