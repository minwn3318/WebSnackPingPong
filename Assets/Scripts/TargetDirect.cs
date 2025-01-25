using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDirect : MonoBehaviour
{
    [SerializeField] private GameObject targetPoint;

    public void Awake()
    {
        targetPoint = transform.GetChild(0).gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.contacts[0].point);
    }
}
