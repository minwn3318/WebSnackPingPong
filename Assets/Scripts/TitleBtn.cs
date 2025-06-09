using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Text;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;
using UnityEngine.Windows;

//public enum BTNType { New, Option, Back }
[Serializable]
public class UserIdDTO
{
    public string game_id;
    public string message;
}
public class TitleBtn : MonoBehaviour
{
    [Header("Main Panels")]
    public CanvasGroup mainGroup;         // 메인 UI 그룹
    public GameObject loginPanel;         // 로그인 패널
    public GameObject confirmPanel;       // 유저 확인 패널

    [Header("Option Menu")]
    public CanvasGroup optionGroup;       // 옵션 메뉴 그룹

    [Header("UI Elements")]
    public GameObject startButton;
    public TMP_InputField idInputField;
    public TMP_Text confirmText;
    public GameObject titleImage;

    [Header("save message")]
    private string currentUserId;

    [Header("Message")]
    [SerializeField] private string message;

    void Start()
    {
        loginPanel.SetActive(false);
        confirmPanel.SetActive(false);
        SetCanvasGroup(optionGroup, false);
        SetCanvasGroup(mainGroup, true);
    }

    public void OnOptionButton()
    {
        AudioManager.Instance.PlaybuttonClickClip();
        SetCanvasGroup(optionGroup, true);
        loginPanel.SetActive(false);
        confirmPanel.SetActive(false);

        if (titleImage != null)
            titleImage.SetActive(false);
        if (startButton != null)
            startButton.SetActive(false);
    }

    public void OnStartButton()
    {
        AudioManager.Instance.PlaybuttonSelectClip();
        SetCanvasGroup(mainGroup, false);
        loginPanel.SetActive(true);
    }

    public void OnBackButton()
    {
        AudioManager.Instance.PlaybuttonClickClip();
        SetCanvasGroup(optionGroup, false);

        if (titleImage != null)
            titleImage.SetActive(true);
        if (startButton != null)
            startButton.SetActive(true);
    }

    public void OnLoginButton()
    {
        AudioManager.Instance.PlaybuttonSelectClip();
        string inputId = idInputField.text.Trim();

        if (string.IsNullOrEmpty(inputId))
        {
            Debug.Log("아이디를 입력하세요.");
            return;
        }

        currentUserId = inputId;
        confirmText.text = $"Is '{currentUserId}' you?";
        loginPanel.SetActive(false);
        confirmPanel.SetActive(true); 

        PlayerPrefs.DeleteAll();
        UnityWebRequest.ClearCookieCache();
    }

    public void OnConfirmYes()
    {
        StartCoroutine(UserLogin(currentUserId));
        AudioManager.Instance.PlaybuttonSelectClip();
        StartCoroutine(Waitting(1f));
    }

    public void OnConfirmNo()
    {
        AudioManager.Instance.PlaybuttonClickClip();
        currentUserId = "";
        confirmPanel.SetActive(false);
        loginPanel.SetActive(true);
    }

    public void OnNewAccountButton()
    {
        AudioManager.Instance.PlaybuttonSelectClip();
        string inputId = idInputField.text.Trim();

        if (string.IsNullOrEmpty(inputId))
        {
            Debug.Log("아이디를 입력하세요.");
            return;
        }
        UnityWebRequest.ClearCookieCache();
        StartCoroutine(UserJoin(inputId));
        UnityWebRequest.ClearCookieCache();
        StartCoroutine(UserLogin(inputId));
        StartCoroutine(Waitting(2f));
    }

    private void SetCanvasGroup(CanvasGroup cg, bool state)
    {
        if (cg == null) return;
        cg.alpha = state ? 1 : 0;
        cg.interactable = state;
        cg.blocksRaycasts = state;
    }

    void Update()
    {
        // 안드로이드 뒤로가기(ESC) 감지
        if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
        {
            if (loginPanel.activeSelf)
            {
                AudioManager.Instance.PlaybuttonClickClip();
                loginPanel.SetActive(false);
                SetCanvasGroup(mainGroup, true);
                if (titleImage != null)
                    titleImage.SetActive(true);
                if (startButton != null)
                    startButton.SetActive(true);
            }
            else if (confirmPanel.activeSelf)
            {
                AudioManager.Instance.PlaybuttonClickClip();
                confirmPanel.SetActive(false);
                loginPanel.SetActive(true);
            }
            else
            {
                Application.Quit();
            }
        }
    }
    IEnumerator Waitting(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Loading");
    }

    IEnumerator UserJoin(string userid)
    {
        string url = "http://113.198.229.158:1435/shooting-miner/userids/join";
        // 1) JSON 페이로드 준비
        var payload = new UserIdDTO { game_id = userid };
        string jsonData = JsonUtility.ToJson(payload);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        // 2) UnityWebRequest 생성 & 헤더/타임아웃 설정
        UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST)
        {
            uploadHandler = new UploadHandlerRaw(bodyRaw),
            downloadHandler = new DownloadHandlerBuffer(),
            timeout = 10
        };
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");

        // 3) 네트워크 요청 — yield은 오직 이 부분에만!
        yield return request.SendWebRequest();

        // 4) 결과 처리 및 예외 안전망
        try
        {
            string responseText = request.downloadHandler.text;
            payload = JsonUtility.FromJson<UserIdDTO>(responseText);
            message = payload.message;
        }
        catch (Exception ex)
        {
            // JSON 파싱이나 내부 로직 에러까지 안전하게 잡아냄
            Debug.LogError($"UserJoin Exception: {ex.GetType().Name} – {ex.Message}");
            message = payload.message + " : " + ex.ToString();
        }
        finally
        {
            // 꼭 Dispose() 해 줘야 핸들 누수 방지
            request.Dispose();
        }
    }

    IEnumerator UserLogin(string userid)
    {
        string url = "http://113.198.229.158:1435/shooting-miner/userids/login";

        UserIdDTO payload = new UserIdDTO { game_id = userid };
        string jsonData = JsonUtility.ToJson(payload);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        // 2) UnityWebRequest 생성 & 헤더/타임아웃 설정
        UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST)
        {
            uploadHandler = new UploadHandlerRaw(bodyRaw),
            downloadHandler = new DownloadHandlerBuffer(),
            timeout = 10
        };
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");

        // 3) 네트워크 요청 — yield은 오직 이 부분에만!
        yield return request.SendWebRequest();

        // 에러 체
        try
        {
            string responseText = request.downloadHandler.text;
            payload = JsonUtility.FromJson<UserIdDTO>(responseText);

            string setCookie = request.GetResponseHeader("Set-Cookie");
            if (!string.IsNullOrEmpty(setCookie))
            {
                string jsession = ParseAndSaveCookie(setCookie, "JSESSIONID");
                if (!string.IsNullOrEmpty(jsession))
                {
                    PlayerPrefs.SetString("JSESSIONID", jsession);
                    PlayerPrefs.SetString("nickname", userid);
                    PlayerPrefs.Save();
                }
            }
            message = payload.message;
        }
        catch (Exception ex)
        {
            // JSON 파싱이나 내부 로직 에러까지 안전하게 잡아냄
            Debug.LogError($"UserJoin Exception: {ex.GetType().Name} – {ex.Message}");
            message = payload.message + " : " + ex.ToString();
        }
        finally
        {
            // 꼭 Dispose() 해 줘야 핸들 누수 방지
            request.Dispose();
        }
    }

    public string ParseAndSaveCookie(string setCookieHeader, string cookieName)
    {
        string[] parts = setCookieHeader.Split(';');
        foreach (var part in parts)
        {
            var kv = part.Trim().Split('=');
            if (kv.Length == 2 && kv[0] == cookieName)
            {
                return kv[1];
            }
        }
        return null;
    }
}
