using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public string fromTo;
    public float pathLength;
    public bool highlighted = false;

    private PathManager pathManager;

    private void Start()
    {
        pathManager = FindObjectOfType<PathManager>();
    }

    private void Update()
    {
        if (highlighted)
        {
            foreach(Transform square in transform)
            {
                SpriteRenderer renderer = square.GetComponent<SpriteRenderer>();
                renderer.color = pathManager.highlighted;
            }
        }
        else
        {
            foreach (Transform square in transform)
            {
                SpriteRenderer renderer = square.GetComponent<SpriteRenderer>();
                renderer.color = pathManager.normal;
            }
        }
    }
}
