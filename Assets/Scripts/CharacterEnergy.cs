using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterEnergy : MonoBehaviour
{
    public float maxEnergy = 100f;
    public float minEnergy = 10f;
    public float currentEnergy;

    public EnergyBar energyBar;
    public GameObject warningText;

    public float runEnergyPerFrame = 0.5f;
    public float ghostAttack = 5f;
    public float getEnergyPerFrame = 1f;
    public float dashEnergy = 5f;
    public float walkEnergyPerFrame = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);
        warningText.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        // Run Energy
        // Shift tuþu basýlý -> true
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            if (currentEnergy > minEnergy)
            {
                TakeDamage(runEnergyPerFrame * Time.deltaTime * 2);
            }
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (currentEnergy > minEnergy)
            {
                TakeDamage(runEnergyPerFrame * Time.deltaTime * 3);
            }
        }

        if (currentEnergy <= minEnergy)
        {
            warningText.SetActive(true);
        }
        else
        {
            warningText.SetActive(false);
        }
        
        // Dash 
        if (Input.GetKeyDown(KeyCode.Q) && this.gameObject.GetComponent<CharacterMovement>().dashCount > 0)
        {
            TakeDamage(dashEnergy);
        }

    }

    public void TakeDamage(float damage)
    {
        if (currentEnergy > minEnergy)
        {
            currentEnergy -= damage;
            energyBar.SetEnergy(currentEnergy);
        }
    }

    public void IncreaseEnergy(float energy)
    {
        if (currentEnergy < maxEnergy)
        {
            currentEnergy += energy;
            energyBar.SetEnergy(currentEnergy);
        }

    }

}