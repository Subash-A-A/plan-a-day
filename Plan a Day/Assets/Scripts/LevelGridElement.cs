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
        manager.currentLevel = levelNumber;
        selector.gameObject.SetActive(false);
        manager.UpdateLevel();
        manager.UpdateAppointment();
    }
}
