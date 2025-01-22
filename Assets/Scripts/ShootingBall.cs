using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ballRB;
    [SerializeField] private float power;
    // Start is called before the first frame update
    void Start()
    {
        ballRB = GetComponent<Rigidbody2D>();
    }

    public float Poewr
    {
        get 
        {
            return power;
        }
        set 
        { 
            power = value;
        }
    }
    public void Shoot(Vector2 velocity_v)
    {
        ballRB.velocity = velocity_v;
    }
}
