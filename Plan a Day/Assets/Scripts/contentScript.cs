using UnityEngine;
using UnityEngine.UI;

public class contentScript : MonoBehaviour
{
    [SerializeField] GameObject journalEntry;

    public GameObject GetJournalEntry()
    {
        return journalEntry;
    }
}
