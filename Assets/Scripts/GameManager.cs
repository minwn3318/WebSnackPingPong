using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject firstBlockGroup;
    [SerializeField] private GameObject secondBlockGroup;
    [SerializeField] private Player playerScript;
    [SerializeField] private bool playing;
    [SerializeField] private int score;
    [SerializeField] private int stage;

    void Awake()
    {
        firstBlockGroup = transform.GetChild(0).gameObject;
        secondBlockGroup = transform.GetChild(1).gameObject;
        playerScript = FindAnyObjectByType<Player>();
        playing = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("GameStart");
            playerScript.SpawnPlayer();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("GameOver");
            playerScript.DestoryPlayer();
        }
    }

    public int Stage
    {
        get { return stage; } 
    }    

    public void plusStage()
    {
        stage++;
    }

    public void ReStart() 
    {
        score = 0;
        stage = 1;
        playing = true;
    }

}
