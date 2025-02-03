using System.Collections;
using System.Collections.Generic;
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

        prob.Clear();
        probList.Clear();
        probListSelected.Clear();

        for (int i = 0; i < 2; i++)
        {
            for(int j = 0; j < 2; j++)
            {

            }
        }
        prob.Add(0.5f);
        prob.Add(0.5f);
        probList.Add(prob);

        prob.Clear();

        prob.Add(0.4f);
        prob.Add(0.3f);
        prob.Add(0.3f);
        probList.Add(prob);
        probListSelected.Add(probList);

        prob.Clear();
        probList.Clear();

        prob.Add(0.6f);
        prob.Add(0.4f);
        probList.Add(prob);

        prob.Clear();

        prob.Add(0.3f);
        prob.Add(0.3f);
        prob.Add(0.4f);
        probList.Add(prob);
        probListSelected.Add(probList);

        Debug.Log(probListSelected);

    }
    public int CaculateCountFromStage()
    {
        int currentStage = (gameManager.Stage / stageGap);
        
        return gameManager.Stage;
    }
}
