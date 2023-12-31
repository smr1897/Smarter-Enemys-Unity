using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{

    public Transform target;

    private EnemyReferences enemyReferences;

    private float shootingDistance;

    private float pathUpdateDeadline;

    private void Awake()
    {
        enemyReferences = GetComponent<EnemyReferences>();
    }

    void Start()
    {
        shootingDistance = enemyReferences.navMeshAgent.stoppingDistance;
        
    }

   
    void Update()
    {
        if(target != null)
        {
            bool inRange = Vector3.Distance(transform.position, target.position) <= shootingDistance;

            if(inRange)
            {
                LookAtTarget();
            }
            else
            {
                UpdatePath();
            }
            enemyReferences.animator.SetBool("Shooting", inRange);
        }
        enemyReferences.animator.SetFloat("Speed", enemyReferences.navMeshAgent.desiredVelocity.sqrMagnitude);
    }

    private void LookAtTarget()
    {
        Vector3 LookPos = target.position - transform.position;
        LookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(LookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }

    private void UpdatePath()
    {
       if(Time.time >= pathUpdateDeadline)
       {
            Debug.Log("Updating path");
            pathUpdateDeadline = Time.time + enemyReferences.pathUpdateDelay;
            enemyReferences.navMeshAgent.SetDestination(target.position);
       }
    }
}
