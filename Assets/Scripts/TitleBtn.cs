using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//public enum BTNType { New, Option, Back }

public class TitleBtn : MonoBehaviour
{
    [Header("Main Panels")]
    public CanvasGroup mainGroup;         // ���� UI �׷�
    public GameObject loginPanel;         // �α��� �г�
    public GameObject confirmPanel;       // ���� Ȯ�� �г�

    [Header("Option Menu")]
    public CanvasGroup optionGroup;       // �ɼ� �޴� �׷�

    [Header("UI Elements")]
    public GameObject startButton;
    public TMP_InputField idInputField;
    public TMP_Text confirmText;
    public GameObject titleImage;

    [Header("JoinLogin")]
    [SerializeField] private PostLoginJoinAPIFront loginJoin;
    [Header("save message")]
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
        AudioManager.Instance.PlaybuttonClickClip();
        Debug.Log("�ɼ� ��ư Ŭ��");
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
        Debug.Log("���� ��ư Ŭ�� �� �α��� �г� ����");
        SetCanvasGroup(mainGroup, false);
        loginPanel.SetActive(true);
    }

    public void OnBackButton()
    {
        AudioManager.Instance.PlaybuttonClickClip();
        Debug.Log("�ɼ� �޴� �ݱ�");
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
            Debug.Log("���̵� �Է��ϼ���.");
            return;
        }
        currentUserId = inputId;
        confirmText.text = $"Is '{currentUserId}' you?";
        loginPanel.SetActive(false);
        confirmPanel.SetActive(true);
    }

    public void OnConfirmYes()
    {
        AudioManager.Instance.PlaybuttonSelectClip();
        Debug.Log("���� Ȯ�� �Ϸ� �� �ε� ȭ������ �̵�");
        SceneManager.LoadScene("Loading");
    }

    public void OnConfirmNo()
    {
        AudioManager.Instance.PlaybuttonClickClip();
        Debug.Log("���� Ȯ�� ��� �� �α��� �гη� ���ư�");
        confirmPanel.SetActive(false);
        loginPanel.SetActive(true);
    }

    public void OnNewAccountButton()
    {
        AudioManager.Instance.PlaybuttonSelectClip();
        string inputId = idInputField.text.Trim();

        if (string.IsNullOrEmpty(inputId))
        {
            Debug.Log("���̵� �Է��ϼ���.");
            return;
        }

        Debug.Log("�� ���� ���� ��û: " + inputId);

        // [�����ͺ��̽� �κ� ����]
        // ���⿡ �����ͺ��̽��� �� ������ �����ϴ� �ڵ尡 ���� ��
        // [�����ͺ��̽� �κ� ��]

        Debug.Log("�� ���� ���� �Ϸ� �� �ε� ȭ������ �̵�");
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
        // �ȵ���̵� �ڷΰ���(ESC) ����
        if (Input.GetKeyDown(KeyCode.Escape))
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
                Debug.Log("�� ���� ó�� �߰� ����");
            }
        }
    }
}
