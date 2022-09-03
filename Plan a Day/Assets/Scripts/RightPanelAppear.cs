using UnityEngine;
using UnityEngine.UI;

public class RightPanelAppear : MonoBehaviour
{
    [SerializeField] Color colorFullAlpha;
    [SerializeField] Color colorTransparent;
    [SerializeField] GameObject leftPanel;
    [SerializeField] Image rightPanel;


    private void Update()
    {
        if (leftPanel.activeSelf)
        {
            rightPanel.color = Color.Lerp(rightPanel.color, colorFullAlpha, 10 * Time.unscaledDeltaTime);
        }
        else
        {
            rightPanel.color = Color.Lerp(rightPanel.color, colorTransparent, 10 * Time.unscaledDeltaTime);
        }
    }
}
