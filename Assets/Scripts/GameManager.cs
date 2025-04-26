using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour // 게임 스테이지를 총괄하는 매니저 클래스
{
    [Header("게임 시스템")]
    [SerializeField] private Player playerScript; // 플레이어 스크립트
    [SerializeField] private int score; // 총 점수
    [SerializeField] private int stage; // 현재 스테이지

    public static GameManager Instance; // 싱글톤 패턴

    [Header("카운트다운 UI")]
    [SerializeField] private TMP_Text countdownTMP; // TextMeshPro 텍스트
    [SerializeField] private GameObject gameElementsToEnable; // 게임 시작 시 활성화할 오브젝트 묶음
    [SerializeField] private TMP_Text scoreTMP; // ScoreText 연결
    [SerializeField] private GameObject resultPanel; // 결과창 Panel
    [SerializeField] private TMP_Text gameOverTMP; // 게임 오버 문구용 텍스트
    [SerializeField] private TMP_Text finalScoreTMP; // 결과창 최종 점수 Text

    private bool gameStarted = false;
    private bool gameOver = false;
    public bool TimerStarted { get; private set; } = false;

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
        Debug.Log($"현재 점수: {score}");
    }

    private void UpdateScoreUI()
    {
        if (scoreTMP != null)
            scoreTMP.text = "Score: " + score.ToString();
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        playerScript = FindAnyObjectByType<Player>();
    }

    void Start()
    {
        StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine()
    {
        // 게임 시작 전 UI/오브젝트 숨기기
        gameElementsToEnable.SetActive(false);
        countdownTMP.gameObject.SetActive(true);

        int count = 3;
        while (count > 0)
        {
            countdownTMP.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count--;
        }

        countdownTMP.text = "Start!";
        yield return new WaitForSeconds(1f);

        countdownTMP.gameObject.SetActive(false);
        gameElementsToEnable.SetActive(true);

        StartGame();
    }

    private void StartGame()
    {
        if (gameStarted) return;

        Debug.Log("GameStart");
        StartSetting();
        playerScript.SpawnPlayer(); // 카운트다운 후 자동 등장
        gameStarted = true;
        TimerStarted = true;
    }

    void Update()
    {
        // ESC 누르면 게임 리셋 테스트용
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("GameOver");
            playerScript.DestoryPlayer();
            gameStarted = false;
            StartCoroutine(CountdownRoutine()); // 다시 카운트다운 시작
        }
    }

    public int Stage => stage;
    public int Score => score;

    public void plusStage() // 스테이지 상승
    {
        stage++;
    }

    public void StartSetting() // 점수, 스테이지 초기화
    {
        score = 0;
        stage = 1;
        UpdateScoreUI();
    }

    // GameManager.cs 안에 추가
    public bool IsGameOver()
    {
        return gameOver;
    }

    public void GameOver()
    {
        if (gameOver) return; // 이미 게임오버 상태면 무시

        Debug.Log("Game Over by Timer");
        gameOver = true;

        playerScript.DestoryPlayer();
        gameElementsToEnable.SetActive(false);

        if (gameOverTMP != null)
        {
            gameOverTMP.gameObject.SetActive(true);
            gameOverTMP.text = "Game Over!";
        }


        resultPanel.SetActive(true);
        finalScoreTMP.text = "Score: " + score.ToString();

        Time.timeScale = 0; // 게임 정지
    }

}