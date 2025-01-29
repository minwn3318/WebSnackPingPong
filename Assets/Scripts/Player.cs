using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private bool turningAllow = false;
    [SerializeField] private bool clickAllow = true;
    [SerializeField] private int count = 0;
    [SerializeField] private int ballCount = 5;
    [SerializeField] private float gap = 0.05f;
    [SerializeField] private Vector2 playerPos = new Vector2(0, 1);
    [SerializeField] private GameObject directer;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector2 pointerPos;
    [SerializeField] private Vector2 directPos;
    [SerializeField] private ShooterQueue polling;

    // Start is called before the first frame update

    public void Awake()
    {
        polling = GetComponent<ShooterQueue>();
        polling.InitQueue(ballCount);
        directer = transform.GetChild(0).gameObject;
        player = transform.gameObject;
        directer.SetActive(false);
    }
    public void OnLoadShoot()
    {
        Debug.Log(clickAllow);
        if (!clickAllow)
        {
            Debug.Log("not click allow");
            return;
        }
        count++;
        count %= 2;
        if (count == 1) return;
        if (polling.Capacity < polling.Size) return;
        turningAllow = !turningAllow;
        if(turningAllow)
        {
            directer.SetActive(true);
            return;
        }
        directer.SetActive(false);
        StartCoroutine(Shooting());
        clickAllow = false;
        turningAllow = false;
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (!turningAllow) return;
        pointerPos = context.ReadValue<Vector2>();
        directPos = (pointerPos - (Vector2)player.transform.position).normalized;
        if (Vector2.Dot(playerPos, directPos) < 0)
        {
            return;
        }
        player.transform.up = directPos;
    }

    public IEnumerator Shooting()
    {
        while(polling.Capacity != 0)
        {
            GameObject obj_v = polling.PopQueue();
            obj_v.transform.position = player.transform.position;
            Ball ball_v = obj_v.GetComponent<Ball>();
            ball_v.Move(directPos);
            yield return new WaitForSeconds(gap);
        }
    }

    public void Return(GameObject obj_v)
    {
        obj_v.transform.position = player.transform.position;
        polling.PollingQueue(obj_v);
        if(polling.Capacity == polling.Size) clickAllow = true;
    }
}
