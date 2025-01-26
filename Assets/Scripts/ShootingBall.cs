using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ballRB;
    [SerializeField] private float speed = 35f;
    [SerializeField] private bool comeBackHome = false;
    [SerializeField] private float time = 1f;
    [SerializeField] private Vector2 lastVelocity;
    [SerializeField] private Player myPlayer;
    // Start is called before the first frame update
    void Awake()
    {
        ballRB = GetComponent<Rigidbody2D>();
        myPlayer = GameObject.FindAnyObjectByType<Player>();
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
        StartCoroutine(ComeBakcAllow());
        ballRB.velocity = velocity_v * speed;
        lastVelocity = ballRB.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Wall"))
        {
            ballRB.velocity = Vector2.zero;
            comeBackHome = false;
            myPlayer.Return(this.gameObject);
            return;
        }
        ballRB.velocity = 
            Mathf.Max(speed, 0f) * 
            Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        lastVelocity = ballRB.velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!comeBackHome) return;
        ballRB.velocity = Vector2.zero;
        comeBackHome = false;
        myPlayer.Return(this.gameObject);
        return;
    }

    private IEnumerator ComeBakcAllow()
    {
        yield return new WaitForSeconds(time);
        comeBackHome=true;    
    }
}
