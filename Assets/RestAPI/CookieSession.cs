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
            // ù ��° ������ �ν��Ͻ����
            Instance = this;
            // �� GameObject�� �� ��ȯ �ÿ��� �ı����� �ʵ��� ����
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // �� ��° ���ķ� ������ Instance���, �ı�
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
