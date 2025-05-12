using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour // 공 오브젝트이다
{
    [SerializeField] private Rigidbody2D ballRB; //공 Rigidbody
    [SerializeField] private float speed = 35f; // 기본 속력
    [SerializeField] private float speedLose = 25f; // 최소 속력
    [SerializeField] private bool comeBackHome = false; // 공 발사 후, 공을 되돌아오게 할 수 있는 상태여부
    [SerializeField] private float forceReturnTime = 3f; // 공을 강제로 돌아오게 하는데 기다리는 시간
    [SerializeField] private Vector2 lastVelocity; // 마지막 속도
    [SerializeField] private Player myPlayer; // 공을 소유하는 플레이어 스크립트

    void Awake() // 게임 시작시 공 초기화
    {
        ballRB = GetComponent<Rigidbody2D>();
        myPlayer = GameObject.FindAnyObjectByType<Player>();
        lastVelocity = Vector2.zero;
    }

    private void FixedUpdate() // 물리엔진 업데이트, 발사 된 후 공의 움직임을 업데이트 시킨다
    {
        if (ballRB.velocity.magnitude < speedLose) // 속력이 최소 속력보다 낮아지면 기본 속력으로 맞춘다 속력이 낮아지면 재미가 심하게 반감된다
        {
            float gapManitude = speed - ballRB.velocity.magnitude;
            Vector2 modifyVector =
                -((lastVelocity.normalized * gapManitude * (speed / speedLose))
                + ballRB.velocity);
            ballRB.velocity = modifyVector;
        }
        if (ballRB.velocity == -lastVelocity) //만약 현재 속도가 마지막 속도의 (-)버전과 같다면, xy역방향을 취해준다 공이 같은 자리를 맴도는걸 막아준다
        {
            ballRB.velocity = new Vector2(ballRB.velocity.y, ballRB.velocity.x);
        }
    }

    public float Speed //속력값을 정하거나 정해진 속력을 가져온다
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
    public void Move(Vector2 velocity_v) //공을 발사한다
    {
        ballRB.velocity = velocity_v * speed;
        lastVelocity = ballRB.velocity;
        StartCoroutine(ForceReturn());
    }

    private void OnCollisionEnter2D(Collision2D collision) //공이 물체와 부딪혔을 때 반응하는 함수다
    {
        if (collision.collider.CompareTag("Wall")) // 공이 아랫 바닥과 부딪히면 반드시 다시 플레이어에게 돌아온다
        {
            lastVelocity = Vector2.zero;
            ballRB.velocity = lastVelocity;
            comeBackHome = false;
            myPlayer.Return(this.gameObject);
            //return;
        }
        
        //아래에는 공이 반사되는 것을 구현한 코드다. 항상 바뀌기전 속도를 저장해둔다
        ballRB.velocity =
            Mathf.Max(speed, 0f) *
            Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        lastVelocity = ballRB.velocity;

        if (collision.collider.CompareTag("Diamond")) // 다이아몬드에 부딪히면 점수 추가
        {
            GameManager.Instance.AddScore(100); // 점수 100점 추가
            GameObject diamond = collision.gameObject;
            GameManager.Instance.OnDiamondHit(); // 먼저 처리
            Destroy(diamond); // 나중에 파괴
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // 공이 플레이어와 충돌했을 때 반드시 돌아온다
    {
        if (!comeBackHome) return;
        lastVelocity = Vector2.zero;
        ballRB.velocity = lastVelocity;
        comeBackHome = false;
        myPlayer.Return(this.gameObject);
        return;
    }

    private IEnumerator ForceReturn() // 공이 반한되는데 걸리는 시간이 지난 후 반드시 반환된다
    {
        float shootingTime = Time.time;
        float restTime = 0;
        while (restTime < forceReturnTime)
        {
            yield return null;
            restTime = Time.time - shootingTime;
            //Debug.Log(restTime);
        }
        myPlayer.Return(this.gameObject);
    }
    public void Stop()
    {
        StopAllCoroutines();
        if (TryGetComponent<Rigidbody2D>(out var rb))
        {
            rb.velocity = Vector2.zero;
        }
    }


}