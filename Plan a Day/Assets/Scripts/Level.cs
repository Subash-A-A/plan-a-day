using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Round[] RoundList;
    [SerializeField] Transform Panel;

    private LevelManager manager;


    private void Awake()
    {
        manager = GetComponentInParent<LevelManager>();
    }

    private void Start()
    {
        for(int i = 0; i < RoundList.Length; i++)
        {
            for(int j = 0; j < RoundList[i].Question.Length; j++)
            {
                GameObject question = Instantiate(manager.GetQuestionPrefab(), Panel);
                question.name = "Question: " + (j + 1);
                
                if(question.TryGetComponent<Text>(out Text questText))
                {
                    questText.text = RoundList[i].Question[j];
                }
            }
        }
    }
}
