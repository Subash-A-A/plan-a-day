using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AdminManager : MonoBehaviour
{
    [SerializeField] Transform Users;
    [SerializeField] Color SuccessColor;
    [SerializeField] Color FailedColor;

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

    public IEnumerator GetConformationPopup(Image userInstanceBg)
    {
        Color original = userInstanceBg.color;
        userInstanceBg.color = SuccessColor;
        yield return new WaitForSeconds(2);
        userInstanceBg.color = original;
    }
}
