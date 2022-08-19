using UnityEngine;
using UnityEngine.UI;

public class RightPanelAppear : MonoBehaviour
{
    [SerializeField] Color colorFullAlpha;
    [SerializeField] Color colorTransparent;
    [SerializeField] GameObject levelManager;

    private Image panel;

    private void Start()
    {
        panel = GetComponentInChildren<Image>();
    }

    private void Update()
    {
        if (levelManager.activeSelf)
        {
            panel.color = Color.Lerp(panel.color, colorFullAlpha, 10 * Time.deltaTime);
        }
        else
        {
            panel.color = Color.Lerp(panel.color, colorTransparent, 10 * Time.deltaTime);
        }
    }
}
