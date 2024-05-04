using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] LevelController[] levels;

    public LevelController currentLevel;

    public LevelController[] Levels { get => levels; set => levels = value; }

    private void Start()
    {
        //OnloadLevel(0);
        //OnInit();
    }

    private void OnInit()
    {
        //player.OnInit();
    }

    //spawn prefab level
    public void OnloadLevel(int levelIndex)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(Levels[levelIndex-1]);
    }

    //when despawn prefab level
    public void OnReset()
    {
        //player.OnDespawn();
        //for (int i = 0; i < bots.Count; i++)
        //{
        //    bots[i].OnDespawn();
        //}

        //bots.Clear();
        //SimplePool.CollectAll();
    }
}
