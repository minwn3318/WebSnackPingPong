using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using TMPro;
using System.Collections.Generic;
using JetBrains.Annotations;

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
    private const string mainURL = "http://113.198.229.158:1435/shooting-miner/play-records/serach";
    private string maxStage = "/max-stage";
    private string maxScore = "/max-score";
    private string maxTotal = "/max-total";
    private string topUsers = "/top-users";

    [Header("Message")]
    [SerializeField] private int message;

    public void ReciveMaxStage()
    {
        StartCoroutine(GetMaxStage(maxStage));
    }

    public void ReciveMaxScore()
    {
        StartCoroutine(GetMaxScore(maxScore));
    }

    public void ReciveMaxTotal()
    {
        StartCoroutine(GetMaxTotal(maxTotal));
    }

    public void ReciveTopUsers()
    {
        StartCoroutine(GetTopUsers(topUsers));
    }

    IEnumerator GetMaxStage(string stageURL)
    {
        string url = $"{mainURL}{stageURL}";
        PlayerStageDTO resp = new PlayerStageDTO();
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            string jsession = PlayerPrefs.GetString("JSESSIONID");
            request.SetRequestHeader("Cookie", $"JSESSIONID={jsession}");

            yield return request.SendWebRequest();

            try 
            {
                string jsonResponse = request.downloadHandler.text;
                resp = JsonUtility.FromJson<PlayerStageDTO>(jsonResponse);
                Debug.Log("max stage : "+resp.stage);
                message = resp.stage;
            }
            catch (Exception ex)
            {
                message = resp.stage;
            }
            finally
            {
                // 꼭 Dispose() 해 줘야 핸들 누수 방지
                request.Dispose();
            }
        }
    }
    IEnumerator GetMaxScore(string scoreURL)
    {
        string url = $"{mainURL}{scoreURL}";
        PlayerScoreDTO resp = new PlayerScoreDTO();
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            string jsession = PlayerPrefs.GetString("JSESSIONID");
            request.SetRequestHeader("Cookie", $"JSESSIONID={jsession}");

            yield return request.SendWebRequest();
            try
            {
                string jsonResponse = request.downloadHandler.text;
                resp = JsonUtility.FromJson<PlayerScoreDTO>(jsonResponse);
                Debug.Log("max score : " + resp.score);
                message = resp.score;
            }
            catch (Exception ex)
            {
                message = resp.score;
            }
            finally
            {
                // 꼭 Dispose() 해 줘야 핸들 누수 방지
                request.Dispose();
            }
        }
    }
    IEnumerator GetMaxTotal(string totalURL)
    {
        string url = $"{mainURL}{totalURL}";
        PlayerTotalDTO resp = new PlayerTotalDTO();
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            string jsession = PlayerPrefs.GetString("JSESSIONID");
            request.SetRequestHeader("Cookie", $"JSESSIONID={jsession}");

            yield return request.SendWebRequest();

            try
            {
                string jsonResponse = request.downloadHandler.text;
                resp = JsonUtility.FromJson<PlayerTotalDTO>(jsonResponse);
                Debug.Log("max total score : " + resp.score);
                Debug.Log("max total stage : " + resp.stage);
                message = resp.score + resp.stage;
            }
            catch (Exception ex)
            {
                message = resp.score +resp.stage;
            }
            finally
            {
                // 꼭 Dispose() 해 줘야 핸들 누수 방지
                request.Dispose();
            }
        }
    }

    IEnumerator GetTopUsers(string topURL)
    {
        string url = $"{mainURL}{topURL}";
        TopPlayerRecordsDTOList resp = new TopPlayerRecordsDTOList();
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            try
            {
                string jsonResponse = request.downloadHandler.text;
                string wrappedJson = "{\"list\":" + jsonResponse + "}";
                resp = JsonUtility.FromJson<TopPlayerRecordsDTOList>(wrappedJson);
                TopPlayerRecordsDTO[] ranking = resp.list;

                List<TopPlayerRecordsDTO> scoreList = new List<TopPlayerRecordsDTO>(ranking);
                Debug.Log("Rank 1 : ID - " + scoreList[0].game_id + " : Stage - " + scoreList[0].stage + " : Score - " + scoreList[0].score);
                Debug.Log("Rank 2 : ID - " + scoreList[1].game_id + " : Stage - " + scoreList[1].stage + " : Score - " + scoreList[1].score);
                Debug.Log("Rank 3 : ID - " + scoreList[2].game_id + " : Stage - " + scoreList[2].stage + " : Score - " + scoreList[2].score);
                message = scoreList[0].stage + scoreList[0].score;
            }
            catch (Exception ex)
            {
                message = resp.list[0].stage + resp.list[0].score; 
            }
            finally
            {
                // 꼭 Dispose() 해 줘야 핸들 누수 방지
                request.Dispose();
            }
        }
    }
}
