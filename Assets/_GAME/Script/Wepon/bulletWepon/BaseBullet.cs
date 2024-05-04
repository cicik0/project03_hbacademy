using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [SerializeField] EWeponType.WeponType bulletType;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Rigidbody rb;

    protected Character attacker;
    protected Action<Character , Character > onHit;
    protected Transform shootTransform;
    protected Vector3 setDirector;

    void Update()
    {
        DistanceToDespawn();

        Shoot(attacker.CurrentWepon.throwSpeed);
      
    }

    public virtual void OnInit(Character attacker, Vector3 setDirector, Transform shootTransform, Action<Character , Character> onHit)
    {
        this.attacker = attacker;        
        this.onHit = onHit;
        this.setDirector = setDirector;
        this.shootTransform = shootTransform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
                //Debug.Log("name of pick ");

            Character beAttacked = Cache.GetCharater(other);
            if (CheckAttacker(attacker, beAttacked))
            {
                onHit?.Invoke(attacker, beAttacked);
                //Debug.Log("victim" + beAttacked.name);
            }
        }

    }

    private void DistanceToDespawn()
    {
        if (attacker != null)
        {
            if (Vector3.Distance(shootTransform.position, transform.position) > 7f)
            {
                //Debug.Log("despaw");
                OnDeSpaw(this);
                attacker.CurrentWepon.OneBullet = 0;
                attacker.CurrentWepon.gameObject.SetActive(true);
            }

            if (attacker.IsDead == true)
            {
                Lean.Pool.LeanPool.Despawn(shootTransform);

            }
        }
        
    }

    public virtual void OnDeSpaw(BaseBullet bullet)
    {
        Lean.Pool.LeanPool.Despawn(bullet);
        if (attacker.IsDead == true)
        {
            Lean.Pool.LeanPool.Despawn(shootTransform);
        }

    }

    public virtual void Shoot(float throwSpeed )
    {
        
        rb.velocity = setDirector * throwSpeed;

    }

    private bool CheckAttacker(Character attacker, Character victim)
    {
        if(attacker != victim) return true;
        return false;
    }

}
