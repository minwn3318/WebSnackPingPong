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

    public int Size
    {
        get { return size; }
        set { size = value; }
    }

    public void InitQueue(int size_v)
    {
        shooter = new Queue<GameObject>(size_v);
        GameObject[] instantQueue = new GameObject[size_v];
        for (int count = 0; count < size_v; count++)
        {
            instantQueue[count] = Instantiate(ball, this.transform.position, this.transform.rotation);
        }
        foreach (GameObject obj_v in instantQueue)
        {
            PollingQueue(obj_v);
        }
    }

    public void PollingQueue(GameObject obj_v)
    {
        obj_v.SetActive(false);
        shooter.Enqueue(obj_v);
        size++;
    }

    public GameObject PopQueue()
    {
        GameObject obj_v = shooter.Dequeue();
        obj_v.SetActive(true);
        size--;
        return obj_v;
    }
}
