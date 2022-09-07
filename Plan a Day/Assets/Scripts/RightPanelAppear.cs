using UnityEngine;

public class RightPanelAppear : MonoBehaviour
{
    [SerializeField] GameObject leftPanel;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (leftPanel.activeSelf)
        {
            anim.SetBool("RightPanelChange", false);
        }
        else
        {
            anim.SetBool("RightPanelChange", true);
        }
    }
}
