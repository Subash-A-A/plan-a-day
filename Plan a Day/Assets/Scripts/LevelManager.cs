using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int noOfLevels = 10;
    public int currentLevel = 1;

    [SerializeField] GameObject QuestionPrefab;

    private void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if((i + 1) == currentLevel)
            {   
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public GameObject GetQuestionPrefab()
    {
        return QuestionPrefab;
    }
}
