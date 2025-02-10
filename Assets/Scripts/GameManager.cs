using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject firstBlockGroup;
    [SerializeField] private GameObject secondBlockGroup;
    [SerializeField] private int score = 0;
    [SerializeField] private int stage = 1;

    void Awake()
    {
        Re();
        firstBlockGroup = transform.GetChild(0).gameObject;
        secondBlockGroup = transform.GetChild(1).gameObject;
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
