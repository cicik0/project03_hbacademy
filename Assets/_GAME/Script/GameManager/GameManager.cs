using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu, GamePlay, Win, Loss, Setting }

public class GameManager : Singleton<GameManager>
{
    private static GameState gameState;

    public static void ChangeState(GameState state)
    {
        gameState = state;
    }

    public static bool IsState(GameState state) => gameState == state;

    private void Awake()
    {
        //tranh viec nguoi choi cham da diem vao man hinh
        Input.multiTouchEnabled = false;
        //target frame rate ve 60 fps
        Application.targetFrameRate = 60;
        //tranh viec tat man hinh
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //xu tai tho
        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }

        LevelController.OnPlayerDie += HandelPlayDie;
        LevelController.OnEnemyDie += HandelEnemyDie;
    }

    private void OnDisable()
    {
        LevelController.OnPlayerDie -= HandelPlayDie;
        LevelController.OnEnemyDie += HandelEnemyDie;
    }

    //when enemy die, plus poit for player
    private void HandelEnemyDie(int point)
    {
        UIManager.Instance.OpenUI<CanvasGamePlay>().UpdatePoint(point);
    }

    //when player die, loss, onpen canvas_fall
    private void HandelPlayDie()
    {
        UIManager.Instance.OpenUI<CanvasFall>();
    }

    private void Start()
    {
        UpdateGameState(GameState.MainMenu);
    }


    public void UpdateGameState(GameState newState)
    {
        gameState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                HandleMainMenu();
                break;
            case GameState.GamePlay:
                HandleGamePlay();
                break;
            case GameState.Win:
                HandleWin();
                break;
            case GameState.Loss:
                HandleLoss();
                break;
            case GameState.Setting:
                HandleSetting();
                break;
        }
    }

    private void HandleSetting()
    {

    }

    private void HandleLoss()
    {
        Debug.Log("game loss");
        UIManager.Instance.CloseUI<CanvasGamePlay>(0);
        HandelPlayDie();
    }

    private void HandleWin()
    {
        Debug.Log("game win");
        UIManager.Instance.OpenUI<CanvasVictory>();
    }

    private void HandleGamePlay()
    {
        Debug.Log("game play");
        LevelManager.Instance.OnloadLevel(CanvasMainMenu.levelIndexInMainMenu);
    }

    private void HandleMainMenu()
    {
        Debug.Log("Main menu");
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }
}
