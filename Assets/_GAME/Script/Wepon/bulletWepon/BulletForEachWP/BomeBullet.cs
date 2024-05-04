using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomeBullet : BaseBullet
{
    public override void OnInit(Character attacker, Vector3 setDirector, Transform shootTransform, Action<Character, Character> onHit)
    {
        base.OnInit(attacker, setDirector,shootTransform, onHit);
    }

}
