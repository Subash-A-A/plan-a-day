using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] float startSecond = 10f;
    [SerializeField] float startMinute = 0f;
    [SerializeField] float currentMinute = 0f;

    [SerializeField] Text countDownSeconds;
    [SerializeField] GameObject Expired;

    private float currentSecond = 0f;

    void Start()
    {
        Expired.SetActive(false);
        currentMinute = startMinute;
        currentSecond = startSecond;
    }

    void Update()
    {
        if (currentMinute <= 0f && currentSecond <= 0f)
        {
            Debug.Log("Timer Expired!");
            Expired.SetActive(true);

            Time.timeScale = 0;
            countDownSeconds.gameObject.SetActive(false);
        }
        else if (currentSecond <= 0f)
        {
            currentSecond = 60f;
            currentMinute -= 1 * Time.deltaTime;
        }

        currentSecond -= 1 * Time.deltaTime;
        countDownSeconds.text = currentMinute.ToString("00") + ": " + currentSecond.ToString("00");
    }
}
