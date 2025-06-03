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
    private const string postURL = "http://localhost:8080/shooting-miner/userids";
    private string joinURL = "/join";
    private string loginURL = "/login";
    private string logoutURL = "/logout";

    public void SendJoin(string gameUser)
    {
        StartCoroutine(UserJoin(gameUser));
    }

    public void SendLogin(string gameUser)
    {
        StartCoroutine(UserLogin(gameUser));
    }

    public void SendLogout()
    {
        StartCoroutine(UserLogout());
    }


    IEnumerator UserJoin(string userid)
    {
        string url = $"{postURL}{joinURL}";

        // ���� �����͸� JSON ���ڿ��� ����
        var payload = new UserIdDTO
        {
            game_id = userid,
        };
        string jsonData = JsonUtility.ToJson(payload);

        // UnityWebRequest ���� (POST + JSON)
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
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

    IEnumerator UserLogin(string userid)
    {
        string url = $"{postURL}{loginURL}";

        // ���� �����͸� JSON ���ڿ��� ����
        var payload = new UserIdDTO
        {
            game_id = userid,
        };
        string jsonData = JsonUtility.ToJson(payload);

        // UnityWebRequest ���� (POST + JSON)
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
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
                string setCookie = request.GetResponseHeader("Set-Cookie");
                if (!string.IsNullOrEmpty(setCookie))
                {
                    string jsession = CookieSession.Instance.ParseAndSaveCookie(setCookie, "JSESSIONID");
                    if (!string.IsNullOrEmpty(jsession))
                    {
                        PlayerPrefs.SetString("JSESSIONID", jsession);
                        PlayerPrefs.Save();
                        Debug.Log($"Saved JSESSIONID = {jsession}");
                    }
                }
            }
        }
    }
    public IEnumerator UserLogout()
    {
        string url = $"{postURL}{logoutURL}";

        // �� �ٵ��� POST ��û ����
        UnityWebRequest uwr = new UnityWebRequest(url, "POST");
        uwr.uploadHandler = new UploadHandlerRaw(new byte[0]);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        // ����� JSESSIONID�� ����� ����
        string jsession = CookieSession.Instance.GetCookie();
        if (!string.IsNullOrEmpty(jsession))
        {
            uwr.SetRequestHeader("Cookie", $"JSESSIONID={jsession}");
        }

        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.Success)
        {
            // ������ Set-Cookie ����� ���� ��Ű�� �����ָ�, ���ÿ����� ����
            string setCookie = uwr.GetResponseHeader("Set-Cookie");
            if (!string.IsNullOrEmpty(setCookie) && setCookie.Contains("JSESSIONID"))
            {
                CookieSession.Instance.DeleteCookie();
                Debug.Log("Cleared JSESSIONID from PlayerPrefs");
            }
            else
            {
                Debug.LogWarning("No JSESSIONID expiration header received.");
            }
        }
        else
        {
            Debug.LogError($"ClearCookie Error: {uwr.error}");
        }
    }
}
