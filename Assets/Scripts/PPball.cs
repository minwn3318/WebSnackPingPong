using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPball : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direct = new Vector2 (0,-7);
    private float velocity = 350;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Shoot();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {
        int choose = Random.Range(0, 2);
        if (choose == 1)
        {
            direct.x = Random.Range(4, 9);
        }
        else
        {
            direct.x = Random.Range(-4, -9);
        }
        rb.AddForce(direct.normalized * velocity);
    }
}
