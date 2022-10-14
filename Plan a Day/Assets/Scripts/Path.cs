using UnityEngine;

public class Path : MonoBehaviour
{
    [System.Serializable]
    public class LevelRound
    {
        public int level;
        public int round;
    }

    public string fromTo;
    public float pathLength;
    public bool highlighted = false;

    public LevelRound[] levelRounds;

    private PathManager pathManager;
    private float yPos;

    private void Start()
    {
        pathManager = FindObjectOfType<PathManager>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            
            if(highlighted && Input.GetMouseButtonDown(0))
            {
                Debug.Log(fromTo+ ", Distance: " + pathLength + " path is Clicked!");
                pathManager.EnterPathDistanceJournal(fromTo, pathLength);
            }

            else if (highlighted)
            {
                foreach (Transform square in transform)
                {
                    SpriteRenderer renderer = square.GetComponent<SpriteRenderer>();
                    renderer.color = pathManager.highlighted;
                }
                yPos = 0.5f;
            }
            else
            {
                foreach (Transform square in transform)
                {
                    SpriteRenderer renderer = square.GetComponent<SpriteRenderer>();
                    renderer.color = pathManager.normal;
                }

                yPos = 0f;
            }

            YPosLerper();
        }
    }

    private void YPosLerper()
    {
        Vector3 pos = new Vector3(transform.position.x, yPos, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, pos, 10 * Time.deltaTime);
    }
    public void ShowPath()
    {
        foreach (LevelRound levelRound in levelRounds)
        {
            if (levelRound.level == LevelManager.currentLevel && levelRound.round == LevelManager.currentRound)
            {
                gameObject.SetActive(true);
                return;
            }
        }
        gameObject.SetActive(false);
    }
}
