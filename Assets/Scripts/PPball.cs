using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPball : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direct;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("create");
        direct.x = Random.Range(200, 200);
        direct.y = Random.Range(200, 200);
        Debug.Log(direct);
        rb.AddForce(direct);
        Debug.Log("shoot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
