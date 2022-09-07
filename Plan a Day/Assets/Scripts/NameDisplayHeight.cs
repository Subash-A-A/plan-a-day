using UnityEngine;
using UnityEngine.UI;
public class NameDisplayHeight : MonoBehaviour
{
    public float displayAtHeight = 5f;

    private contentScript content;
    private LevelManager manager;

    private void Start()
    {
        content = FindObjectOfType<contentScript>();
        manager = FindObjectOfType<LevelManager>();
    }
    private void OnMouseDown()
    {
        if (!manager.GetLevelPanel().activeSelf)
        {
            GameObject journalEntry = Instantiate(content.GetJournalEntry(), content.transform);
            Text text = journalEntry.GetComponent<Text>();
            text.text = transform.name;
        }
    }
}

