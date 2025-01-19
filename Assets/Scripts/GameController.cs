using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [SerializeField] private InputAction load, axis, shoot;
    [SerializeField] private bool rotateBool;
    // Start is called before the first frame update
    private void Awake()
    {
        load.Enable();
        axis.Enable();
        shoot.Enable();
        load.performed += _ => { rotateBool = true; };
    }

    private IEnumerator Rotate()
    {
        while(rotateBool)
        {
            yield return null;
        }
    }
}
