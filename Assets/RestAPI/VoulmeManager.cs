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
        // �ʱⰪ ���� (Inspector���� �����̴� Value�� �ٲ�ξ��ٸ� �̰� ���� ����)
        bgmSlider.value = 0.5f;
        sfxSlider.value = 0.5f;

        // �̺�Ʈ ������ ���
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    private void OnDestroy()
    {
        // �޸� ���� ����: RemoveListener
        bgmSlider.onValueChanged.RemoveListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.RemoveListener(OnSFXVolumeChanged);
    }

    // �����̴� ���� �ٲ� ������ ȣ��Ǵ� �ݹ�

    private void OnBGMVolumeChanged(float value)
    {
        AudioManager.Instance.SetBGMVolume(value);
    }

    private void OnSFXVolumeChanged(float value)
    {
        AudioManager.Instance.SetSFXVolume(value);
    }
}
