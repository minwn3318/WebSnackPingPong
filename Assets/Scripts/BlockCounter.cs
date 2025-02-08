using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockCounter : MonoBehaviour
{
    [SerializeField] private int stageGap;
    [SerializeField] private List<float> probList;
    [SerializeField] private List<int> probArray;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        stageGap = 3;
        probList = new List<float>() { 0.5f, 0.5f, 0.4f, 0.3f, 0.3f, 0.4f};
        probArray = new List<int>() { 8, 16, 24, 32, 40};
    }

    public int CaculateCountFromStage()
    {
        int stageState = gameManager.Stage % stageGap;
        if (stageState != 0)
        {

        }
        
        return gameManager.Stage;
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
