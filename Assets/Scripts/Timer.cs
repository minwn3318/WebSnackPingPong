using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour // 시간제한 클래스이다
{
    [SerializeField] private float elapsedTime; // 현재시간을 저장한 시간이다
    [SerializeField] private float LimitteTime; // 제한시간이다

    private void Awake()
    {
        elapsedTime = 0f;
        LimitteTime = 300f;
    }
}
