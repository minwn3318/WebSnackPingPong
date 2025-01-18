using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ball;
    private Queue<GameObject> ballPointer;
    private Vector3 spawnPoint = new Vector3(0,-2,0);

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(ball, transform.position + spawnPoint, ball.transform.rotation);
        }
    }

    void SpawnBall()
    {

    }
}
