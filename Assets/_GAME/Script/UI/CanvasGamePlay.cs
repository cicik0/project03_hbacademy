using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] private Button settingButton;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI pointText;

    private void Start()
    {
        settingButton.onClick.AddListener(() => SettingButton());
    }

    public void UpdatePoint(int point)
    {
        pointText.text = point.ToString();
    }

    public void ResetPoint()
    {
        pointText.text = "0";
    }

    public void UpdateCoin(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void SettingButton()
    {
        Time.timeScale = 0;
        UIManager.Instance.OpenUI<CanvasSetting>().SetState(this);
    }

}
