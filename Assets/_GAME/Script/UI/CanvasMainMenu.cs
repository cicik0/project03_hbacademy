using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainMenu : UICanvas
{
    [SerializeField] Button playButton;
    [SerializeField] Button settingButton;
    [SerializeField] Button shopWeponButton;
    [SerializeField] Button shopSkinButton;
    [SerializeField] Button fontLeveltButton;
    [SerializeField] Button backLevelButton;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI levelText;

    public static int levelIndexInMainMenu = 1; //have one value for choose level

    public void UpdateCoin(int coin)
    {
        coinText.text = coin.ToString();
    }

    private void Start()
    {
        levelText.text += levelIndexInMainMenu.ToString();
        playButton.onClick.AddListener(() => PlayButton());
        settingButton.onClick.AddListener(() => SettingButton());
        shopWeponButton.onClick.AddListener(() => ShopWeponButton());
        shopSkinButton.onClick.AddListener(() => ShopSkinButton());
        fontLeveltButton.onClick.AddListener(() => FontLeveltButton());
        backLevelButton.onClick.AddListener(() => BackLeveltButton());
    }

    private void BackLeveltButton()
    {
        if(levelIndexInMainMenu > 1)
        {
            levelIndexInMainMenu--;
            levelText.text = "LEVEL " + levelIndexInMainMenu.ToString();
        }
    }

    private void FontLeveltButton()
    {
        if (levelIndexInMainMenu < LevelManager.Instance.Levels.Length)
        {
            levelIndexInMainMenu++;
            levelText.text = "LEVEL " + levelIndexInMainMenu.ToString();
        }
    }

    public void PlayButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        GameManager.Instance.UpdateGameState(GameState.GamePlay);
    }

    public void SettingButton()
    {
        UIManager.Instance.OpenUI<CanvasSetting>().SetState(this);
    }

    public void ShopWeponButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasShopWepon>();
    }
    public void ShopSkinButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasShopSkin>();
    }

}
