using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackRange = 3f;
    public int attackDamage = 1;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public LayerMask enemyLayers;

    //public GameObject attackEffect;
    public AudioSource swing;
    public AudioSource hit;

    public GameObject claws;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
        }   
    }

    public void Attack()
    {
        if (!readyToAttack || attacking) return;
        if (GetComponent<CharacterEnergy>().currentEnergy<=10) return;
        claws.GetComponent<Animator>().SetTrigger("Attack");
        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);
    }

    public void ResetAttack()
    {
        readyToAttack = true;
        attacking = false;
    }

    public void AttackRaycast()
    {
        attackCount++;
        if (attackCount >= 3)
        {
            attackCount = 0;
            readyToAttack = false;
            attacking = true;
            Invoke(nameof(ResetAttack), attackSpeed);
        }
        else
        {
            readyToAttack = false;
            attacking = true;
            Invoke(nameof(ResetAttack), attackSpeed);
        }

        swing.Play();
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<GhostMovement>().TakeDamage(attackDamage);
            //Instantiate(attackEffect, enemy.transform.position, Quaternion.identity);
            hit.Play();
        }

        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
