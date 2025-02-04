using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockCounter : MonoBehaviour
{
    [SerializeField] private int intiCount;
    [SerializeField] private int stageGap;
    [SerializeField] private List<float> probListSelected;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        intiCount = 8;
        stageGap = 3;
        probListSelected = new List<float>() {0.3f, 0.4f, 0.5f, 0.6f};
    }

    public int CaculateCountFromStage()
    {
        int currentStage = (gameManager.Stage / stageGap);
        
        return gameManager.Stage;
    }
}
