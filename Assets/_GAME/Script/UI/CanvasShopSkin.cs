using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasShopSkin : UICanvas
{
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button buyButon;
    [SerializeField] private Button headButton;
    [SerializeField] private Button pantsButton;
    [SerializeField] private Button skinButton;
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject[] scrollViews;

    float r = 0, g = 0, b = 0, opBefor = 0.2f, opAfter = 0;

    private void Start()
    {
        HeadButton();
        cancelButton.onClick.AddListener(() => CancelButton());
        buyButon.onClick.AddListener(() => BuyButton());
        headButton.onClick.AddListener(() => HeadButton());
        pantsButton.onClick.AddListener(() => PantsButton());
        skinButton.onClick.AddListener(() => SkinButton());
    }

    private void BuyButton()
    {
        
    }

    private void PantsButton()
    {
        CloseAllScrollView();
        scrollViews[1].gameObject.SetActive(true);
        buttons[1].image.color = new Color(r, g, b, opAfter);
    }

    private void HeadButton()
    {
        CloseAllScrollView();
        scrollViews[0].gameObject.SetActive(true);
        buttons[0].image.color = new Color(r, g, b, opAfter);

    }

    private void SkinButton()
    {
        CloseAllScrollView();
        scrollViews[2].gameObject.SetActive(true);
        buttons[2].image.color = new Color(r, g, b, opAfter);
    }

    private void CancelButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }

    private void CloseAllScrollView()
    {
        for (int i = 0; i < scrollViews.Length; i++)
        {
            scrollViews[i].gameObject.SetActive(false);
            buttons[i].image.color = new Color(r, g, b, opBefor);
        }
    }
}
