using JetBrains.Annotations;
using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepon : MonoBehaviour
{
    [SerializeField] public Character character;
    [SerializeField] public EWeponType.WeponType weponType;
    [SerializeField] public int coin;
    [SerializeField] public float throwSpeed;
    [SerializeField] public GameObject wpPrefab;
    [SerializeField] private WeponListSO wplistSO;
    [SerializeField] private BulletListSo bulletListSO;
  
    private BaseBullet chooseBullet;
    private BaseBullet bulletUI;
    private int oneBullet = 0;
    private Vector3 bulletDirector;

    public int OneBullet { get => oneBullet; set => oneBullet = value; }

    public void OnInit(WeponData wpData)
    {
        this.wpPrefab = wpData.weponPrefab;
        this.weponType = wpData.weponType;
        this.coin = wpData.coin;
        this.throwSpeed = wpData.throwSpeed;
        CheckBulletPrefab(this.weponType);
    }


    public void Throw(Character character, Vector3 setDirector, Transform SpawBulletPosition, Action<Character, Character> onHit)
    {
        //Debug.Log("character " + character.transform.position);
        //Debug.Log("check to spaw " + oneBullet);
        if (OneBullet == 0)
        {
            //Debug.Log("spaw bullet ");
            bulletDirector = setDirector;
            bulletUI = LeanPool.Spawn(chooseBullet, SpawBulletPosition);
            bulletUI.OnInit(character,bulletDirector,SpawBulletPosition, onHit);
            //bulletUI.transform.localPosition = SpawBulletPosition.position;
            OneBullet ++;
        }      
        //Debug.Log("character " + character.transform.position);
        //if(bulletUI != null)
        //{
        //    bulletUI.Shoot(character.transform.position, target, throwSpeed);
        //}

    }

    public void CancelBullet()
    {
        bulletUI.OnDeSpaw(bulletUI);
        OneBullet--;
    }


    private void CheckBulletPrefab (EWeponType.WeponType bType)
    {
        foreach(var bullet in bulletListSO.lilstBullet)
        {
            if(bullet.bulletType == bType)
            {
                chooseBullet = bullet.bulletPrefab;
            }
        }
    }  
}
