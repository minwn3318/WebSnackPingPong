using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private bool turningAllow = false;
    [SerializeField] private int count = 0;
    [SerializeField] private GameObject directer;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector2 playerPos;
    [SerializeField] private Vector2 pointerPos;
    [SerializeField] private Vector2 directPos;
    [SerializeField] private ShooterQueue polling;

    // Start is called before the first frame update

    public void Awake()
    {
        polling = GetComponent<ShooterQueue>();
        polling.InitQueue(2);
        directer = transform.GetChild(0).gameObject;
        player = transform.gameObject;
        playerPos = new Vector2(0, 1);
        directer.SetActive(false);
    }
    public void OnLoadShoot()
    {
        count++;
        count %= 2;
        if (count == 1) return;
        turningAllow = !turningAllow;
        if(turningAllow)
        {
            directer.SetActive(true);
            return;
        }
        directer.SetActive(false);
        StartCoroutine(Shooting());
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (turningAllow)
        {
            pointerPos = context.ReadValue<Vector2>();
            directPos = (pointerPos - (Vector2)player.transform.position).normalized;
            if(Vector2.Dot(playerPos, directPos) < 0)
            {
                return;
            }
            player.transform.up = directPos;
        }
    }

    public IEnumerator Shooting()
    {
        while(polling.Size != 0)
        {
            GameObject obj_v = polling.PopQueue();
            ShootingBall ball_v = obj_v.GetComponent<ShootingBall>();
            ball_v.Move(directPos);
            yield return new WaitForSeconds(0.07f);
        }
    }

    public void Return(GameObject obj_v)
    {
        obj_v.transform.position = player.transform.position;
        polling.PollingQueue(obj_v);
    }
}
