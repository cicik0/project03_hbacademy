using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFall : UICanvas
{
    [SerializeField] Button mainMenuButton;
    [SerializeField] Button playAgainButton;
    [SerializeField] TextMeshProUGUI scoreText;

    public void SetBestScore(int score)
    {
        scoreText.text = score.ToString();
    }

    private void Start()
    {
        mainMenuButton.onClick.AddListener(() => MainMenuButton());
        playAgainButton.onClick.AddListener(() => PlayAgainButton());
    }

    private void PlayAgainButton()
    {
        GameManager.Instance.UpdateGameState(GameState.GamePlay);
        Close(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>().ResetPoint();
    }

    private void MainMenuButton()
    {
        //UIManager.Instance.CloseAllUI();
        Close(0);
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }
}
