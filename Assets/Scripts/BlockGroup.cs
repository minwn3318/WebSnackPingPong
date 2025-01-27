using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BlockGroup : MonoBehaviour
{
    [SerializeField] private List<GameObject> blocks = new List<GameObject>();
    [SerializeField] private int arrySize = 84;
    [SerializeField] private int firstSize = 21;
    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < arrySize; i++)
        {
            GameObject obj_v = transform.GetChild(i).gameObject;
            blocks.Add(obj_v);
        }
        if(this.CompareTag("FirstStap"))
        {
            for(int i = firstSize; i < arrySize; i++)
            {
                Block obj_v = blocks[i].GetComponent<Block>();
                obj_v.EnableInteract();
            }
        }
    }
}
