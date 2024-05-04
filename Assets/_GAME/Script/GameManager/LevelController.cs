using Lean.Pool;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class LevelController : MonoBehaviour
{
    [SerializeField] private List<Character> listCharacter = new List<Character>();
    [SerializeField] private MeshCollider spawArea;
    [SerializeField] private Transform spawBulletPosition;
    [SerializeField] private int numEnemy = 3;
    [SerializeField] private Canvas inputCanva;
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private CameraFollow cameraPlayer;
    [SerializeField] private int pointToWin;

    private List<Transform> listSpawBullet = new List<Transform>();
    private List<Character> listEnemy = new List<Character> ();
    private Character character;
    private Character characterToSpaw;
    private Transform spawBullet;
    private int pointOfPlayer = 0;

    public static Action OnPlayerDie;
    public static Action<int> OnEnemyDie;

    private void OnEnable()
    {
        Character.OnLevelDespaw += HandelOnLevelDespaw;
        OnInit();
    }

    private void OnDisable()
    {
        Character.OnLevelDespaw -= HandelOnLevelDespaw;
    }

    // Update is called once per frame
    void Update()
    {
        if (listEnemy.Count < numEnemy)
        {
            //SpawCharacter(Constant.ENEMY_TEAM);
        }

        //DeSpawCharacter();

        //RemoveEnemy(listEnemy);
    }

    private void OnInit()
    {
        SpawCharacter(Constant.PLAYER_TEAM);
        for (int i = 0; i < numEnemy; i++)
        {
            SpawCharacter(Constant.ENEMY_TEAM);
        }
    }

    //tao vi tri ngau nhien cua enemy   
    private Vector3 RandomPositon()
    {
        Bounds bounds = spawArea.bounds;
        float x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float z = UnityEngine.Random.Range(bounds.min.z, bounds.max.z);
        float y = transform.position.y + 2.5f;
        Vector3 rdPosition = new Vector3(x, y ,z);
        return rdPosition;
    }

    private void SpawCharacter(string characterTeam)
    {
        foreach(Character a in listCharacter)
        {
            if (a.CharacterTeam == characterTeam)
            {
                characterToSpaw = a;
                //Debug.Log(characterToSpaw);
                if (characterTeam == Constant.ENEMY_TEAM)
                {
                    //Debug.Log("spaw enemy " + listEnemy.Count);

                    character = LeanPool.Spawn(characterToSpaw, transform) as Enemy;
                    character.transform.localPosition = RandomPositon();
                    listEnemy.Add(character);

                    spawBullet = LeanPool.Spawn(spawBulletPosition, this.transform);
                    listSpawBullet.Add(spawBullet);
                    character.SpawBulletPosition = spawBullet;
                }
                else if (characterTeam == Constant.PLAYER_TEAM)
                {
                    character = LeanPool.Spawn(characterToSpaw, transform) as Player;
                    cameraPlayer = CameraManager.instance.cameraFollow;
                    if (cameraPlayer != null)
                    {
                        cameraPlayer.Target = character.transform;
                    }
                    else
                    {
                        Debug.Log("don's have script for camera");
                    }
                    inputCanva.gameObject.SetActive(true);
                    (character as Player).SetJoystick(inputCanva, joystick);
                    listEnemy.Add(character);//add player

                    spawBullet = LeanPool.Spawn(spawBulletPosition, this.transform);
                    listSpawBullet.Add(spawBullet);
                    character.SpawBulletPosition = spawBullet;
                }
            }
        }       
    }

    //private void DeSpawCharacter()
    //{
    //    //Debug.Log(listEnemy.Count);
    //    foreach (var enemy in listEnemy)
    //    {
    //        if(enemy.IsDead == true)
    //        {
    //            enemy.OnDeSpaw(enemy, OnLevelDespaw);
    //        }
    //    }
    //}

    private void HandelOnLevelDespaw(Character character)
    {
        character.IsDead = false;
        LeanPool.Despawn(character);
        //SpawCharacter(Constant.ENEMY_TEAM);
        if(character.CharacterTeam == Constant.PLAYER_TEAM)
        {
            inputCanva.gameObject.SetActive(false);
            GameManager.Instance.UpdateGameState(GameState.Loss);
            OnPlayerDie?.Invoke();
        }

        if (character.CharacterTeam == Constant.ENEMY_TEAM)
        {
            SpawCharacter(Constant.ENEMY_TEAM);

            if(pointOfPlayer < pointToWin)
            {
                pointOfPlayer++;
                OnEnemyDie?.Invoke(pointOfPlayer);
            }
            else
            {
                GameManager.Instance.UpdateGameState(GameState.Win);
            }
        }
    }
}
