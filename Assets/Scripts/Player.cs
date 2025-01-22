using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private bool turningAllow = false;
    [SerializeField] private int count = 0;
    [SerializeField] private GameObject directer;
    [SerializeField] private GameObject myPlayer;
    [SerializeField] private Vector2 pointerPos;

    // Start is called before the first frame update

    public void Awake()
    {
        directer = transform.GetChild(0).gameObject;
        myPlayer = transform.gameObject;
        directer.SetActive(false);
    }
    public void OnLoadShoot()
    {
        count++;
        if (count % 2 == 1) return;
        turningAllow = !turningAllow;
        if(turningAllow)
        {
            directer.SetActive(true);
            return;
        }
        directer.SetActive(false);
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (turningAllow)
        {
            pointerPos = context.ReadValue<Vector2>();
            myPlayer.transform.up = (pointerPos - (Vector2)myPlayer.transform.position).normalized;
        }
    }

}
