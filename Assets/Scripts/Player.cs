using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private bool turningAllow = false; // 회전을 허용여부
    [SerializeField] private bool clickAllow = false; // 클릭 허용 여부
    [SerializeField] private int ballCount = 5; // 소유할 공 갯수
    [SerializeField] private float gap = 0.05f; // 공 발사 타임 간격
    [SerializeField] private SpriteRenderer playerRender; // 플레이어 보이게하는 변수
    [SerializeField] private Vector2 playerPos = new Vector2(0, 1); // 플레이어가 기본으로 바라보고 있는 방향 벡터
    [SerializeField] private GameObject directer; // 화살표 오브젝트
    [SerializeField] private Transform playerTrans; // 플레이어 트랜스폼
    [SerializeField] private Vector2 pointerPos; // 클릭포인터 방향 벡터
    [SerializeField] private Vector2 directPos; // 화살표 벡터
    [SerializeField] private ShooterQueue polling; // 공을 저장할 풀 (큐)

    public void Awake()
    {
        polling = GetComponent<ShooterQueue>();
        directer = transform.GetChild(0).gameObject;
        playerTrans = GetComponent<Transform>();
        playerRender = GetComponent<SpriteRenderer>();
        directer.SetActive(false);
        //playerRender.enabled = false;
    }

    public void SpawnPlayer() // 게임시작 후 플레이어를 보이게하기
    {
        playerRender.enabled = true;
        //directer.SetActive(true);
        clickAllow = true;
        polling.InitQueue(ballCount);
    }

    public void DestoryPlayer() // 게임오버후 플레이어를 보이지 않게 하기
    {
        polling.RemoveQueue();
        clickAllow = false;
        directer.SetActive(false);
        playerRender.enabled = false;
    }

    public void OnLoadShoot(InputAction.CallbackContext context) // 클릭 후 공을 발사한다 공이 다시 되돌아오기 전에는 클릭이 허용되지 않는다
    {
        if (!clickAllow) return;
        if (context.performed) // 눌러져있을 때 회전을 허락한다
        {
            directer.SetActive(true);
            turningAllow = true;
        }
        else if (context.canceled && turningAllow) // 눌림이 취소되고 회전이 허락되어있으면 공을 발사한다
        {
            StartCoroutine(Shooting());
            directer.SetActive(false);
            clickAllow = false;
            turningAllow = false;
        }
    }

    public void OnRotate(InputAction.CallbackContext context) // 플레이어가 회전한다
    {
        if (!turningAllow) return;
        pointerPos = context.ReadValue<Vector2>();
        directPos = -(pointerPos - (Vector2)playerTrans.position).normalized;
        if (Vector2.Dot(playerPos, directPos) < 0)
        {
            return;
        }
        playerTrans.up = directPos;
    }

    public IEnumerator Shooting() // 공을 다 소진할 때까지 전부 발사한다
    {
        for(int i = 0; i < polling.Capacity; i++)
        {
            GameObject obj_v = polling.PopQueue();
            obj_v.transform.position = playerTrans.position;
            Ball ball_v = obj_v.GetComponent<Ball>();
            ball_v.Move(directPos);
            yield return new WaitForSeconds(gap);
        }
    }

    public void Return(GameObject obj_v) // 공을 다시 반환하며 공을 전부 반환했으면 클릭이 가능해진다
    {
        obj_v.transform.position = playerTrans.position;
        polling.PollingQueue(obj_v);
        if(polling.Capacity == polling.Size) clickAllow = true;
    }
}
