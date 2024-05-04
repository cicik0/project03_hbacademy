using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float radius;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private Rigidbody rb;

    private IState currentState;
    private bool canThrow;
    private bool isMove;
    private Vector3 point;

    public bool CanThrow { get => canThrow; set => canThrow = value; }
    public bool IsMove { get => isMove; set => isMove = value; }
    public NavMeshAgent Agent { get => agent; set => agent = value; }
    public Vector3 Point { get => point; set => point = value; }

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTargetInRange();
        if(currentState != null)
        {
            currentState.OnExcute(this);
        }
        //Debug.Log("list " + ListCheckInRange.Count);
        //Debug.Log("target " + Target);
    }

    public override void OnInit()
    {
        base.OnInit();
        CharacterTeam = Constant.ENEMY_TEAM;
        ChangeState(new IdleState());
        CreateWepon(CharacterWpType);
    }

    public void ChangeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void EnemyMove()
    {
        if (Agent.remainingDistance <= Agent.stoppingDistance)
        {
            ChangeAnim(Constant.IDLE);
            if (RandomMovement(centerPoint.position, radius, out point))
            {
                Debug.DrawRay(Point, Vector3.up, Color.red, 1f);
                this.transform.LookAt(Point);
                this.ChangeAnim(Constant.RUN);
                Agent.SetDestination(Point);
            }
        }

        //Debug.Log("list count " + listCheckInRange.Count);
        //Debug.Log("target" + target);
    }

    private bool RandomMovement(Vector3 center, float radius, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * radius; ;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    public void StopMoving()
    {
        ChangeAnim(Constant.IDLE);
        rb.velocity = Vector3.zero;
    }

    public void EnemyAttak()
    {
        if (CanThrow)
        {
            Throw();

        }
    }
}
