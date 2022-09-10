using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AdminManager : MonoBehaviour
{
    [SerializeField] Transform Users;
    [SerializeField] GameObject UpdateConformationPopup;

    private void Start()
    {
        UpdateConformationPopup.SetActive(false);
    }
    public void Filter(InputField key)
    {   
        foreach(Transform user in Users)
        {
            if (user.name.ToLower().StartsWith(key.text.ToLower()) || user.name.ToLower().EndsWith(key.text.ToLower()))
            {
                user.gameObject.SetActive(true);
            }
            else
            {
                user.gameObject.SetActive(false);
            }
        }
    }

    public void CloseConformationPopUp()
    {
        UpdateConformationPopup.SetActive(false);
    }

    public GameObject GetConformationPopup()
    {
        return UpdateConformationPopup;
    }
}
