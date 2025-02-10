using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BlockGroup firstBlockGroup;
    [SerializeField] private BlockGroup secondBlockGroup;
    [SerializeField] private Player playerScript;
    [SerializeField] private int score;
    [SerializeField] private int stage;

    void Awake()
    {
        firstBlockGroup = transform.GetChild(0).GetComponent<BlockGroup>();
        secondBlockGroup = transform.GetChild(1).GetComponent<BlockGroup>();
        playerScript = FindAnyObjectByType<Player>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("GameStart");
            playerScript.SpawnPlayer();
            StartSetting();
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

    public void ToggleCurrentStage()
    {
        
    }

    public void StartSetting() 
    {
        score = 0;
        stage = 1;
    }

}
