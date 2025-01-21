using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [SerializeField] private InputAction loadShoot, rotate;
    [SerializeField] private bool turningAllow;
    // Start is called before the first frame update
    void Awake()
    {
        loadShoot.Enable();
        rotate.Enable();
        loadShoot.performed += _ => { StartCoroutine(TurnDirect()); };
        loadShoot.canceled += _ => { turningAllow = false; };
    }

    private IEnumerator TurnDirect()
    {
        turningAllow = true;
        while(turningAllow)
        {
            Debug.Log("TurnDirect");
            yield return null;
        }
    }
}
