using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieChaseState : StateMachineBehaviour
{
    private NavMeshAgent _agent;

    private Transform _player;

    public float chaseSpeed = 6f;

    public float stopChaseDistance = 21;

    public float attackDistance = 2.5f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent = animator.GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _agent.speed = chaseSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_player.position);
        animator.transform.LookAt(_player);

        var distanceFromPlayer = Vector3.Distance(_player.position, animator.transform.position);
        if (distanceFromPlayer > stopChaseDistance)
        {
            animator.SetBool("isChasing",false);
        }

        if (distanceFromPlayer < attackDistance)
        {
            animator.SetBool("isAttacking",true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(animator.transform.position);
    }
}
