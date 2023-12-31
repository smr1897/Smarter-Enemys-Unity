using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunToCover_State : IState
{
    private EnemyReferences enemyReferences;
    private CoverArea coverArea;
    

    public RunToCover_State(EnemyReferences enemyReferences, CoverArea coverArea)
    {
        this.enemyReferences = enemyReferences;
        this.coverArea = coverArea;
    }

    public void OnEnter()
    {
         Cover nextCover = coverArea.GetRandomCover(enemyReferences.transform.position);
         enemyReferences.navMeshAgent.SetDestination(nextCover.transform.position);
                
    }

    public void OnExit()
    {
        enemyReferences.animator.SetFloat("Speed", 0f);
    }

    public void Tick()
    {
        enemyReferences.animator.SetFloat("Speed", enemyReferences.navMeshAgent.desiredVelocity.sqrMagnitude);
    }

    public Color GizmoColor()
    {
        return Color.blue;
    }

    

    public bool HasArrivedAtDestination()
    {
        return enemyReferences.navMeshAgent.remainingDistance < 0.1f ;
    }
}
