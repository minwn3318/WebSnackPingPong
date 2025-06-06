using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CallAPIFront : MonoBehaviour
{
    public PostLoginJoinAPIFront PostLoginJoinAPIFront;
    public GetRankingAPIFront GetRankingAPIFront;
    public PostRecordAPIFront PostRecordAPIFront;
    public PostLogoutAPIFront PostLogoutAPIFront;

    [Header("stage")]
    public int st;
    [Header("score")]
    public int sc;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.A))
        {
            StartCoroutine(Hello());
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            PostLoginJoinAPIFront.SendJoin("tesrer01");
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            PostLoginJoinAPIFront.SendJoinCheck("tesrer01");
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            PostLoginJoinAPIFront.SendLogin("tesrer01");
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            GetRankingAPIFront.ReciveMaxStage();
        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            GetRankingAPIFront.ReciveMaxScore();
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            GetRankingAPIFront.ReciveMaxTotal();
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            GetRankingAPIFront.ReciveTopUsers();
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            PostRecordAPIFront.SendRecord(st, sc);
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            PostLogoutAPIFront.SendLogout();
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
