using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSetting : UICanvas   
{
    [SerializeField] Button mainMenuButton;
    [SerializeField] Button continueButton;
    [SerializeField] Button closeButton;
    [SerializeField] GameObject[] buttons;


    public void SetState(UICanvas canvas)
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        if(canvas is CanvasMainMenu)
        {
            buttons[2].gameObject.SetActive(true);

        }
        else if(canvas is CanvasGamePlay)
        {
            buttons[0].gameObject.SetActive(true);
            buttons[1].gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        mainMenuButton.onClick.AddListener(() => MainMenuButton());
        continueButton.onClick.AddListener(() => ContinueButton());
        closeButton.onClick.AddListener(() => CloseButton());
    }

    private void ContinueButton()
    {
        Time.timeScale = 1.0f;
        UIManager.Instance.CloseUI<CanvasSetting>(0);
    }

    private void CloseButton()
    {
        UIManager.Instance.CloseUI<CanvasSetting>(0);
    }

    private void MainMenuButton()
    {
        Time.timeScale = 1.0f;
        UIManager.Instance.CloseAllUI();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }


}
