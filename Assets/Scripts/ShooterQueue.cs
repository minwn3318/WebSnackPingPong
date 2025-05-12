using System.Collections.Generic;
using UnityEngine;

public class ShooterQueue : MonoBehaviour // 공을 저장하는 풀이다 (오브젝트 풀링 활용)
{
    [SerializeField] private Queue<GameObject> shooter; // 공을 저장하는 큐이다
    [SerializeField] private GameObject ball; // 공 오브젝트이다
    [SerializeField] private int capacity = 0; // 큐에 담겨져있는 현재 공의 수량이다
    [SerializeField] private int size; // 큐의 크기이다

    public int Capacity
    {
        get { return capacity; }
        set { capacity = value; }
    }

    public int Size
    {
        get { return size; }
        set { size = value; }
    }

    public void InitQueue(int size_v) // 큐를 초기화하고 공을 생성해 큐에 넣는다 
    {
        size = size_v;
        shooter = new Queue<GameObject>(size);

        if (ball == null)
        {
            Debug.LogError("ShooterQueue: ball 프리팹이 비어 있습니다. 프리팹을 인스펙터에 연결하세요.");
            return;
        }

        GameObject[] instantQueue = new GameObject[size];
        for (int count = 0; count < size; count++)
        {
            instantQueue[count] = Instantiate(ball, this.transform.position, this.transform.rotation);
        }

        foreach (GameObject obj_v in instantQueue)
        {
            PollingQueue(obj_v);
        }

        Debug.Log($"ShooterQueue: InitQueue 완료 - size: {size}, capacity: {capacity}");
    }

    public void RemoveQueue() // 플레이어가 게임오버됐을 때 큐를 아예 삭제한다
    {
        if (shooter != null)
        {
            while (shooter.Count > 0)
            {
                GameObject obj_v = shooter.Dequeue();
                Destroy(obj_v);
            }
        }

        capacity = 0;
        size = 0;
        shooter = null;

        Debug.Log("ShooterQueue: RemoveQueue 완료");
    }

    public void PollingQueue(GameObject obj_v) // 공을 큐에 넣는다
    {
        obj_v.SetActive(false);
        shooter.Enqueue(obj_v);
        capacity++;
    }

    public GameObject PopQueue() // 큐에서 공을 꺼낸다
    {
        if (shooter == null || shooter.Count == 0)
        {
            Debug.LogError("ShooterQueue: PopQueue 실패 - Queue가 비어 있습니다.");
            return null;
        }

        GameObject obj_v = shooter.Dequeue();
        obj_v.SetActive(true);
        capacity--;
        return obj_v;
    }
}
