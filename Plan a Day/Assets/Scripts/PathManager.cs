using UnityEngine;
using UnityEngine.UI;

public class PathManager : MonoBehaviour
{
    public Color normal;
    public Color highlighted;
    [SerializeField] LayerMask pathMask;
    [SerializeField] LayerMask overlayMask;
    
    private LevelManager levelManager;
    private Transform journalContent;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        journalContent = levelManager.GetJournalContent();
    }

    private void Update()
    {

        if (!levelManager.isLevelCat2)
        {
            HidePaths();
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.SphereCast(ray, 1f, out RaycastHit hitInfo, 999f, pathMask))
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
        
    }

    private void HighlightPath(Transform pathTransform)
    {
        Path pathInfo = pathTransform.GetComponent<Path>();
        pathInfo.highlighted = true;
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

    public void ShowSelectedPaths(string fromTo)
    {   
        foreach (Transform pathTransform in transform)
        {
            Path path = pathTransform.GetComponent<Path>();

            if (path.fromTo.Equals(fromTo))
            {
                path.ShowPath();
            }
            else
            {
                pathTransform.gameObject.SetActive(false);
            }
        }
    }

    public void HidePaths()
    {
        foreach (Transform pathTransform in transform)
        {
            pathTransform.gameObject.SetActive(false);
        }
    }
}
