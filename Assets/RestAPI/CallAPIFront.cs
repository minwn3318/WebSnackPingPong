using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CallAPIFront : MonoBehaviour
{

    [Header("stage")]
    public int st;
    [Header("score")]
    public int sc;

    [Header("message")]
    public string message;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.A))
        {
            StartCoroutine(Hello());
        }

    }
    IEnumerator Hello()
    {
        string url = "http://113.198.229.158:1435/shooting-miner/hello";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"GetRankingAPIFront -87 GET Error: {request.error}");
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log("hello 49 - "+jsonResponse);
            }
        }
    }
}
