using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ballRB;
    [SerializeField] private float power;
    // Start is called before the first frame update
    void Awake()
    {
        ballRB = GetComponent<Rigidbody2D>();
        power = 20f;
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
    public void Move(Vector2 velocity_v)
    {
        Debug.Log(velocity_v * power);
        ballRB.velocity = velocity_v * power;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("reflect");
        Debug.Log(-collision.transform.position.normalized);
        Debug.Log(Vector2.Reflect(ballRB.velocity, collision.transform.position.normalized));
        ballRB.velocity = Vector2.Reflect(ballRB.velocity, -collision.transform.position.normalized);
    }
}
