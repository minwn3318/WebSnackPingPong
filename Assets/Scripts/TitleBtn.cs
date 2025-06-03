using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum BTNType { New, Option, Back }

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

    private string currentUserId;

    void Start()
    {
        loginPanel.SetActive(false);
        confirmPanel.SetActive(false);
        SetCanvasGroup(optionGroup, false);
        SetCanvasGroup(mainGroup, true);
    }

    public void OnOptionButton()
    {
        Debug.Log("옵션 버튼 클릭");
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
        Debug.Log("시작 버튼 클릭 → 로그인 패널 열기");
        SetCanvasGroup(mainGroup, false);
        loginPanel.SetActive(true);
    }

    public void OnBackButton()
    {
        Debug.Log("옵션 메뉴 닫기");
        SetCanvasGroup(optionGroup, false);

        if (titleImage != null)
            titleImage.SetActive(true);
        if (startButton != null)
            startButton.SetActive(true);
    }

    public void OnLoginButton()
    {
        string inputId = idInputField.text.Trim();

        if (string.IsNullOrEmpty(inputId))
        {
            Debug.Log("아이디를 입력하세요.");
            return;
        }

        if (CheckUserInDatabase(inputId))
        {
            currentUserId = inputId;
            confirmText.text = $"Is '{currentUserId}' you?";
            loginPanel.SetActive(false);
            confirmPanel.SetActive(true);
        }
        else
        {
            Debug.Log("존재하지 않는 유저입니다.");
        }
    }

    public void OnConfirmYes()
    {
        Debug.Log("유저 확인 완료 → 로딩 화면으로 이동");
        SceneManager.LoadScene("Loading");
    }

    public void OnConfirmNo()
    {
        Debug.Log("유저 확인 취소 → 로그인 패널로 돌아감");
        confirmPanel.SetActive(false);
        loginPanel.SetActive(true);
    }

    public void OnNewAccountButton()
    {
        string inputId = idInputField.text.Trim();

        if (string.IsNullOrEmpty(inputId))
        {
            Debug.Log("아이디를 입력하세요.");
            return;
        }

        Debug.Log("새 계정 생성 요청: " + inputId);

        // [데이터베이스 부분 시작]
        // 여기에 데이터베이스에 새 계정을 생성하는 코드가 들어가야 함
        // [데이터베이스 부분 끝]

        Debug.Log("새 계정 생성 완료 → 로딩 화면으로 이동");
        SceneManager.LoadScene("Loading");
    }

    private bool CheckUserInDatabase(string userId)
    {
        return userId.ToLower() == "user1" || userId.ToLower() == "user2";
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (loginPanel.activeSelf)
            {
                loginPanel.SetActive(false);
                SetCanvasGroup(mainGroup, true);
                if (titleImage != null)
                    titleImage.SetActive(true);
                if (startButton != null)
                    startButton.SetActive(true);
            }
            else if (confirmPanel.activeSelf)
            {
                confirmPanel.SetActive(false);
                loginPanel.SetActive(true);
            }
            else
            {
                Debug.Log("앱 종료 처리 추가 가능");
            }
        }
    }
}
