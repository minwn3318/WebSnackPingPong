using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ballRB;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 lastVelocity;
    [SerializeField] private Player myPlayer;
    // Start is called before the first frame update
    void Awake()
    {
        ballRB = GetComponent<Rigidbody2D>();
        myPlayer = GameObject.FindAnyObjectByType<Player>();
        speed = 25f;
    }

    public float Speed
    {
        get 
        {
            return speed;
        }
        set 
        {
            speed = value;
        }
    }
    public void Move(Vector2 velocity_v)
    {
        ballRB.velocity = velocity_v * speed;
        lastVelocity = ballRB.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Ball"))
        {
            return;
        }
        else if(collision.collider.CompareTag("Wall"))
        {
            ballRB.velocity = Vector2.zero;
            myPlayer.Return(this.gameObject);
            return;
        }
        ballRB.velocity = 
            Mathf.Max(speed, 0f)
            * Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        lastVelocity = ballRB.velocity;
    }   
}
