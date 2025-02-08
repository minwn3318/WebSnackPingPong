using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockCounter : MonoBehaviour
{
    [SerializeField] private int stageGap;
    [SerializeField] private int lastRemain;
    [SerializeField] private int forNum;
    [SerializeField] private int probIndex;
    [SerializeField] private List<float> probList;
    [SerializeField] private List<int> probArray;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        stageGap = 3;
        forNum = 0;
        probIndex = 0;
        lastRemain = -1;
        probList = new List<float>() { 0.5f, 0.5f, 0.4f, 0.3f, 0.3f, 0.4f};
        probArray = new List<int>() { 8, 16, 24, 32, 40};
    }

    public void SelectProb()
    {
        int stageRest = gameManager.Stage % stageGap;
        switch (stageRest)
        {
            case 0:
                forNum = 3;
                break;
            default:
                forNum = 2;
                break;
        }
        if(gameManager.Stage < 10 && lastRemain == 0)
        {
            probIndex++;
        }
        lastRemain = stageRest;
        if(gameManager.Stage == 10)
        {
            probArray.Clear();
            probArray = new List<int>() { 24, 32, 40 };
            probIndex = 1;
        }
    }

    float Choose(float[] probs)
    {

        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }
}
