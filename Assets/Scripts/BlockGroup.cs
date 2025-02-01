using System.Collections.Generic;
using UnityEngine;

public class BlockGroup : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<GameObject> blocks = new List<GameObject>();
    [SerializeField] private int arrySize = 84;
    [SerializeField] private int firstSize = 21;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        for (int i = 0; i < arrySize; i++)
        {
            GameObject obj_v = transform.GetChild(i).gameObject;
            blocks.Add(obj_v);
        }
        if(this.CompareTag("FirstStap"))
        {
            int currentStage = gameManager.Stage;
            for(int i = firstSize; i < arrySize; i++)
            {
                Block obj_v = blocks[i].GetComponent<Block>();
                obj_v.EnableInteract(10);
            }
        }
    }
}
