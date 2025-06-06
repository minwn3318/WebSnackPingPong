using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class UserIdDTO
{
    public string game_id;
    public string message;
}
public class PostLoginJoinAPIFront : MonoBehaviour
{
    private const string postURL = "http://113.198.229.158:1435/shooting-miner/userids";
    private string joinURL = "/join";
    private string checkURL = "/joincheck";
    private string loginURL = "/login";

    public void SendJoin(string gameUser)
    {
        StartCoroutine(UserJoin(gameUser));
    }
    public void SendJoinCheck(string gameUser)
    {
        StartCoroutine(UserJoinCheck(gameUser));
    }
    public void SendLogin(string gameUser)
    {
        StartCoroutine(UserLogin(gameUser));
    }

    IEnumerator UserJoin(string userid)
    {
        string url = $"{postURL}{joinURL}";

        // 보낼 데이터를 JSON 문자열로 생성
        var payload = new UserIdDTO
        {
            game_id = userid,
        };
        string jsonData = JsonUtility.ToJson(payload);

        // UnityWebRequest 생성 (POST + JSON)
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // 요청 전송
            yield return request.SendWebRequest();

            // 에러 체크
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"PostLoginJoinAPIFront - 57 POST Error: {request.error}");
            }
            else
            {
                string responseText = request.downloadHandler.text;
                Debug.Log($"PostLoginJoinAPIFront - 62 POST Success: {responseText}");
                UserIdDTO resp = JsonUtility.FromJson<UserIdDTO>(responseText);
                Debug.Log($"PostLoginJoinAPIFront 78 message : {resp.message}");
            }
        }
    }

    IEnumerator UserJoinCheck(string userid)
    {
        string url = $"{postURL}{checkURL}";

        // 보낼 데이터를 JSON 문자열로 생성
        var payload = new UserIdDTO
        {
            game_id = userid,
        };
        string jsonData = JsonUtility.ToJson(payload);

        // UnityWebRequest 생성 (POST + JSON)
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // 요청 전송
            yield return request.SendWebRequest();

            // 에러 체크
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"PostLoginJoinAPIFront - 93 POST Error: {request.error}");
            }
            else
            {
                string responseText = request.downloadHandler.text;
                Debug.Log($"POST Success: {responseText}");
                UserIdDTO resp = JsonUtility.FromJson<UserIdDTO>(responseText);
                Debug.Log($"PostLoginJoinAPIFront 116 message :{resp.message}");
            }
        }
    }
    IEnumerator UserLogin(string userid)
    {
        string url = $"{postURL}{loginURL}";

        // 보낼 데이터를 JSON 문자열로 생성
        var payload = new UserIdDTO
        {
            game_id = userid,
        };
        string jsonData = JsonUtility.ToJson(payload);

        // UnityWebRequest 생성 (POST + JSON)
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // 요청 전송
            yield return request.SendWebRequest();

            // 에러 체크
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"PostLoginJoinAPIFront - 146 POST Error: {request.error}");
            }
            else
            {
                string responseText = request.downloadHandler.text;
                Debug.Log($"POST Success: {responseText}");
                UserIdDTO resp = JsonUtility.FromJson<UserIdDTO>(responseText);
                Debug.Log($"PostLoginJoinAPIFront - 153 message : {resp.message}");

                string setCookie = request.GetResponseHeader("Set-Cookie");
                Debug.Log($"PostLoginJoinAPIFront - 156 cookie: {setCookie}");

                if (!string.IsNullOrEmpty(setCookie))
                {
                    string jsession = ParseAndSaveCookie(setCookie, "JSESSIONID");
                    if (!string.IsNullOrEmpty(jsession))
                    {
                        PlayerPrefs.SetString("JSESSIONID", jsession);
                        PlayerPrefs.SetString("nickname", userid);
                        PlayerPrefs.Save();
                        Debug.Log($" PostLoginJoinAPIFront - 166 Saved JSESSIONID = {jsession}");
                        Debug.Log("PostLoginJoinAPIFront - 167 Player JSESSIONID" + PlayerPrefs.GetString("JSESSIONID"));
                        Debug.Log("PostLoginJoinAPIFront - 168 Player nickname" + PlayerPrefs.GetString("nickname"));
                    }
                }
            }
        }
    }

    public string ParseAndSaveCookie(string setCookieHeader, string cookieName)
    {
        string[] parts = setCookieHeader.Split(';');
        foreach (var part in parts)
        {
            var kv = part.Trim().Split('=');
            if (kv.Length == 2 && kv[0] == cookieName)
            {
                return kv[1];
            }
        }
        return null;
    }
}
