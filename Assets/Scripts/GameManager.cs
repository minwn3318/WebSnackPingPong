using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int stage;
    [SerializeField] private GameObject blocksGrupsFirst;
    [SerializeField] private GameObject blocksGrupsSecond;

    void Awake()
    {
        blocksGrupsFirst = transform.GetChild(0).gameObject;
        blocksGrupsSecond = transform.GetChild(1).gameObject;
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
