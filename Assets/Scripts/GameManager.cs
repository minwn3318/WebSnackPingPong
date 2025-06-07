using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float lastDiamondXPos;

    [Header("게임 시스템")]
    [SerializeField] private Player playerScript;
    [SerializeField] private TMP_Text countdownTMP;
    [SerializeField] private GameObject gameElementsToEnable;
    [SerializeField] private TMP_Text scoreTMP;
    [SerializeField] private TMP_Text stageTMP;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TMP_Text gameOverTMP;
    [SerializeField] private TMP_Text finalScoreTMP;
    [SerializeField] private GameObject countdownPanel;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private TMP_Text resultStageTMP;



    private int score = 0;
    private int stageCount = 0;
    private bool gameStarted = false;
    private bool gameOver = false;
    private int ballCount = 0;
    private int ballReturnCount = 0;
    private bool diamondHit = false; // 다이아몬드를 맞았는가?

    public bool TimerStarted { get; private set; } = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        playerScript = FindObjectOfType<Player>();
    }

    private void Start()
    {
        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        gameElementsToEnable.SetActive(false);
        countdownTMP.gameObject.SetActive(true);
        countdownPanel.SetActive(true);

        int count = 3;
        while (count > 0)
        {
            countdownTMP.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count--;
        }

        countdownTMP.text = "Start!";
        yield return new WaitForSeconds(1f);
        countdownPanel.SetActive(false);
        countdownTMP.gameObject.SetActive(false);
        gameElementsToEnable.SetActive(true);

        StartGame();
    }

    private void StartGame()
    {
        if (gameStarted) return;

        score = 0;
        UpdateScoreUI();
        gameStarted = true;
        gameOver = false;
        TimerStarted = true;

        playerScript.SpawnPlayer();
        StageManager.Instance.GenerateStage(true);
    }

    private void Update()
    {
        if (!gameStarted || gameOver) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameOver();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    public void IncreaseStage()
    {
        stageCount++;
        Debug.Log($"[DEBUG] Stage Increased: {stageCount}");

        if (stageTMP == null)
            Debug.LogError("StageTMP is NULL!");
        else
            stageTMP.text = $"Stage: {stageCount}";
    }




    private void UpdateScoreUI()
    {
        if (scoreTMP != null)
            scoreTMP.text = $"Score: {score}";
        else
            Debug.LogError($"StageTMP is NULL! (GameObject: {this.gameObject.name})");

        if (stageTMP != null)
            stageTMP.text = $"Stage: {stageCount}";
        else
            Debug.LogError($"StageTMP is NULL! (GameObject: {this.gameObject.name})");
    }


    public void SetBallCount(int count)
    {
        ballCount = count;
        ballReturnCount = 0;
    }

    public void OnBallReturned()
    {
        ballReturnCount++;

        if (ballReturnCount >= ballCount && !StageManager.Instance.isStageMoving)
        {
            if (diamondHit)
            {
                // 모든 공이 회수됐고, 다이아몬드도 먹었으면 스테이지 변경 시작
                diamondHit = false; // 초기화
                StartCoroutine(WaitAndHandleStageChange());
            }
            else
            {
                playerScript.EnableInput(); // 다이아몬드 안먹은 경우엔 입력 가능
            }
        }
    }


    public void OnDiamondHit()
    {
        playerScript.DisableInput();
        diamondHit = true;
    }
    
    //private IEnumerator WaitForBallsThenStageChange()
    //{
    //    // 공 회수 대기
    //    while (ballReturnCount < ballCount)
    //    {
    //        yield return null;
    //    }

    //    yield return StartCoroutine(HandleStageChange());
    //}

    private IEnumerator WaitAndHandleStageChange()
    {
        // 공이 전부 화면에서 사라질 때까지 대기
        while (true)
        {
            Ball[] balls = GameObject.FindObjectsOfType<Ball>(true); // true: 비활성 포함

            bool anyVisible = false;
            foreach (Ball ball in balls)
            {
                if (ball.gameObject.activeSelf) // 아직 활성화된 공이 있다면 대기
                {
                    anyVisible = true;
                    break;
                }
            }

            if (!anyVisible)
                break;

            yield return null; // 다음 프레임까지 대기
        }

        yield return new WaitForSeconds(0.1f); // 안정화 유예

        GameObject newStage = StageManager.Instance.GenerateStage();
        //yield return StartCoroutine(StageManager.Instance.MoveStageUp(newStage));

        playerScript.MoveToNewStage();
        playerScript.EnableInput();
    }



    //private IEnumerator HandleStageChange()
    //{
    //    // 기존 스테이지 제거
    //    //StageManager.Instance.ClearCurrentStage();
    //    //yield return new WaitForSeconds(0.2f);

    //    // 새 스테이지 생성 (아래쪽 y = -22 부근에서 시작)
    //    GameObject newStage = StageManager.Instance.GenerateStage();

    //    // 위로 이동 (y=0까지 올라옴)
    //    //yield return StartCoroutine(StageManager.Instance.MoveStageUp(newStage));

    //    // 플레이어는 y=10
    //    playerScript.EnableInput();
    //}



    public void GameOver()
    {
        if (gameOver) return;

        gameOver = true;
        TimerStarted = false;
        playerScript.DestoryPlayer();
        gameElementsToEnable.SetActive(false);

        if (gameOverTMP != null)
        {
            gameOverTMP.gameObject.SetActive(true);
            gameOverTMP.text = "Game Over!";
        }

        resultPanel.SetActive(true);
        finalScoreTMP.text = "Score: " + score;

        if (resultStageTMP != null)
            resultStageTMP.text = "Stage: " + stageCount;

        StartCoroutine(LoadUserInfoScene());
    }

    private IEnumerator ForceGameOver()
    {
        Debug.Log("게임 시간이 종료되어 강제 GameOver 실행");

        // 모든 Ball 멈추기
        Ball[] balls = FindObjectsOfType<Ball>();
        foreach (Ball ball in balls)
        {
            ball.Stop();
        }

        yield return new WaitForSecondsRealtime(1f);
        GameOver();
    }

    private IEnumerator LoadUserInfoScene()
    {
        Debug.Log("LoadUserInfoScene 실행됨");
        yield return new WaitForSecondsRealtime(3f);
        Debug.Log("씬 전환 시도");
        SceneManager.LoadScene("UserInfoScene");
    }

    public bool IsGameOver()
    {
        return gameOver;
    }
}
