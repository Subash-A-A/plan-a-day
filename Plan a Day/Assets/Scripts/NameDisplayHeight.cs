using UnityEngine;
using UnityEngine.UI;
public class NameDisplayHeight : MonoBehaviour
{
    public float displayAtHeight = 5f;
    private Text tasks;
    private contentScript content;
    private void Start()
    {
        content = FindObjectOfType<contentScript>();
        tasks = content.gameObject.GetComponent<Text>();
    }
    private void OnMouseDown()

    {
        string txt = tasks.text;
        tasks.text = txt + "\n" + gameObject.transform.name;
        Debug.Log(gameObject.transform.name);
    }
}

