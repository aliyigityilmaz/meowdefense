using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Transform target;
    public LayerMask whatIsGround, whatIsPlayer, whatIsTarget;

    [Header("Extras")]
    public int health;
    public GameObject deathEffect;
    public GameObject deathEffectTwo;

    [Header("Patrol")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    [Header("Attack Settings")]
    public float timeBeetweenAttacks;
    bool alreadyAttacked;
    public int damage;

    [Header("States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    [Header("TargetStates")]
    public float targetSightRange, targetAttackRange;
    public bool targetInSightRange, targetInAttackRange;

    private CharacterEnergy characterEnergy;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        target = GameObject.Find("Target").transform;
        agent = GetComponent<NavMeshAgent>();
        characterEnergy = player.GetComponent<CharacterEnergy>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        targetInSightRange = Physics.CheckSphere(transform.position, targetSightRange, whatIsTarget);
        targetInAttackRange = Physics.CheckSphere(transform.position, targetAttackRange, whatIsTarget);

        if (Vector3.Distance(player.position, this.gameObject.transform.position) < Vector3.Distance(target.position, this.gameObject.transform.position) && characterEnergy.currentEnergy >= 10)
        {
            if(player == null) return;
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange && characterEnergy.currentEnergy >= 10) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }
        else
        {
            if (target == null) return;
            if (!targetInSightRange && !targetInAttackRange) Patroling();
            if (targetInSightRange && !targetInAttackRange) ChaseTarget();
            if (targetInSightRange && targetInAttackRange) AttackTarget();
        }


    }
    
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void ChaseTarget()
    {
        agent.SetDestination(target.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            if (GetComponent<Animator>() != null)
            {
                GetComponent<Animator>().SetTrigger("Attack");
            }
            Attack();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBeetweenAttacks);
        }
    }

    private void AttackTarget()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(target);

        if (!alreadyAttacked)
        {
            if (GetComponent<Animator>() != null)
            {
                GetComponent<Animator>().SetTrigger("Attack");
            }
            DamageTarget();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBeetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public void Die()
    {   
        GameObject vfx1 = Instantiate(deathEffect, transform.position, transform.rotation);
        GameObject vfx2 = Instantiate(deathEffectTwo, transform.position, transform.rotation);
        Destroy(vfx1, 5f);
        Destroy(vfx2, 5f);
        Destroy(gameObject);
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Attack()
    {
        player.GetComponent<CharacterEnergy>().TakeDamage(damage);
    }

    public void DamageTarget()
    {
        target.GetComponent<TargetBehave>().TakeDamage(damage);
    }
}
