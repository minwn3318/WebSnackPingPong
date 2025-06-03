using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class PlayRecordsDTO
{
    public string game_id;
    public int stage;
    public int score;
    public string play_datetime;
}
public class PostRecordAPIFront : MonoBehaviour
{
    private const string postURL = "http://localhost:8080/shooting-miner/play-records/save";

    public void SendRecord(int gameScore, int gameStage)
    {
        StartCoroutine(CreatePlayRecord(gameStage, gameScore));
    }

    IEnumerator CreatePlayRecord(int stage, int score)
    {
        // 보낼 데이터를 JSON 문자열로 생성
        var payload = new PlayRecordsDTO
        {
            game_id = "",
            stage = stage,
            score = score,
            play_datetime = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss")
        };
        string jsonData = JsonUtility.ToJson(payload);

        // UnityWebRequest 생성 (POST + JSON)
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        using (UnityWebRequest request = new UnityWebRequest(postURL, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            string jsession = CookieSession.Instance.GetCookie();
            request.SetRequestHeader("Cookie", $"JSESSIONID={jsession}");
            // 인증 헤더가 필요하면 추가
            // request.SetRequestHeader("Authorization", "Bearer YOUR_TOKEN_HERE");

            // 요청 전송
            yield return request.SendWebRequest();

            // 에러 체크
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"POST Error: {request.error}");
            }
            else
            {
                string responseText = request.downloadHandler.text;
                Debug.Log($"POST Success: {responseText}");
                // 응답 JSON 처리
                // var result = JsonUtility.FromJson<YourResultType>(responseText);
            }
        }
    }
}
