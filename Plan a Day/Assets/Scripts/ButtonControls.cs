using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControls : MonoBehaviour
{
    [SerializeField] GameObject levelManager;
    public void ShowStreetMap()
    {
        levelManager.SetActive(false);
    }

    public void ShowLeftPanel()
    {
        levelManager.SetActive(true);
    }
}
