using UnityEngine;
using UnityEngine.UI;

public class PathButton : MonoBehaviour
{   
    private PathManager pathManager;
    private void Start()
    {
        pathManager = FindObjectOfType<PathManager>();
    }
    public void ShowPath()
    {
        string pathName = GetComponentInChildren<Text>().text;
        pathManager.ShowSelectedPaths(pathName);
    }
}
