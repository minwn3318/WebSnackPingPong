using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour // 게임 스테이지를 총괄하는 매니저 클래스다
{
    [SerializeField] private Player playerScript; // 플레이어 스크립트
    [SerializeField] private int score; // 총 점수
    [SerializeField] private int stage; // 현재 스테이지

    void Awake()
    {
        playerScript = FindAnyObjectByType<Player>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바를 누르면 게임이 시작된다
        {
            Debug.Log("GameStart");
            playerScript.SpawnPlayer();
            StartSetting();
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) // ESC를 누르면 게임오버상태로 만든다 다시 스페이스바를 누르면 시작된다
        {
            Debug.Log("GameOver");
            playerScript.DestoryPlayer();
        }
    }

    public int Stage
    {
        get { return stage; } 
    }    

    public void plusStage() // 스테이지 상승
    {
        stage++;
    }

    public void StartSetting() // 스테이지와 점수 초기화
    {
        score = 0;
        stage = 1;
    }

}
