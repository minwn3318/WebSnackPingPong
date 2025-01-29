using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ballRB;
    [SerializeField] private float speed = 35f;
    [SerializeField] private float speedLose = 25f;
    [SerializeField] private bool comeBackHome = false;
    [SerializeField] private float time = 1f;
    [SerializeField] private float forceTime = 3f;
    [SerializeField] private Vector2 lastVelocity;
    [SerializeField] private Player myPlayer;
    [SerializeField] public int myindex;
    // Start is called before the first frame update
    void Awake()
    {
        ballRB = GetComponent<Rigidbody2D>();
        myPlayer = GameObject.FindAnyObjectByType<Player>();
        lastVelocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (ballRB.velocity.magnitude < speedLose)
        {
            //Debug.Log("before : " + ballRB.velocity + ", " +ballRB.velocity.magnitude);
            float gapManitude = speed - ballRB.velocity.magnitude;
            Vector2 modifyVector = -((lastVelocity.normalized * gapManitude) + ballRB.velocity);
            //Debug.Log("gap magnitude : "+ modifyVector.magnitude);
            //Debug.Log("gap Vector : " + modifyVector);
            ballRB.velocity = modifyVector;
            //Debug.Log("after : " + ballRB.velocity + ", " + ballRB.velocity.magnitude);
        }
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
        StartCoroutine(ForceReturn());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Wall"))
        {
            lastVelocity = Vector2.zero;
            ballRB.velocity = lastVelocity;
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
        if (!comeBackHome) return;
        lastVelocity = Vector2.zero;
        ballRB.velocity = lastVelocity;
        comeBackHome = false;
        myPlayer.Return(this.gameObject);
        return;
    }

    private IEnumerator ComeBakcAllow()
    {
        yield return new WaitForSeconds(time);
        comeBackHome=true;    
    }

    private IEnumerator ForceReturn()
    {
        yield return new WaitForSeconds(forceTime);
        myPlayer.Return(this.gameObject);
    }
}
