using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject firstBlockGroup;
    [SerializeField] private GameObject secondBlockGroup;
    [SerializeField] private int score;
    [SerializeField] private int stage;

    void Awake()
    {
        firstBlockGroup = transform.GetChild(0).gameObject;
        secondBlockGroup = transform.GetChild(1).gameObject;
        Re();
    }

    public int Stage
    {
        get { return stage; } 
    }    

    public void plusStage()
    {
        stage++;
    }

    public void Re() 
    {
        score = 0;
        stage = 1;
    }

}
