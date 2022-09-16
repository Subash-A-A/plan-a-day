using UnityEngine;

public class Points : MonoBehaviour
{
    public Transform[] points;

    [SerializeField] GameObject Distance;
    [SerializeField] Material pointMat;

    [SerializeField] Color pointInitialColor;
    [SerializeField] Color lineInitialColor;
    [SerializeField] Color finalColor;

    private LineRenderer lineRenderer;
    private Color currentPointColor;
    private Color currentLineColor;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        currentPointColor = finalColor;
        currentLineColor = finalColor;
        
        lineRenderer.enabled = true;
        Distance.SetActive(false);

        SetPoints();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            DrawLine();
        }
        else
        {
            ClearLine();
        }
        ColorLerper();
    }

    void SetPoints()
    {   
        lineRenderer.positionCount = points.Length;
        for(int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }
    }

    void DrawLine()
    {
        Distance.SetActive(true);
        currentPointColor = pointInitialColor;
        currentLineColor = lineInitialColor;
    }
    void ClearLine()
    {
        Distance.SetActive(false);
        currentPointColor = finalColor;
        currentLineColor = finalColor;
    }

    void ColorLerper()
    {
        pointMat.color = Color.Lerp(pointMat.color, currentPointColor, 10 * Time.deltaTime);
        lineRenderer.startColor = Color.Lerp(lineRenderer.startColor, currentLineColor, 10 * Time.deltaTime);
        lineRenderer.endColor = Color.Lerp(lineRenderer.endColor, currentLineColor, 10 * Time.deltaTime);
    }
}
