using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class WeponData
{
    public EWeponType.WeponType weponType;
    public int coin;
    public float throwSpeed;
    public GameObject weponPrefab; 
}
