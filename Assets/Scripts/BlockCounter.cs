using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockCounter : MonoBehaviour
{
    [SerializeField] private int stageGap;
    [SerializeField] private int lastRemain;
    [SerializeField] private int forNum;
    [SerializeField] private int countIndex;
    [SerializeField] private int probIndex;
    [SerializeField] private List<float> probList;
    [SerializeField] private List<int> countList;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        stageGap = 3;
        lastRemain = -1;
        forNum = 0;
        countIndex = 0;
        probIndex = 0;
        probList = new List<float>() { 0.5f, 0.5f, 0.4f, 0.3f, 0.3f};
        countList = new List<int>() { 8, 16, 24, 32, 40};
    }

    public void SelectProb()
    {
        int stageRest = gameManager.Stage % stageGap;
        switch (stageRest)
        {
            case 0:
                forNum = 5;
                probIndex = 2;
                break;
            default:
                forNum = 2;
                probIndex = 0;
                break;
        }
        if(gameManager.Stage < 10 && lastRemain == 0)
        {
            countIndex++;
        }
        lastRemain = stageRest;
        if(gameManager.Stage == 10)
        {
            countList.Clear();
            countList = new List<int>() { 24, 32, 40 };
            probList.Clear();
            probList = new List<float>() { 0.6f, 0.4f, 0.4f, 0.3f, 0.3f};
            countIndex = 1;
        }
    }

    int ChooseProbAndCount()
    {
        float total = 0;
        int lastCountIndex = countIndex;

        for (int j = probIndex; j < forNum; j++)
        {
            total += probList[j];
        }
        float randomPoint = Random.value * total;

        for (int i = probIndex; i < forNum; i++)
        {
            if (randomPoint < probList[i])
            {
                countIndex = lastCountIndex;
                return countList[countIndex];
            }
            else
            {
                randomPoint -= probList[i];
            }
            countIndex++;
        }
        countIndex = lastCountIndex;
        return forNum;
    }
}
