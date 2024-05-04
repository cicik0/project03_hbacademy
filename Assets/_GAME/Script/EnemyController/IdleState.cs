using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public void OnEnter(Enemy enemy)
    {
        //Debug.Log("is idle state");

        enemy.StopMoving();
        enemy.CanThrow = true;
    }

    public void OnExcute(Enemy enemy)
    {
        if (enemy.Target != null)
        {
            enemy.ChangeState(new AttackState());
        }
        else
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
