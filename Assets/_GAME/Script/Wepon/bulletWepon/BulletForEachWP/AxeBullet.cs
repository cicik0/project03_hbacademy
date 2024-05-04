using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeBullet : BaseBullet
{
    public override void OnInit(Character attacker, Vector3 setDirector, Transform shootTransform, Action<Character, Character> onHit)
    {
        base.OnInit(attacker, setDirector, shootTransform, onHit);
        this.transform.localRotation = Quaternion.Euler(90, 0, 0);
    }

    public override void Shoot(float throwSpeed)
    {
        base.Shoot(throwSpeed);
        this.transform.Rotate(Vector3.forward * throwSpeed);
    }
}
