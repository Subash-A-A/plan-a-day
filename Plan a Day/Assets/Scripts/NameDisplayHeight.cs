using UnityEngine;
using UnityEngine.UI;
public class NameDisplayHeight : MonoBehaviour
{
    public float displayAtHeight = 5f;

    private contentScript content;
    private LevelManager manager;
    private FlagManager flagManager;

    private void Start()
    {
        content = FindObjectOfType<contentScript>();
        manager = FindObjectOfType<LevelManager>();
        flagManager = FindObjectOfType<FlagManager>();
    }
    private void OnMouseDown()
    {
        if (!manager.GetLevelPanel().activeSelf && !manager.GetLevelSelector().activeSelf && !manager.isLevelCat2)
        {
            GameObject journalEntry = Instantiate(content.GetJournalEntry(), content.transform);
            Text text = journalEntry.GetComponent<Text>();
            text.text = transform.name;
            journalEntry.name = transform.name;

            // Flag position is always at child0
            Transform flagPosition = transform.GetChild(0);
            flagManager.SetFlagTransform(flagPosition.position, flagPosition.rotation);
        }
        
    }
}

