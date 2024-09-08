using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    [SerializeField] private int hp = 100;
    private Animator animator;

    public bool isDead;

    private bool _dead;

    public NavMeshAgent navMeshAgent;
    public void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        isDead = false;
        _dead = false;
        
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 2.0f, NavMesh.AllAreas))
        {
            // Set the agent's position to the nearest valid position on the NavMesh
            navMeshAgent.Warp(hit.position);
        }

    }

    public void TakeDamage(int damageAmount)
    {
        if (_dead) return;
        hp -= damageAmount;
        if (hp <=0)
        {
            var val = Random.Range(0, 2);
            animator.SetTrigger($"DIE{val+1}");
            isDead = true;
            _dead = true;

        }
        else
        {
            animator.SetTrigger("DAMAGE");
            isDead = false;
        }
    }
}
