using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Smart_Enemy_Control : MonoBehaviour
{
    private EnemyReferences enemyReferences;
    private StateMachine stateMachine;
    
    void Start()
    {
        enemyReferences = GetComponent<EnemyReferences>();
        
        stateMachine = new StateMachine();

        CoverArea coverArea = FindObjectOfType<CoverArea>();

        var runToCover = new RunToCover_State(enemyReferences , coverArea);
        var delayAfterRun = new Delay_State(2f);
        var cover = new Cover_State(enemyReferences);

        At(runToCover, delayAfterRun, () => runToCover.HasArrivedAtDestination());

        At(delayAfterRun, cover, () => delayAfterRun.IsDone());

        //start state
        stateMachine.SetState(runToCover);

        

        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, Func<bool> condition) => stateMachine.AddAnyTransition(to, condition);

        
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Tick();
    }

    private void OnDrawGizmos()
    {
        if(stateMachine != null)
        {
            Gizmos.color = stateMachine.GetGizmoColor();
            Gizmos.DrawSphere(transform.position + Vector3.up * 3, 0.4f);
        }
    }
}
