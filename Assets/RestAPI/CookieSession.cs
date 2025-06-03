using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieSession : MonoBehaviour
{
    public static CookieSession Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            // 첫 번째 생성된 인스턴스라면
            Instance = this;
            // 이 GameObject를 씬 전환 시에도 파괴되지 않도록 설정
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // 두 번째 이후로 생성된 Instance라면, 파괴
            Destroy(gameObject);
        }
    }

    [Header("Cookie")]
    [SerializeField]
    private string cookie;

    public string ParseAndSaveCookie(string setCookieHeader, string cookieName)
    {
        string[] parts = setCookieHeader.Split(';');
        foreach (var part in parts)
        {
            var kv = part.Trim().Split('=');
            if (kv.Length == 2 && kv[0] == cookieName)
            {
                cookie = kv[1];
                return kv[1];
            }
        }
        return null;
    }

    public void DeleteCookie()
    {
        cookie = null;
    }

    public string GetCookie()
    {
        return cookie;
    }
}
