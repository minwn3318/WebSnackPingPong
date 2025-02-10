using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float elapsedTime;
    [SerializeField] private float LimitteTime;

    private void Awake()
    {
        elapsedTime = 0f;
        LimitteTime = 300f;
    }
}
