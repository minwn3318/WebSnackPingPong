using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Collections;
using System;
using UnityEditor.PackageManager.Requests;
using UnityEngine.InputSystem;

public class GetRankingAPIFront : MonoBehaviour
{
    private const string mainURL = "http://localhost:8080/shooting-miner/play-records/serach";
    private string maxStage = "/max-stage";
    private string maxScore = "/max-score";
    private string maxTotal = "/max-total";
    [SerializeField]
    private string gameID = "gamer01";


    // Start is called before the first frame update
    void Start()
    {
        SetGameID("gamer01");
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.A))
        {
            StartCoroutine(GetMaxStage(maxStage));
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            StartCoroutine(GetMaxScore(maxScore));
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            StartCoroutine(GetMaxTotal(maxTotal));
        }
    }

    public void SetGameID(string id)
    {
        gameID = id;
    }

    public string GetGameID()
    {
        return gameID;
    }

    IEnumerator GetMaxStage(string stageURL)
    {
        string url = $"{mainURL}{stageURL}?userId={UnityWebRequest.EscapeURL(GetGameID())}";
        Debug.Log(url);
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"GET Error: {request.error}");
                StartCoroutine(GetMaxStage(url));
            }
            else 
            {
                string jsonResponse = request.downloadHandler.text;
                if(jsonResponse == null)
                {
                    Debug.Log("null");
                }
                Debug.Log($"GET Stage Success: {jsonResponse}");
            }
        }
    }
    IEnumerator GetMaxScore(string scoreURL)
    {
        string url = $"{mainURL}{scoreURL}?userId={UnityWebRequest.EscapeURL(GetGameID())}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"GET Error: {request.error}");
                StartCoroutine(GetMaxScore(url));
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log($"GET Score Success: {jsonResponse}");
            }
        }
    }
    IEnumerator GetMaxTotal(string totalURL)
    {
        string url = $"{mainURL}{totalURL}?userId={UnityWebRequest.EscapeURL(GetGameID())}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"GET Error: {request.error}");
                StartCoroutine(GetMaxTotal(url));
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log($"GET Total Success: {jsonResponse}");
            }
        }
    }
}
