using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Round[] RoundList;
    private LevelManager manager;

    private void Awake()
    {
        manager = FindObjectOfType<LevelManager>();
    }

    private void Start()
    {
        for(int i = 0; i < RoundList.Length; i++)
        {
            GameObject round = Instantiate(manager.GetRoundPrefab(), transform);
            round.transform.name = "Round: " + (i + 1);

            for (int j = 0; j < RoundList[i].Question.Length; j++)
            {
                GameObject question = Instantiate(manager.GetQuestionPrefab(), round.transform);
                question.name = "Question: " + (j + 1);
                if (question.TryGetComponent<Text>(out Text questText))
                {
                    questText.text = RoundList[i].Question[j];
                }
            }

            if ((i + 1) != manager.currentRound)
            {
                round.SetActive(false);
            }
        }
    }
}
