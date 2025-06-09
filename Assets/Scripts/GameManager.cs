using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Text;
using UnityEngine.Networking;

[Serializable]
public class PlayRecordsDTO
{
    public string game_id;
    public int stage;
    public int score;
    public string play_datetime;
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float lastDiamondXPos;

    [Header("���� �ý���")]
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

    private const string postURL = "http://113.198.229.158:1435/shooting-miner/play-records/save";

    private int score = 0;
    private int stageCount = 0;
    private bool gameStarted = false;
    private bool gameOver = false;
    private int ballCount = 0;
    private int ballReturnCount = 0;
    private bool diamondHit = false; // ���̾Ƹ�带 �¾Ҵ°�?

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
            AudioManager.Instance.PlaygameOverClip();
            countdownTMP.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count--;
        }
        AudioManager.Instance.PlayloadingSceneClip();
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
        AudioManager.Instance.PlaypointUpClip();
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
                // ��� ���� ȸ���ư�, ���̾Ƹ�嵵 �Ծ����� �������� ���� ����
                diamondHit = false; // �ʱ�ȭ
                StartCoroutine(WaitAndHandleStageChange());
            }
            else
            {
                playerScript.EnableInput(); // ���̾Ƹ�� �ȸ��� ��쿣 �Է� ����
            }
        }
    }


    public void OnDiamondHit()
    {
        playerScript.DisableInput();
        diamondHit = true;
    }
    

    private IEnumerator WaitAndHandleStageChange()
    {
        // ���� ���� ȭ�鿡�� ����� ������ ���
        while (true)
        {
            Ball[] balls = GameObject.FindObjectsOfType<Ball>(true); // true: ��Ȱ�� ����

            bool anyVisible = false;
            foreach (Ball ball in balls)
            {
                if (ball.gameObject.activeSelf) // ���� Ȱ��ȭ�� ���� �ִٸ� ���
                {
                    anyVisible = true;
                    break;
                }
            }

            if (!anyVisible)
                break;

            yield return null; // ���� �����ӱ��� ���
        }

        yield return new WaitForSeconds(0.1f); // ����ȭ ����

        GameObject newStage = StageManager.Instance.GenerateStage();

        playerScript.MoveToNewStage();
        playerScript.EnableInput();
    }

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
        StartCoroutine(CreatePlayRecord(stageCount, score));
        StartCoroutine(LoadUserInfoScene());
    }

    private IEnumerator ForceGameOver()
    {
        Debug.Log("���� �ð��� ����Ǿ� ���� GameOver ����");

        // ��� Ball ���߱�
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
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene("UserInfoScene");
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    IEnumerator CreatePlayRecord(int stage, int score)
    {
        // ���� �����͸� JSON ���ڿ��� ����
        var payload = new PlayRecordsDTO
        {
            game_id = "",
            stage = stage,
            score = score,
            play_datetime = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss")
        };
        string jsonData = JsonUtility.ToJson(payload);

        // UnityWebRequest ���� (POST + JSON)
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        using (UnityWebRequest request = new UnityWebRequest(postURL, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            string jsession = PlayerPrefs.GetString("JSESSIONID");
            request.SetRequestHeader("Cookie", $"JSESSIONID={jsession}");

            // ��û ����
            yield return request.SendWebRequest();

            // ���� üũ
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"PostRecordAPIFront - 54 POST Error: {request.error}");
            }
            else
            {
                string responseText = request.downloadHandler.text;
                PlayRecordsDTO resp = JsonUtility.FromJson<PlayRecordsDTO>(responseText);
            }
        }
    }
}
