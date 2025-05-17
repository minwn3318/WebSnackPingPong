using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Collections;
using System;
using UnityEditor.PackageManager.Requests;
using TMPro;
using System.Collections.Generic;

[Serializable]
public class PlayerStageDTO
{
    public int stage;
}

[Serializable]
public class PlayerScoreDTO
{
    public int score;
}

[Serializable]
public class PlayerTotalDTO
{
    public int stage;
    public int score;
}

[Serializable]
public class TopPlayerRecordsDTO
{
    public string game_id;
    public int stage;
    public int score;
}

[Serializable]
public class TopPlayerRecordsDTOList
{
    public TopPlayerRecordsDTO[] list;
}

public class GetRankingAPIFront : MonoBehaviour
{
    private const string mainURL = "http://localhost:8080/shooting-miner/play-records/serach";
    private string maxStage = "/max-stage";
    private string maxScore = "/max-score";
    private string maxTotal = "/max-total";
    private string topUsers = "/top-users";

    [Header("UserId")]
    [SerializeField]
    private string gameID = "gamer01";
    public TMP_Text userID;

    [Header("Stage")]
    [SerializeField]
    private TMP_Text TMPstage;

    [Header("Score")]
    [SerializeField]
    private TMP_Text TMPscore;

    [Header("maxStage")]
    [SerializeField]
    private TMP_Text TMPmaxStage;

    [Header("maxScore")]
    [SerializeField]
    private TMP_Text TMPmaxScoreTX;

    [Header("Rank 1")]
    [SerializeField]
    private TMP_Text TMPone;

    [Header("Rank 2")]
    [SerializeField]
    private TMP_Text TMPsecond;

    [Header("Rank 3")]
    [SerializeField]
    private TMP_Text TMPthird;

    private void Awake()
    {
        SetGameID("gamer01");
        userID.text = gameID;
    }
    void Start()
    {
        StartCoroutine(GetMaxStage(maxStage));
        StartCoroutine(GetMaxScore(maxScore));
        StartCoroutine(GetMaxTotal(maxTotal));
        StartCoroutine(GetTopUsers(topUsers));
    }

    public void SetGameID(string id)
    {
        gameID = id;
    }

    public string GetGameID()
    {
        return gameID;
    }

    IEnumerator GetMaxStage(string stageURL)
    {
        string url = $"{mainURL}{stageURL}?userId={UnityWebRequest.EscapeURL(GetGameID())}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"GET Error: {request.error}");
            }
            else 
            {
                string jsonResponse = request.downloadHandler.text;
                PlayerStageDTO resp = JsonUtility.FromJson<PlayerStageDTO>(jsonResponse);
                Debug.Log(resp.stage);
                TMPstage.text = resp.stage.ToString();

            }
        }
    }
    IEnumerator GetMaxScore(string scoreURL)
    {
        string url = $"{mainURL}{scoreURL}?userId={UnityWebRequest.EscapeURL(GetGameID())}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"GET Error: {request.error}");
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                PlayerScoreDTO resp = JsonUtility.FromJson<PlayerScoreDTO>(jsonResponse);
                Debug.Log(resp.score);
                TMPscore.text = resp.score.ToString();
            }
        }
    }
    IEnumerator GetMaxTotal(string totalURL)
    {
        string url = $"{mainURL}{totalURL}?userId={UnityWebRequest.EscapeURL(GetGameID())}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"GET Error: {request.error}");
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                PlayerTotalDTO resp = JsonUtility.FromJson<PlayerTotalDTO>(jsonResponse);
                TMPmaxStage.text = resp.stage.ToString();
                TMPmaxScoreTX.text = resp.score.ToString();
            }
        }
    }

    IEnumerator GetTopUsers(string topURL)
    {
        string url = $"{mainURL}{topURL}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"GET Error: {request.error}");
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                string wrappedJson = "{\"list\":" + jsonResponse + "}";
                TopPlayerRecordsDTOList resp = JsonUtility.FromJson<TopPlayerRecordsDTOList>(wrappedJson);
                TopPlayerRecordsDTO[] ranking = resp.list;

                List<TopPlayerRecordsDTO> scoreList = new List<TopPlayerRecordsDTO>(ranking);
                TMPone.text = "Rank 1 : " + scoreList[0].game_id.ToString() + " : " + scoreList[0].stage.ToString() + " : " + scoreList[0].score.ToString();
                TMPsecond.text = "Rank 2 : " + scoreList[1].game_id.ToString() + " : " + scoreList[1].stage.ToString() + " : " + scoreList[1].score.ToString();
                TMPthird.text = "Rank 3 : " + scoreList[2].game_id.ToString() + " : " + scoreList[2].stage.ToString() + " : " + scoreList[2].score.ToString();

            }
        }
    }
}
