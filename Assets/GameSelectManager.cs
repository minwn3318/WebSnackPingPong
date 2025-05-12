using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSelectManager : MonoBehaviour
{
    public TextMeshProUGUI nicknameText;
    public Button singlePlayButton;
    public Button backButton;

    void Start()
    {
        Time.timeScale = 1f;
        nicknameText.text = PlayerPrefs.GetString("nickname");

        singlePlayButton.onClick.AddListener(() => {
            SceneManager.LoadScene("MainTitle");
        });

        backButton.onClick.AddListener(() => {
            SceneManager.LoadScene("TitleScene");
        });
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
