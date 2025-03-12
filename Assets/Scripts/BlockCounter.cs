using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockCounter : MonoBehaviour // 블럭의 카운트를 정할 클래스다 아직 완성되지 않아 사용하고 있지 않다
{
    [SerializeField] private int stageGap; //스테이지
    [SerializeField] private int stageRest;
    [SerializeField] private int lastRemain;
    [SerializeField] private int forNum;
    [SerializeField] private int countIndex;
    [SerializeField] private int probIndex;
    [SerializeField] private List<float> probList; // 확률표로 블록 그룹에서 몇 퍼센트의 블록이 해당 카운트를 가질지 정한다
    [SerializeField] private List<int> countList; // 카운트 목록이다 
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        Debug.Log(gameManager);
        stageGap = 3;
        stageRest = 0;
        lastRemain = -1;
        forNum = 0;
        countIndex = 0;
        probIndex = 0;
        probList = new List<float>() { 0.5f, 0.5f, 0.4f, 0.3f, 0.3f};
        countList = new List<int>() { 8, 16, 24, 32, 40};
    }

    public void Test() // 테스트 용으로 카운트의 초기값을 확인하고 확률 리스트가 정상적으로 되었는지 확인하다
    {
        Debug.Log(gameManager);
        Debug.Log(stageGap);
        Debug.Log(stageRest);
        Debug.Log(lastRemain);
        Debug.Log(forNum);
        Debug.Log(countIndex);
        Debug.Log(probIndex);
        for(int i =0; i < 5; i++)
        {
            Debug.Log(probList[i]);
        }
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(countList[i]);
        }
        Debug.Log("I am Counter");
    }

    public void SelectProb()
    {
        Debug.Log(gameManager);
        //Debug.Log(gameManager.Stage);
        //stageRest = gameManager.Stage % stageGap;
        Debug.Log("I am SelectProb");


        /*
        //Debug.Log("stageRest : "+stageRest);


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
        //Debug.Log("forNum : "+forNum);
        //Debug.Log("probIndex : " + probIndex);


        if (gameManager.Stage < 10 && lastRemain == 0)
        {
            countIndex++;
            //Debug.Log("countIndex Up : " + countIndex);

        }
        lastRemain = stageRest;

        //Debug.Log("lastRemain : " + lastRemain);
        //Debug.Log("countIndex : " + countIndex);


        if (gameManager.Stage == 10)
        {
            countList.Clear();
            countList = new List<int>() { 32, 40 };
            probList.Clear();
            probList = new List<float>() { 0.6f, 0.4f, 0.4f, 0.3f, 0.3f};
            countIndex = 0;
        }
        Debug.Log("end");
        */
    }

    public int ChooseProbAndCount()
    {
        Debug.Log(gameManager);
        Debug.Log("ChooseProbAndCount");


        /*int lastCountIndex = countIndex;
        for(int i = probIndex; i < forNum; i++)
        {
            Debug.Log(probList[i]);
            Debug.Log(countList[countIndex]);
            Debug.Log("countIndex : " + countIndex);
            countIndex++;
        }
        countIndex = lastCountIndex;*/


        /*
        float total = 0;
        int lastCountIndex = countIndex;

        for (int j = probIndex; j < forNum; j++)
        {
            total += probList[j];
        }
        float randomPoint = Random.value * total;

        if (gameManager.Stage > 9)
        {
            if(stageRest == 1)
            {
                countList.RemoveAt(24);
            }
            else if(stageRest == 2)
            {
                countList.Insert(1, 24);
            }
        }

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
        */
        return 1;
    }
}
