using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover_State : IState
{
    private EnemyReferences enemyReferences;
    private StateMachine stateMachine;

    public Cover_State(EnemyReferences enemyReferences)
    {
        this.enemyReferences = enemyReferences;
        stateMachine = new StateMachine();

        //States
        var enemyShoot = new Shoot_State(enemyReferences);
        var enemyDelay = new Delay_State(1f);
        var enemyReload = new Reload_State(enemyReferences);

        //Transitions
        At(enemyShoot, enemyReload, () => enemyReferences.shooter.ShouldReload());
        At(enemyReload, enemyDelay, () => !enemyReferences.shooter.ShouldReload());
        At(enemyDelay, enemyShoot, () => enemyDelay.IsDone());

        //Start State
        stateMachine.SetState(enemyShoot);

        //Functions and Conditions
        void At(IState from , IState to , Func<bool> condition) => stateMachine.AddTransition(from , to , condition);
        void Any(IState to , Func<bool> condition) => stateMachine.AddAnyTransition(to , condition);
    }

    public void OnEnter()
    {
        enemyReferences.animator.SetBool("Combat", true);
    }

    public void OnExit()
    {
        enemyReferences.animator.SetBool("Combat", false);
    }

    public void Tick()
    {
        stateMachine.Tick();
    }

    public Color GizmoColor()
    {
        //return Color.gray;
        return stateMachine.GetGizmoColor();
    }

    
}
