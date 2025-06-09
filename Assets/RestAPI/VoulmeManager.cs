using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoulmeManager : MonoBehaviour
{
    [Header("UI Sliders")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        // 초기값 세팅 (Inspector에서 슬라이더 Value를 바꿔두었다면 이건 생략 가능)
        bgmSlider.value = 0.5f;
        sfxSlider.value = 0.5f;

        // 이벤트 리스너 등록
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    private void OnDestroy()
    {
        // 메모리 누수 방지: RemoveListener
        bgmSlider.onValueChanged.RemoveListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.RemoveListener(OnSFXVolumeChanged);
    }

    // 슬라이더 값이 바뀔 때마다 호출되는 콜백

    private void OnBGMVolumeChanged(float value)
    {
        AudioManager.Instance.SetBGMVolume(value);
    }

    private void OnSFXVolumeChanged(float value)
    {
        AudioManager.Instance.SetSFXVolume(value);
    }
}
