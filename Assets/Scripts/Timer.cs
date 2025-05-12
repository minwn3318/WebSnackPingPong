using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Image timerbar;
    public float maxTime = 10f;
    float timeLeft;
    public GameObject GameOverText;

    private void Start()
    {
        GameOverText.SetActive(false);
        timerbar = GetComponent<Image>();
        timeLeft = maxTime;
    }

    private void Update()
    {
        // 게임 시작 후 시간이 흐르기 시작했을 때만 타이머 작동
        if (!GameManager.Instance.TimerStarted) return;

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerbar.fillAmount = timeLeft / maxTime;
        }
        else
        {
            if (!GameManager.Instance.IsGameOver())
            {
                GameManager.Instance.GameOver();
                Time.timeScale = 0;
            }
        }
    }
}
