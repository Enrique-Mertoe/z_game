using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombiePatrolState : StateMachineBehaviour
{
    private float timer;

    public float patrolingTime = 10f;

    private Transform player;

    private NavMeshAgent navAgent;

    public float detectionArea = 18f;

    public float patrolSpeed = 2f;

    private List<Transform> waypointList = new();
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent = animator.GetComponent<NavMeshAgent>();
        NavMeshHit hit;
        if (NavMesh.SamplePosition(animator.transform.position, out hit, 2.0f, NavMesh.AllAreas))
        {
            // navAgent.Warp(hit.position);
            Instantiate(navAgent, hit.position, Quaternion.identity);

        }

        navAgent.speed = patrolSpeed;
        timer = 0;
        var waypointCluster = GameObject.FindGameObjectWithTag("WayPoints");

        foreach (Transform t in waypointCluster.transform)
        {
            waypointList.Add(t);
        }

        var nextPosition = waypointList[Random.Range(0, waypointList.Count)].position;
        navAgent.SetDestination(nextPosition);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!navAgent)
        {
            Debug.Log("VAR");
            return;
        }
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
            navAgent.SetDestination(waypointList[Random.Range(0, waypointList.Count)].position);
        timer += Time.deltaTime;
        if (timer>patrolingTime)
        {
         animator.SetBool("isWalking",false);   
        }
        animator.SetBool("isChasing",
            Vector3.Distance(player.position, animator.transform.position) < detectionArea);
    
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        navAgent.SetDestination(navAgent.transform.position);
    }
}
