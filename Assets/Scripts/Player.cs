using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private bool turningAllow = false; // ȸ���� ��� ����
    [SerializeField] private bool clickAllow = false; // Ŭ�� ��� ����
    [SerializeField] private int ballCount = 5; // ������ �� ����
    [SerializeField] private float gap = 0.05f; // �� �߻� Ÿ�� ����
    [SerializeField] private SpriteRenderer playerRender; // �÷��̾� ��������Ʈ
    [SerializeField] private Vector2 playerPos = new Vector2(0, 1); // �⺻ �߻� ����
    [SerializeField] private GameObject directer; // ȭ��ǥ ������Ʈ
    [SerializeField] private Transform playerTrans; // �÷��̾� Ʈ������
    [SerializeField] private Vector2 pointerPos; // ���콺 ������ ��ġ
    [SerializeField] private Vector2 directPos; // ���� �߻� ����
    [SerializeField] private ShooterQueue polling; // �� Ǯ ����

    private bool isShooting = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        polling = GetComponent<ShooterQueue>();
        directer = transform.GetChild(0).gameObject;
        playerTrans = GetComponent<Transform>();
        playerRender = GetComponent<SpriteRenderer>();
        directer.SetActive(false);
    }

    public void SpawnPlayer()
    {
        playerRender.enabled = true;
        clickAllow = true;
        isShooting = false;
        polling.InitQueue(ballCount);
        GameManager.Instance.SetBallCount(ballCount);
    }

    public void DestoryPlayer()
    {
        polling.RemoveQueue();
        clickAllow = false;
        isShooting = false;
        directer.SetActive(false);
        playerRender.enabled = false;
    }

    public void OnLoadShoot(InputAction.CallbackContext context)
    {
        if (!clickAllow || isShooting) return;

        if (context.performed)
        {
            directer.SetActive(true);
            turningAllow = true;
        }
        else if (context.canceled && turningAllow)
        {
            StartCoroutine(Shooting());
            directer.SetActive(false);
            clickAllow = false;
            turningAllow = false;
        }
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (!turningAllow) return;

        pointerPos = (context.ReadValue<Vector2>());
        pointerPos.x = -(pointerPos.x);  
        pointerPos.x += 500f;
        directPos = ((Vector2)playerTrans.position - pointerPos).normalized;

        playerTrans.up = directPos;
    }

    public IEnumerator Shooting()
    {
        isShooting = true;

        for (int i = 0; i < polling.Capacity; i++)
        {
            AudioManager.Instance.PlayBallLaunch();
            GameObject obj_v = polling.PopQueue();
            if (obj_v == null) continue;

            obj_v.transform.position = playerTrans.position;
            Ball ball_v = obj_v.GetComponent<Ball>();
            ball_v.Move(directPos);
            yield return new WaitForSeconds(gap);
        }

        isShooting = false;
    }

    public void Return(GameObject obj_v)
    {
        obj_v.transform.position = playerTrans.position;
        polling.PollingQueue(obj_v);
        GameManager.Instance.OnBallReturned();

        if (polling.Capacity == polling.Size)
        {
            clickAllow = true;
        }
    }

    public void MoveToNewStage()
    {
        Vector3 newPosition = new Vector3(GameManager.Instance.lastDiamondXPos, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }

    public void DisableInput()
    {
        clickAllow = false;
    }

    public void EnableInput()
    {
        clickAllow = true;
    }
}
