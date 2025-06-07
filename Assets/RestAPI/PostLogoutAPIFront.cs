using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PostLogoutAPIFront : MonoBehaviour
{
    private string url = "http://113.198.229.158:1435/shooting-miner/userids/logout";

    public void SendLogout()
    {
        StartCoroutine(UserLogout());
    }

    IEnumerator UserLogout()
    {
        // 빈 바디의 POST 요청 생성
        UnityWebRequest uwr = new UnityWebRequest(url, "POST");
        uwr.uploadHandler = new UploadHandlerRaw(new byte[0]);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        // 저장된 JSESSIONID를 헤더에 포함
        string jsession = PlayerPrefs.GetString("JSESSIONID");
        Debug.Log("PostLogoutAPIFront - 25 Player JSESSIONID" + PlayerPrefs.GetString("JSESSIONID"));
        if (!string.IsNullOrEmpty(jsession))
        {
            uwr.SetRequestHeader("Cookie", $"JSESSIONID={jsession}");
        }

        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.Success)
        {
            // 서버가 Set-Cookie 헤더로 만료 쿠키를 내려주면, 로컬에서도 삭제
            string setCookie = uwr.GetResponseHeader("Set-Cookie");
            if (!string.IsNullOrEmpty(setCookie) && setCookie.Contains("JSESSIONID"))
            {
                PlayerPrefs.DeleteAll();
                Debug.Log("PostLogoutAPIFront 39 - Cleared JSESSIONID from PlayerPrefs");
            }
            else
            {
                Debug.LogWarning("PostLogoutAPIFront 43 - No JSESSIONID expiration header received.");
            }
        }
        else
        {
            Debug.LogError($"GameSelectManager 48 - ClearCookie Error: {uwr.error}");
        }
    }
}
