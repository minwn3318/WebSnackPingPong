using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour // �� ������Ʈ�̴�
{
    [SerializeField] private Rigidbody2D ballRB; //�� Rigidbody
    [SerializeField] private float speed = 35f; // �⺻ �ӷ�
    [SerializeField] private float speedLose = 25f; // �ּ� �ӷ�
    [SerializeField] private bool comeBackHome = false; // �� �߻� ��, ���� �ǵ��ƿ��� �� �� �ִ� ���¿���
    [SerializeField] private float forceReturnTime = 3f; // ���� ������ ���ƿ��� �ϴµ� ��ٸ��� �ð�
    [SerializeField] private Vector2 lastVelocity; // ������ �ӵ�
    [SerializeField] private Player myPlayer; // ���� �����ϴ� �÷��̾� ��ũ��Ʈ

    void Awake() // ���� ���۽� �� �ʱ�ȭ
    {
        ballRB = GetComponent<Rigidbody2D>();
        myPlayer = GameObject.FindAnyObjectByType<Player>();
        lastVelocity = Vector2.zero;
    }

    private void FixedUpdate() // �������� ������Ʈ, �߻� �� �� ���� �������� ������Ʈ ��Ų��
    {
        if (ballRB.velocity.magnitude < speedLose) // �ӷ��� �ּ� �ӷº��� �������� �⺻ �ӷ����� ����� �ӷ��� �������� ��̰� ���ϰ� �ݰ��ȴ�
        {
            float gapManitude = speed - ballRB.velocity.magnitude;
            Vector2 modifyVector =
                -((lastVelocity.normalized * gapManitude * (speed / speedLose))
                + ballRB.velocity);
            ballRB.velocity = modifyVector;
        }
        if (ballRB.velocity == -lastVelocity) //���� ���� �ӵ��� ������ �ӵ��� (-)������ ���ٸ�, xy�������� �����ش� ���� ���� �ڸ��� �ɵ��°� �����ش�
        {
            ballRB.velocity = new Vector2(ballRB.velocity.y, ballRB.velocity.x);
        }
    }

    public float Speed //�ӷ°��� ���ϰų� ������ �ӷ��� �����´�
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
    public void Move(Vector2 velocity_v) //���� �߻��Ѵ�
    {
        ballRB.velocity = velocity_v * speed;
        lastVelocity = ballRB.velocity;
        StartCoroutine(ForceReturn());
    }

    private void OnCollisionEnter2D(Collision2D collision) //���� ��ü�� �ε����� �� �����ϴ� �Լ���
    {
        if (collision.collider.CompareTag("Wall")) // ���� �Ʒ� �ٴڰ� �ε����� �ݵ�� �ٽ� �÷��̾�� ���ƿ´�
        {
            lastVelocity = Vector2.zero;
            ballRB.velocity = lastVelocity;
            comeBackHome = false;
            myPlayer.Return(this.gameObject);
            //return;
        }
        
        //�Ʒ����� ���� �ݻ�Ǵ� ���� ������ �ڵ��. �׻� �ٲ���� �ӵ��� �����صд�
        ballRB.velocity =
            Mathf.Max(speed, 0f) *
            Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        lastVelocity = ballRB.velocity;

        if (collision.collider.CompareTag("Diamond")) // ���̾Ƹ�忡 �ε����� ���� �߰�
        {
            GameManager.Instance.AddScore(100); // ���� 100�� �߰�
            GameManager.Instance.IncreaseStage();
            GameObject diamond = collision.gameObject;
            GameManager.Instance.OnDiamondHit(); // ���� ó��
            Destroy(diamond); // ���߿� �ı�
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // ���� �÷��̾�� �浹���� �� �ݵ�� ���ƿ´�
    {
        if (!comeBackHome) return;
        lastVelocity = Vector2.zero;
        ballRB.velocity = lastVelocity;
        comeBackHome = false;
        myPlayer.Return(this.gameObject);
        return;
    }

    private IEnumerator ForceReturn() // ���� ���ѵǴµ� �ɸ��� �ð��� ���� �� �ݵ�� ��ȯ�ȴ�
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