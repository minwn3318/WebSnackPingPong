using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;   // 배경음악용 AudioSource
    [SerializeField] private AudioSource sfxSource;   // 효과음용 AudioSource

    [Header("Audio Clips BGM")]
    [SerializeField] private AudioClip bgmClip;

    [Header("SFX")]
    [SerializeField] private AudioClip ballLaunchClip;    // 공 발사 효과음
    [SerializeField] private AudioClip buttonSelectClip;
    [SerializeField] private AudioClip buttonClickClip;   // 버튼 클릭 효과음
    [SerializeField] private AudioClip blockCountDownClip;
    [SerializeField] private AudioClip blockBreakClip;    // 블럭 깨짐 효과음
    [SerializeField] private AudioClip pointUpClip;
    [SerializeField] private AudioClip StageUpClip;
    [SerializeField] private AudioClip gameOverClip;
    [SerializeField] private AudioClip loadingSceneClip;

    [Header("Volume Settings")]
    [Range(0f, 1f)][SerializeField] private float bgmVolume = 0.5f;
    [Range(0f, 1f)][SerializeField] private float sfxVolume = 0.5f;

    private void Awake()
    {
        // 싱글톤 인스턴스 초기화
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);  // 이미 인스턴스가 있으면 자기 자신을 파괴
            return;
        }

        // AudioSource가 할당되어 있지 않으면 경고 출력
        if (bgmSource == null || sfxSource == null)
        {
            Debug.LogError("[AudioManager] BGM 또는 SFX용 AudioSource가 할당되지 않았습니다.");
        }

        // 초기 볼륨 세팅 적용
        ApplyVolumeSettings();
        PlayBGM();
    }

    public void PlayBGM()
    {
        if (bgmClip == null)
        {
            Debug.LogWarning("[AudioManager] BGM AudioClip이 할당되지 않았습니다.");
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
            Debug.LogWarning("[AudioManager] 재생하려는 SFX AudioClip이 할당되지 않았습니다.");
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
