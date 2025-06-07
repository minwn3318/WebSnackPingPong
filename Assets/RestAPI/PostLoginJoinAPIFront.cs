using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.PackageManager.Requests;
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

    [Header("Message")]
    [SerializeField] private string message;

    public string SendJoin(string gameUser)
    {
        StartCoroutine(UserJoin(gameUser));
        return message;
    }
    public string SendJoinCheck(string gameUser)
    {
        StartCoroutine(UserJoinCheck(gameUser));
        return message;
    }
    public string SendLogin(string gameUser)
    {
        StartCoroutine(UserLogin(gameUser));
        return message;
    }

    IEnumerator UserJoin(string userid)
    {
        string url = $"{postURL}{joinURL}";
        // 1) JSON 페이로드 준비
        var payload = new UserIdDTO { game_id = userid };
        string jsonData = JsonUtility.ToJson(payload);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        // 2) UnityWebRequest 생성 & 헤더/타임아웃 설정
        UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST)
        {
            uploadHandler = new UploadHandlerRaw(bodyRaw),
            downloadHandler = new DownloadHandlerBuffer(),
            timeout = 10
        };
        request.SetRequestHeader("Content-Type", "application/json");

        // 3) 네트워크 요청 — yield은 오직 이 부분에만!
        yield return request.SendWebRequest();

        // 4) 결과 처리 및 예외 안전망
        try
        {
            string responseText = request.downloadHandler.text;
            Debug.Log($"UserJoin Success: {responseText}");
            payload = JsonUtility.FromJson<UserIdDTO>(responseText);
            Debug.Log($"UserJoin message: {payload.message}");
            message = payload.message;
        }
        catch (Exception ex)
        {
            // JSON 파싱이나 내부 로직 에러까지 안전하게 잡아냄
            Debug.LogError($"UserJoin Exception: {ex.GetType().Name} – {ex.Message}");
            message = payload.message;
        }
        finally
        {
            // 꼭 Dispose() 해 줘야 핸들 누수 방지
            request.Dispose();
        }
    }

    IEnumerator UserJoinCheck(string userid)
    {
        string url = $"{postURL}{checkURL}";

        var payload = new UserIdDTO { game_id = userid };
        string jsonData = JsonUtility.ToJson(payload);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        // 2) UnityWebRequest 생성 & 헤더/타임아웃 설정
        UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST)
        {
            uploadHandler = new UploadHandlerRaw(bodyRaw),
            downloadHandler = new DownloadHandlerBuffer(),
            timeout = 10
        };
        request.SetRequestHeader("Content-Type", "application/json");

        // 3) 네트워크 요청 — yield은 오직 이 부분에만!
        yield return request.SendWebRequest();

        // 4) 결과 처리 및 예외 안전망
        try
        {
            string responseText = request.downloadHandler.text;
            Debug.Log($"UserJoin Success: {responseText}");
            payload = JsonUtility.FromJson<UserIdDTO>(responseText);
            Debug.Log($"UserJoin message: {payload.message}");
            message = payload.message;
        }
        catch (Exception ex)
        {
            // JSON 파싱이나 내부 로직 에러까지 안전하게 잡아냄
            Debug.LogError($"UserJoin Exception: {ex.GetType().Name} – {ex.Message}");
            message = payload.message;
        }
        finally
        {
            // 꼭 Dispose() 해 줘야 핸들 누수 방지
            request.Dispose();
        }
    }

    IEnumerator UserLogin(string userid)
    {
        string url = $"{postURL}{loginURL}";

        var payload = new UserIdDTO { game_id = userid };
        string jsonData = JsonUtility.ToJson(payload);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        // 2) UnityWebRequest 생성 & 헤더/타임아웃 설정
        UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST)
        {
            uploadHandler = new UploadHandlerRaw(bodyRaw),
            downloadHandler = new DownloadHandlerBuffer(),
            timeout = 10
        };
        request.SetRequestHeader("Content-Type", "application/json");

        // 3) 네트워크 요청 — yield은 오직 이 부분에만!
        yield return request.SendWebRequest();

        // 에러 체
        try
        {
            string responseText = request.downloadHandler.text;
            Debug.Log($"POST Success: {responseText}");
            payload = JsonUtility.FromJson<UserIdDTO>(responseText);
            Debug.Log($"PostLoginJoinAPIFront - 153 message : {payload.message}");

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
            message = payload.message;
        }
        catch (Exception ex)
        {
            // JSON 파싱이나 내부 로직 에러까지 안전하게 잡아냄
            Debug.LogError($"UserJoin Exception: {ex.GetType().Name} – {ex.Message}");
            message = payload.message;
        }
        finally
        {
            // 꼭 Dispose() 해 줘야 핸들 누수 방지
            request.Dispose();
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
