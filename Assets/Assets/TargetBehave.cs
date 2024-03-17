using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetBehave : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public GameObject deathEffect;

    public Slider healthBar;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.value = CalculateHealth();
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = CalculateHealth();
        health += 0.5f * Time.deltaTime;     
        if (health > maxHealth)
        {
            health = maxHealth;
        }

    }

    float CalculateHealth()
    {   
        return health / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
