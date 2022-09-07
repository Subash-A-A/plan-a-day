using UnityEngine;
using UnityEngine.UI;

public class LevelGridElement : MonoBehaviour
{
    private LevelSelector selector;
    private LevelManager manager;
    private Text levelText;

    private int levelNumber;
    private void Awake()
    {
        levelText = GetComponentInChildren<Text>();
        selector = GetComponentInParent<LevelSelector>();
    }
    private void Start()
    {
        levelNumber = int.Parse(levelText.text);
    }
    public void ChangeLevel()
    {
        LevelManager.currentLevel = levelNumber;
        LevelManager.currentRound = 1;
        selector.gameObject.SetActive(false);
        manager.UpdateLevel();
        manager.UpdateAppointment();
    }
}
