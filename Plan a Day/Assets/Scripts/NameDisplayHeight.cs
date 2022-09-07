using UnityEngine;
using UnityEngine.UI;
public class NameDisplayHeight : MonoBehaviour
{
    public float displayAtHeight = 5f;
    private contentScript content;
    private void Start()
    {
        content = FindObjectOfType<contentScript>();
    }
    private void OnMouseDown()
    {
        GameObject journalEntry = new GameObject("JournalEntry: " + gameObject.transform.name);
        Text text = journalEntry.AddComponent<Text>();
        text.text = gameObject.transform.name;

        Instantiate(journalEntry, content.transform);
    }
}

