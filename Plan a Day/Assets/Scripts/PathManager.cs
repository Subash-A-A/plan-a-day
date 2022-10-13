using UnityEngine;

public class PathManager : MonoBehaviour
{
    public Color normal;
    public Color highlighted;
    [SerializeField] LayerMask pathMask;
    
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hitInfo, 999f, pathMask))
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
}
