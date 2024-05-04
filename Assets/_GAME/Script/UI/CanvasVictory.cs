using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasVictory : UICanvas
{
    [SerializeField] Button mainMenuButton;
    [SerializeField] TextMeshProUGUI scoreText;

    public void SetBestScore(int score)
    {
        scoreText.text = score.ToString(); 
    }

    private void Start()
    {
        mainMenuButton.onClick.AddListener(() => MainMenuButton());
    }

    private void MainMenuButton()
    {
        //UIManager.Instance.CloseAllUI();
        Close(0);
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }
}
