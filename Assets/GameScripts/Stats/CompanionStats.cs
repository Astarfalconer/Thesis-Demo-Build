using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CompanionStats : CharacterStats
{
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckResurrect();
        //CheckEnemies();
    }

    public override void Die()
    {
        base.Die();
        animator.SetTrigger("Die");
        Debug.Log(transform.name + " has died.");
        // Add companion-specific death logic here (e.g., play animation, drop loot)
    }

    public void CheckResurrect()
    {
        if (isDead)
        {
           if (EnemyManager.instance.enemies.Count == 0)
           {
               
               currentHealth = maxHealth;
               animator.SetTrigger("Resurrect");
               Debug.Log(transform.name + " has been resurrected.");
            }
        }
    }

    


}
