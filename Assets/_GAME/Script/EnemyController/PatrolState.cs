using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{

    public void OnEnter(Enemy enemy)
    {
        //Debug.Log("is patrol state");
        enemy.CanThrow = false;
        enemy.IsMove = true;
    }

    public void OnExcute(Enemy enemy)
    {
        if (enemy.Target != null && Vector3.Distance(enemy.transform.position, enemy.Point) <= 0.001)
        {
            enemy.ChangeState(new IdleState());
        }
        else
        {
            enemy.EnemyMove();
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}
