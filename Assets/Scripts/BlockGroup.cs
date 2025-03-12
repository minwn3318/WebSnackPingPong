using System.Collections.Generic;
using UnityEngine;

public class BlockGroup : MonoBehaviour // 블럭 그룹 클래스다
{
    [SerializeField] private List<GameObject> blocks = new List<GameObject>(); // 블럭을 저장하는 1벡터 리스트다
    [SerializeField] private Transform groupPosition; // 블럭그룹(전체)의 위치이다
    [SerializeField] private bool currentStage; // 블럭그룹을 2개를 계속 돌려 쓸것이기에 지금 이 그룹이 현재 스테이지에 나타나야하는 스테이지인지 판정한다
    [SerializeField] private BlockCounter couter; //블럭의 카운트를 정해줄 스크립트이다
    [SerializeField] private int arrySize = 84; // 블럭의 행 갯수다
    [SerializeField] private int firstSize = 21; //블럭의 열 갯수다

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

    public void SettingBlockCount(int start) // 지금 보여져야한다면 블럭의 카운트들을 정하고 보이게한다 (아직 지금은 사용하지 않는다)
    {
        couter.SelectProb();
        for (int i = start; i < arrySize; i++)
        {
            Block obj_v = blocks[i].GetComponent<Block>();
            obj_v.EnableInteract(couter.ChooseProbAndCount());
        }
    }

    public void ResetBlockGruop() // 게임 오버시 초기화 시킬 함수다
    {
        
    }
}
