using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AttackState : IState
{
    public void OnEnter(Enemy enemy)
    {
        //Debug.Log("is attack state");
        enemy.StopMoving();
        //Debug.Log("target " + enemy.Target.name);
        //Debug.Log(enemy.ListCheckInRange.Count);
        enemy.SpawBulletPosition.position = enemy.transform.position + new Vector3(0, 1f, 0);

    }

    public void OnExcute(Enemy enemy)
    {
        if (enemy.Target != null)
        {
            enemy.EnemyAttak();
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
