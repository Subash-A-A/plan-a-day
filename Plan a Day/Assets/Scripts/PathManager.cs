using UnityEngine;
using UnityEngine.UI;

public class PathManager : MonoBehaviour
{
    public Color normal;
    public Color highlighted;
    [SerializeField] LayerMask pathMask;
    
    private LevelManager levelManager;
    private Transform journalContent;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        journalContent = levelManager.GetJournalContent();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.SphereCast(ray, 0.5f, out RaycastHit hitInfo, 999f, pathMask))
        {
            HighlightPath(hitInfo.transform.parent);
        }
        else
        {
            foreach(Transform pathTransform in transform)
            {
                Path path = pathTransform.GetComponent<Path>();
                path.highlighted = false;
            }
        }

        ShowPath();
    }

    private void HighlightPath(Transform pathTransform)
    {
        Path pathInfo = pathTransform.GetComponent<Path>();
        pathInfo.highlighted = true;
    }

    private void ShowPath()
    {
        foreach(Transform pathTransform in transform)
        {
            Path path = pathTransform.GetComponent<Path>();
            path.ShowPath();
        }
    }

    public void EnterPathDistanceJournal(string fromTo, float length)
    {
        foreach(Transform journal in journalContent)
        {
            if (journal.GetComponent<FromToData>().fromTo.Equals(fromTo))
            {
                journal.GetComponent<InputField>().text = length + "";
            }
        }
    }
}
