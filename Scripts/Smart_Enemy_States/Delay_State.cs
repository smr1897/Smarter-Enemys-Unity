using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay_State : IState
{
    private float waitForSeconds;
    private float deadLine;

    public Delay_State(float waitForSeconds)
    {
        this.waitForSeconds = waitForSeconds;
    }

    public void OnEnter()
    {
        deadLine = Time.time + waitForSeconds;
    }

    public void OnExit()
    {
        Debug.Log("Enemy Delay onExit");
    }

    public void Tick()
    {

    }

    public Color GizmoColor()
    {
        return Color.white;
        
    }

    
    public bool IsDone()
    {
        return Time.time >= deadLine;
        
    }
}
