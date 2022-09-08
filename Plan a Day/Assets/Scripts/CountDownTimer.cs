using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    public static float startSecond = 10f;
    public static float startMinute = 0f;

    [SerializeField] Text countDownSeconds;
    [SerializeField] GameObject Expired;

    private float currentSecond = 0f;
    private float currentMinute = 0f;

    void Start()
    {
        countDownSeconds.gameObject.SetActive(true);
        Expired.SetActive(false);

        currentMinute = startMinute;
        currentSecond = startSecond;
    }

    void Update()
    {
        if (currentMinute <= 0f && currentSecond <= 0f)
        {
            Expired.SetActive(true);
            Time.timeScale = 0;
            countDownSeconds.gameObject.SetActive(false);
        }
        else if (currentSecond <= 0f)
        {
            currentSecond = 59f;
            currentMinute -= 1;
        }

        currentSecond -= 1 * Time.deltaTime;
        countDownSeconds.text = currentMinute.ToString("00") + ": " + currentSecond.ToString("00");
    }
}
