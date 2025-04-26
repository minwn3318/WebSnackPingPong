using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBtn : MonoBehaviour
{
    public BTNType currentType;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;
    public GameObject titleImage;

    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.New:
                StartBtn();
                break;
            case BTNType.Option:
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);

                if (titleImage != null)
                    titleImage.SetActive(false); // 옵션 열 때 이미지 숨기기
                break;

            case BTNType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);

                if (titleImage != null)
                    titleImage.SetActive(true); // 돌아올 때 이미지 다시 보이기
                break;
        }
    }

    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void StartBtn()
    {
        SceneManager.LoadScene("Loading");
    }
}
