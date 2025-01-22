using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [SerializeField] private bool turningAllow = false;
    [SerializeField] private int count = 0;
    [SerializeField] private GameObject directer;
    // Start is called before the first frame update

    public void Awake()
    {
        directer.SetActive(false);
    }
    public void OnLoadShoot()
    {
        count++;
        if (count % 2 == 1) return;
        Debug.Log(count + " : " + turningAllow);
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
            Debug.Log("rotating position");
        }

    }

}
