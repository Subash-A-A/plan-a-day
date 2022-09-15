using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelGridElement : MonoBehaviour
{
    private LevelSelector selector;
    private LevelManager manager;
    private Text levelText;
    private ButtonControls buttonControls;

    private int levelNumber;
    private void Awake()
    {
        manager = FindObjectOfType<LevelManager>();
        buttonControls = FindObjectOfType<ButtonControls>();
        levelText = GetComponentInChildren<Text>();
        selector = GetComponentInParent<LevelSelector>();
    }
    private void Start()
    {
        levelNumber = int.Parse(levelText.text);
    }
    public void ChangeLevel()
    {
        buttonControls.ClearJournal();
        LevelManager.currentRound = 1;
        LevelManager.currentLevel = levelNumber;
        StartCoroutine(UpdateLevelAppoinments());
    }

    IEnumerator UpdateLevelAppoinments()
    {
        bool check = LevelManager.currentLevel == levelNumber && LevelManager.currentRound == 1;
        yield return new WaitUntil(predicate: () => check);

        manager.ChangeCurrentLevel();
        manager.UpdateLevel();
        manager.UpdateAppointment();

        if (manager.isLevelCat2)
        {
            manager.LoadJournalInput();
        }

        selector.gameObject.SetActive(false);
    }
}
