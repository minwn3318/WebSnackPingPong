using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;
using System;
using UnityEngine.Networking;
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
public class GameSelectManager : MonoBehaviour
{
    [Header("users")]
    public TextMeshProUGUI nicknameText;
    [Header("TStage")]
    public TextMeshProUGUI tStage;
    [Header("TScore")]
    public TextMeshProUGUI tScore;
    [Header("HStage")]
    public TextMeshProUGUI hStage;
    [Header("HScore")]
    public TextMeshProUGUI hScore;

    [Header("top1")]
    public TextMeshProUGUI nickname1;
    [Header("TStag1")]
    public TextMeshProUGUI tStage1;
    [Header("TScor1")]
    public TextMeshProUGUI tScore1;

    [Header("top2")]
    public TextMeshProUGUI nickname2;
    [Header("TStag2")]
    public TextMeshProUGUI tStage2;
    [Header("TScor2")]
    public TextMeshProUGUI tScore2;

    [Header("top3")]
    public TextMeshProUGUI nickname3;
    [Header("TStag3")]
    public TextMeshProUGUI tStage3;
    [Header("TScor3")]
    public TextMeshProUGUI tScore3;

    public Button singlePlayButton;
    public Button backButton;

    [Header("Message")]
    [SerializeField] private string message;
    [Header("num")]
    [SerializeField] private int num;

    void Start()
    {
        Time.timeScale = 1f;

        singlePlayButton.onClick.AddListener(() => {
            SceneManager.LoadScene("MainTitle");
        });

        backButton.onClick.AddListener(() => {
            AudioManager.Instance.PlaybuttonClickClip();
            StartCoroutine(UserLogout());
            StartCoroutine(Staying());
        });
    }

    private void OnEnable()
    {
        Debug.Log("Scene Start : "+ PlayerPrefs.GetString("nickname"));
        nicknameText.text = PlayerPrefs.GetString("nickname");
        StartCoroutine(GetMaxStage());
        StartCoroutine(GetMaxScore());
        StartCoroutine(GetMaxTotal());
        StartCoroutine(GetTopUsers());
    }

    public void BackToMain()
    {
        AudioManager.Instance.PlaybuttonClickClip();
        StartCoroutine(UserLogout());
        StartCoroutine(Staying());
    }

    IEnumerator Staying()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("TitleScene");
    }

    IEnumerator UserLogout()
    {
        string url = "http://113.198.229.158:1435/shooting-miner/userids/logout";
        // 빈 바디의 POST 요청 생성
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(new byte[0]);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");

        // 저장된 JSESSIONID를 헤더에 포함
        string jsession = PlayerPrefs.GetString("JSESSIONID");
        if (!string.IsNullOrEmpty(jsession))
        {
            request.SetRequestHeader("Cookie", $"JSESSIONID={jsession}");
        }

        yield return request.SendWebRequest();
        string setCookie = request.GetResponseHeader("Set-Cookie");
        string responseText = request.downloadHandler.text;
        UserIdDTO resp = JsonUtility.FromJson<UserIdDTO>(responseText);

        try
        {
            PlayerPrefs.DeleteAll();
            message = resp.message;
        }
        catch(Exception ex)
        {
            Debug.LogError($"GameSelectManager 48 - ClearCookie Error: {request.error}");
            
            message = resp.message + " : " + ex.ToString();
        }
        finally
        {
            // 꼭 Dispose() 해 줘야 핸들 누수 방지
            request.Dispose();
        }
    }

    IEnumerator GetMaxStage()
    {
        string url = "http://113.198.229.158:1435/shooting-miner/play-records/serach/max-stage";
        PlayerStageDTO resp = new PlayerStageDTO();
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            string jsession = PlayerPrefs.GetString("JSESSIONID");
            request.SetRequestHeader("Cookie", $"JSESSIONID={jsession}");
            request.SetRequestHeader("Accept", "application/json");

            yield return request.SendWebRequest();

            try
            {
                string jsonResponse = request.downloadHandler.text;
                resp = JsonUtility.FromJson<PlayerStageDTO>(jsonResponse);
                tStage.text = resp.stage.ToString();
                num = resp.stage;
            }
            catch (Exception ex)
            {
                num = ex.GetHashCode();
            }
            finally
            {
                // 꼭 Dispose() 해 줘야 핸들 누수 방지
                request.Dispose();
            }
        }
    }
    IEnumerator GetMaxScore()
    {
        string url = "http://113.198.229.158:1435/shooting-miner/play-records/serach/max-score";
        PlayerScoreDTO resp = new PlayerScoreDTO();
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            string jsession = PlayerPrefs.GetString("JSESSIONID");
            request.SetRequestHeader("Cookie", $"JSESSIONID={jsession}");
            request.SetRequestHeader("Accept", "application/json");

            yield return request.SendWebRequest();
            try
            {
                string jsonResponse = request.downloadHandler.text;
                resp = JsonUtility.FromJson<PlayerScoreDTO>(jsonResponse);
                tScore.text = resp.score.ToString();
                num = resp.score;
            }
            catch (Exception ex)
            {
                num = ex.GetHashCode();
            }
            finally
            {
                // 꼭 Dispose() 해 줘야 핸들 누수 방지
                request.Dispose();
            }
        }
    }
    IEnumerator GetMaxTotal()
    {
        string url = "http://113.198.229.158:1435/shooting-miner/play-records/serach/max-total";
        PlayerTotalDTO resp = new PlayerTotalDTO();
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            string jsession = PlayerPrefs.GetString("JSESSIONID");
            request.SetRequestHeader("Cookie", $"JSESSIONID={jsession}");
            request.SetRequestHeader("Accept", "application/json");

            yield return request.SendWebRequest();

            try
            {
                string jsonResponse = request.downloadHandler.text;
                resp = JsonUtility.FromJson<PlayerTotalDTO>(jsonResponse);
                hStage.text = resp.stage.ToString();
                hScore.text = resp.score.ToString();
                num = resp.score + resp.stage;
            }
            catch (Exception ex)
            {
                num = ex.GetHashCode();
            }
            finally
            {
                // 꼭 Dispose() 해 줘야 핸들 누수 방지
                request.Dispose();
            }
        }
    }

    IEnumerator GetTopUsers()
    {
        string url = "http://113.198.229.158:1435/shooting-miner/play-records/serach/top-users";
        TopPlayerRecordsDTOList resp = new TopPlayerRecordsDTOList();
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            yield return request.SendWebRequest();

            try
            {
                string jsonResponse = request.downloadHandler.text;
                string wrappedJson = "{\"list\":" + jsonResponse + "}";
                resp = JsonUtility.FromJson<TopPlayerRecordsDTOList>(wrappedJson);
                TopPlayerRecordsDTO[] ranking = resp.list;

                List<TopPlayerRecordsDTO> scoreList = new List<TopPlayerRecordsDTO>(ranking);
                nickname1.text = scoreList[0].game_id;
                tStage1.text = scoreList[0].stage.ToString();
                tScore1.text = scoreList[0].score.ToString();

                nickname2.text = scoreList[1].game_id;
                tStage2.text = scoreList[1].stage.ToString();
                tScore2.text = scoreList[1].score.ToString();

                nickname3.text = scoreList[2].game_id;
                tStage3.text = scoreList[2].stage.ToString();
                tScore3.text = scoreList[2].score.ToString();

                num = scoreList[0].stage + scoreList[0].score;
            }
            catch (Exception ex)
            {
                num = ex.GetHashCode();
            }
            finally
            {
                // 꼭 Dispose() 해 줘야 핸들 누수 방지
                request.Dispose();
            }
        }
    }
}
