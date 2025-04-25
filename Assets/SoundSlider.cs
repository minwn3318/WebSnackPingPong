//using UnityEngine;
//using UnityEngine.UI;


//public class AudioManager : MonoBehaviour
//{
//    public static AudioManager Instance;

//    public AudioSource bgmSource;
//    public AudioSource seSource;

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    public void SetBGMVolume(float volume)
//    {
//        bgmSource.volume = volume;
//    }

//    public void SetSEVolume(float volume)
//    {
//        seSource.volume = volume;
//    }
//}


//public class SoundSlider : MonoBehaviour
//{
//    public Slider bgmSlider;
//    public Slider seSlider;

//    private const string BGM_KEY = "BGM_VOLUME";
//    private const string SE_KEY = "SE_VOLUME";

//    void Start()
//    {
//        // 저장된 볼륨 값 불러오기 (없으면 기본값 1.0f)
//        float savedBGM = PlayerPrefs.GetFloat(BGM_KEY, 1f);
//        float savedSE = PlayerPrefs.GetFloat(SE_KEY, 1f);

//        // 슬라이더 초기값 설정
//        bgmSlider.value = savedBGM;
//        seSlider.value = savedSE;

//        // 실제 오디오 볼륨도 반영
//        AudioManager.Instance.SetBGMVolume(savedBGM);
//        AudioManager.Instance.SetSEVolume(savedSE);

//        // 슬라이더 변경 시 호출될 함수 연결
//        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
//        seSlider.onValueChanged.AddListener(OnSEVolumeChanged);
//    }

//    public void OnBGMVolumeChanged(float value)
//    {
//        AudioManager.Instance.SetBGMVolume(value);
//        PlayerPrefs.SetFloat(BGM_KEY, value); // 저장
//    }

//    public void OnSEVolumeChanged(float value)
//    {
//        AudioManager.Instance.SetSEVolume(value);
//        PlayerPrefs.SetFloat(SE_KEY, value); // 저장
//    }
//}

//**** 메모*****
//사운드 추가하면서 코드 수정할 예정. 지금은 슬라이더 ui작동만 확인용