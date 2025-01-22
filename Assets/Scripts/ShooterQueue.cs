using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

public class ShooterQueue : MonoBehaviour
{
    [SerializeField] private Queue<GameObject> shooter;
    [SerializeField] private GameObject ball;
    [SerializeField] private int size;
    // Start is called before the first frame update
    void Awake()
    {
        InitQueue(10);
    }

    public int Size
    {
        get { return size; }
        set { size = value; }
    }

    public void InitQueue(int size_v)
    {
        size = size_v;
        shooter = new Queue<GameObject>(size);
        GameObject[] instantQueue = new GameObject[size];
        for (int count = 0; count < size; count++)
        {
            instantQueue[count] = Instantiate(ball, this.transform.position, this.transform.rotation);
        }
        foreach (GameObject obj_v in instantQueue)
        {
            PushQueue(obj_v);
        }
        return;
    }

    public void PushQueue(GameObject obj_v)
    {
        obj_v.SetActive(false);
        shooter.Enqueue(obj_v);
    }

    public void PopQueue()
    {
        GameObject obj_v = shooter.Dequeue();
        obj_v.SetActive(true);

    }
}
