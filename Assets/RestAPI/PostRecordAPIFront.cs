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
            string jsession = CookieSession.Instance.GetCookie();
            request.SetRequestHeader("Cookie", $"JSESSIONID={jsession}");
            // ���� ����� �ʿ��ϸ� �߰�
            // request.SetRequestHeader("Authorization", "Bearer YOUR_TOKEN_HERE");

            // ��û ����
            yield return request.SendWebRequest();

            // ���� üũ
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"POST Error: {request.error}");
            }
            else
            {
                string responseText = request.downloadHandler.text;
                Debug.Log($"POST Success: {responseText}");
                // ���� JSON ó��
                // var result = JsonUtility.FromJson<YourResultType>(responseText);
            }
        }
    }
}
