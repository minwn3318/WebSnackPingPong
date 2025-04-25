using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour // 시간제한 클래스이다
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
        if (!GameManager.Instance.TimerStarted) return;

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerbar.fillAmount = timeLeft / maxTime;
        } else
        {
            if (!GameManager.Instance.IsGameOver()) // 이미 게임오버 상태가 아닐 때만
            {
                GameManager.Instance.GameOver(); // GameManager에게 게임오버 요청
                Time.timeScale = 0;
            }
        }
    }
}
