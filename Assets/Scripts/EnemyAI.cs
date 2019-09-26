using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float attackValueAdjustment = 0.2f;

    NavMeshAgent navMeshAgent;
    Animator myAnimator;

    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    	
	void Start ()
    {
        target = FindObjectOfType<PlayerHealth>().transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        myAnimator = GetComponent<Animator>();
	}
		
	void Update ()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position) - attackValueAdjustment; 

        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
            ChaseTarget();
        }
                
    }

    private void ChaseTarget()
    {
        myAnimator.SetTrigger("move");
        myAnimator.SetBool("attack", false);
        navMeshAgent.SetDestination(target.position);
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    public void OnDeath()
    {
        navMeshAgent.enabled = false;
        this.enabled = false;
    }

    private void EngageTarget()
    {
        FaceTarget();
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        else if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        myAnimator.SetBool("attack", true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
