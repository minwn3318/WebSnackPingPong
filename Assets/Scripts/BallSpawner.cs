using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ball;
    private Queue<GameObject> ball_Poiner;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(ball, ball.transform.position, ball.transform.rotation);
            Debug.Log("spawn");
        }
    }

    void SpawnBall()
    {

    }
}
