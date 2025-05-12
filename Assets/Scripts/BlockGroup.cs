using System.Collections.Generic;
using UnityEngine;

public class BlockGroup : MonoBehaviour
{
    [SerializeField] private List<GameObject> blocks = new List<GameObject>();
    [SerializeField] private Transform groupPosition;
    [SerializeField] private bool currentStage;
    [SerializeField] private BlockCounter couter;
    [SerializeField] private int arrySize = 84;
    [SerializeField] private int firstSize = 21;

    void Awake()
    {
        couter = FindAnyObjectByType<BlockCounter>();
        groupPosition = GetComponent<Transform>();
        for (int i = 0; i < arrySize; i++)
        {
            GameObject obj_v = transform.GetChild(i).gameObject;
            blocks.Add(obj_v);
        }
    }

    public void SettingBlockCount(int start)
    {
        couter.SelectProb();
        for (int i = start; i < arrySize; i++)
        {
            Block obj_v = blocks[i].GetComponent<Block>();
            obj_v.SetCount(couter.ChooseProbAndCount()); // 체력 설정 먼저
            obj_v.EnableInteract();                      // 그 다음 보이게
        }
    }

    public void ResetBlockGruop()
    {
        // 필요 시 구현
    }
}
