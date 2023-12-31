using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload_State : IState
{
    private EnemyReferences enemyReferences;
    
    public Reload_State(EnemyReferences enemyReferences)
    { 
        this.enemyReferences = enemyReferences;
    }

    public void OnEnter()
    {
        Debug.Log("Start Reload");
        enemyReferences.animator.SetFloat("cover", 1);
        enemyReferences.animator.SetTrigger("Reload");
    }

    public void OnExit()
    {
        Debug.Log("Stop Reloading");
        enemyReferences.animator.SetFloat("cover", 0);
    }

    public void Tick()
    {

    }

    public Color GizmoColor()
    {
        return Color.gray;
    }
}
