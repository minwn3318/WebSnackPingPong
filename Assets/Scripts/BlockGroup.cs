using System.Collections.Generic;
using UnityEngine;

public class BlockGroup : MonoBehaviour
{
    [SerializeField] private List<GameObject> blocks = new List<GameObject>();
    [SerializeField] private BlockCounter couter;
    [SerializeField] private int arrySize = 84;
    [SerializeField] private int firstSize = 21;
    void Awake()
    {
        couter = FindAnyObjectByType<BlockCounter>();
        Debug.Log(couter);
        for (int i = 0; i < arrySize; i++)
        {
            GameObject obj_v = transform.GetChild(i).gameObject;
            blocks.Add(obj_v);
        }
        /*if(this.CompareTag("FirstStap"))
        {
            SettingBlockCount(firstSize);
            couter.Test();
            couter.SelectProb();
            couter.ChooseProbAndCount();
        }*/
    }

    public void SettingBlockCount(int start)
    {
        couter.SelectProb();
        for (int i = start; i < arrySize; i++)
        {
            Block obj_v = blocks[i].GetComponent<Block>();
            obj_v.EnableInteract(couter.ChooseProbAndCount());
        }
    }
}
