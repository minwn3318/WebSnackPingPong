using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;   // ������ǿ� AudioSource
    [SerializeField] private AudioSource sfxSource;   // ȿ������ AudioSource

    [Header("Audio Clips BGM")]
    [SerializeField] private AudioClip bgmClip;

    [Header("SFX")]
    [SerializeField] private AudioClip ballLaunchClip;    // �� �߻� ȿ����
    [SerializeField] private AudioClip buttonSelectClip;
    [SerializeField] private AudioClip buttonClickClip;   // ��ư Ŭ�� ȿ����
    [SerializeField] private AudioClip blockCountDownClip;
    [SerializeField] private AudioClip blockBreakClip;    // �� ���� ȿ����
    [SerializeField] private AudioClip pointUpClip;
    [SerializeField] private AudioClip StageUpClip;
    [SerializeField] private AudioClip gameOverClip;
    [SerializeField] private AudioClip loadingSceneClip;

    [Header("Volume Settings")]
    [Range(0f, 1f)][SerializeField] private float bgmVolume = 0.5f;
    [Range(0f, 1f)][SerializeField] private float sfxVolume = 0.5f;

    private void Awake()
    {
        // �̱��� �ν��Ͻ� �ʱ�ȭ
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject);  // �̹� �ν��Ͻ��� ������ �ڱ� �ڽ��� �ı�
            return;
        }

        // AudioSource�� �Ҵ�Ǿ� ���� ������ ��� ���
        if (bgmSource == null || sfxSource == null)
        {
            Debug.LogError("[AudioManager] BGM �Ǵ� SFX�� AudioSource�� �Ҵ���� �ʾҽ��ϴ�.");
        }

        // �ʱ� ���� ���� ����
        ApplyVolumeSettings();
        PlayBGM();
    }

    public void PlayBGM()
    {
        if (bgmClip == null)
        {
            Debug.LogWarning("[AudioManager] BGM AudioClip�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("[AudioManager] ����Ϸ��� SFX AudioClip�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        ApplyVolumeSettings();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        ApplyVolumeSettings();
    }

    private void ApplyVolumeSettings()
    {
        if (bgmSource != null)
            bgmSource.volume = bgmVolume;
        if (sfxSource != null)
            sfxSource.volume = sfxVolume;
    }
    public void PlayBallLaunch()
    {
        PlaySFX(ballLaunchClip);
    }
    public void PlaybuttonSelectClip()
    {
        PlaySFX(buttonSelectClip);
    }
    public void PlaybuttonClickClip()
    {
        PlaySFX(buttonClickClip);
    }

    public void PlayblockCountDownClip()
    {
        PlaySFX(blockCountDownClip);
    }

    public void PlayblockBreakClip()
    {
        PlaySFX(blockBreakClip);
    }

    public void PlaypointUpClip()
    {
        PlaySFX(pointUpClip);
    }

    public void PlayStageUpClip()
    {
        PlaySFX(StageUpClip);
    }

    public void PlaygameOverClip()
    {
        PlaySFX(gameOverClip);
    }

    public void PlayloadingSceneClip()
    {
        PlaySFX(loadingSceneClip);
    }
   
}
