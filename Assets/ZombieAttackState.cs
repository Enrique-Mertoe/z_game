using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAttackState : StateMachineBehaviour
{
    private Transform _player;

    private NavMeshAgent _agent;

    public float stopAttackingDistance = 2.5f;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = animator.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LookAtPlayer();
        var distanceFromPlayer = Vector3.Distance(_player.position, animator.transform.position);
        if (distanceFromPlayer > stopAttackingDistance)
        {
            animator.SetBool("isAttacking",false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    

    void LookAtPlayer()
    {
        var direction = _player.position - _agent.nextPosition;
        _agent.transform.rotation = Quaternion.LookRotation(direction);
        var yRotation = _agent.transform.eulerAngles.y;
        _agent.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

}
