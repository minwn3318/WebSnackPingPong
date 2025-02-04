using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockCounter : MonoBehaviour
{
    [SerializeField] private int intiCount;
    [SerializeField] private int stageGap;
    [SerializeField] private List<List<List<float>>> probListSelected;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        intiCount = 8;
        stageGap = 3;
        initList();
    }

    public void initList()
    {
        probListSelected = new List<List<List<float>>>();
        List<List<float>> probList = new List<List<float>>();
        List<float> prob = new List<float>();

        probListSelected.Clear();
        probList.Clear();
        prob.Clear();

        prob.Add(0.5f);
        prob.Add(0.5f);
        probList.Add(prob.ToList());

        prob.Clear();

        prob.Add(0.4f);
        prob.Add(0.3f);
        prob.Add(0.3f);
        probList.Add(prob.ToList());
        probListSelected.Add(probList.ToList());

        prob.Clear();
        probList.Clear();

        prob.Add(0.6f);
        prob.Add(0.4f);
        probList.Add(prob.ToList());

        prob.Clear();

        prob.Add(0.3f);
        prob.Add(0.4f);
        prob.Add(0.4f);
        probList.Add(prob.ToList());
        probListSelected.Add(probList.ToList());
        Debug.Log(probListSelected[0][0][0]);
        Debug.Log(probListSelected[0][0][1]);
        Debug.Log(probListSelected[0][1][0]);
        Debug.Log(probListSelected[0][1][1]);
        Debug.Log(probListSelected[0][1][2]);
        Debug.Log(probListSelected[1][0][0]);
        Debug.Log(probListSelected[1][0][1]);
        Debug.Log(probListSelected[1][1][0]);
        Debug.Log(probListSelected[1][1][1]);
        Debug.Log(probListSelected[1][1][2]);

    }
    public int CaculateCountFromStage()
    {
        int currentStage = (gameManager.Stage / stageGap);
        
        return gameManager.Stage;
    }
}
