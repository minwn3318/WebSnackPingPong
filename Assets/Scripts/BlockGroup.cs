using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BlockGroup : MonoBehaviour
{
    [SerializeField] private GameObject block;
    [SerializeField] private List<List<GameObject>> rowBlocks;
    [SerializeField] private int columnSize =7;
    [SerializeField] private int rowSize = 12;
    // Start is called before the first frame update
    void Awake()
    {
        rowBlocks = new List<List<GameObject>>();
        float blockPosX = this.transform.position.x;
        float blockPosY = this.transform.position.y;
        float blockGap = 2f;

        for(int i = 0; i < rowSize; i++)
        {
            List<GameObject> columnBlocks = new List<GameObject>();
            for(int j = 0; j < columnSize; j++)
            {
                Vector2 blockPos = new Vector2(blockPosX, blockPosY);
                Debug.Log(blockPos);
                columnBlocks.Add(Instantiate(block, blockPos, this.transform.rotation));
                blockPosX += blockGap;
                if (blockPosX > -this.transform.position.x) blockPosX = this.transform.position.x; 
            }
            rowBlocks.Add(columnBlocks);
            blockPosY += blockGap;
        }
    }

}
